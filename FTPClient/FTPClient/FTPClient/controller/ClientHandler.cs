using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FTPClient.model;
using FTPClient.model.events;
using FTPClient.util;
using FTPLibrary;
using Directory = FTPLibrary.Directory;

namespace FTPClient.controller
{
    internal class ClientHandler
    {

        public event EventHandler LoginAuthorised;
        public event EventHandler Disconnected;
        public event ServerErrorEventHandler ServerError;
        public event ServerEventHandler ServerEvent;
        public event ClientInfoEventHandler ClientInfo;
        public event UploadedEvent.DirectoryUploadedEventHandler DirectoryUploaded;
        public event UploadedEvent.FileUploadedEventHandler FileUploaded;
        public event UploadedEvent.ProgressEventHandler ProgressOfFile;
        public event UploadedEvent.ListOfProgressFileEventHandler ListOfFiles;

        protected virtual void OnListOfProgressFiles(UploadedEvent.FileListProgressArgs e)
        {
            ListOfFiles?.Invoke(this, e);
        }
        protected virtual void OnProgressOfFile(UploadedEvent.FileProgressArgs e)
        {
            ProgressOfFile?.Invoke(this, e);
        }
        protected virtual void OnFileUploaded(UploadedEvent.UploadedFileArgs e)
        {
            FileUploaded?.Invoke(this, e);
        }
        protected virtual void OnDirectoryUploaded(UploadedEvent.UploadedDirectoryArgs e)
        {
            DirectoryUploaded?.Invoke(this,e);
        }

        protected virtual void OnLoginAuthorised()
        {
            LoginAuthorised?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnServerError(ServerErrorArgs e)
        {
            ServerError?.Invoke(this, e);
        }

        protected virtual void OnServerEvent(ServerArgs e)
        {
            ServerEvent?.Invoke(this, e);
        }

        protected virtual void OnDisconncted()
        {
            Disconnected?.Invoke(this,EventArgs.Empty);
        }

        protected virtual void OnClientLogIssued(ClientInfoArgs e)
        {
            ClientInfo?.Invoke(this, e);
        }


        public Connection ConnectionToServer { get; private set; }
        public Host HostClientInformation { get; set; }

        public Thread ClientThread { get; set; }

        public void Connect(Host hostClientInformation)
        {

            if (this.ConnectionToServer?.TcpClient != null)
            {
                if (this.ConnectionToServer.TcpClient.Connected)
                {
                    return;
                }
            }
            this.HostClientInformation = hostClientInformation;
            this.ClientThread = new Thread(HandleClientSetup);
            ClientThread.Start();

        }


        //public void Register(Client client)
        //{
        //    Connect(client, true);
        //}

        private void HandleClientSetup()
        {
            try
            {
                using (ConnectionToServer = new Connection(new TcpClient(HostClientInformation.Ip, Int32.Parse(HostClientInformation.Port))))
                {
                    AuthenticateWithServer();
                    byte autenticationResponse = ConnectionToServer.BinaryReader.ReadByte();
                    switch (autenticationResponse)
                    {
                        case Commands.Ready:
                            OnClientLogIssued(new ClientInfoArgs("Authenticated with server."));
                            OnLoginAuthorised();
                            HandleReceive();
                            break;
                        case Commands.ErrorUserDoesNotExist:
                            OnServerError(new ServerErrorArgs("SERVER ERROR: User does not exist."));
                            CloseConnection();
                            break;
                        case Commands.ErrorPasswrong:
                            OnServerError(new ServerErrorArgs("SERVER ERROR: Password incorrect."));
                            CloseConnection();
                            break;
                        case Commands.ErrorUserAlreadyLoggedIn:
                            OnServerError(new ServerErrorArgs("SERVER ERROR: This user is already logged in."));
                            CloseConnection();
                            break;
                        case Commands.ErrorUnknownCommand:
                            OnServerError(new ServerErrorArgs("SERVER ERROR: Unknown command."));
                            CloseConnection();
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                OnClientLogIssued(new ClientInfoArgs("Could not connect to server."));
                if (ConnectionToServer?.TcpClient != null)
                {
                    if (ConnectionToServer.TcpClient.Connected)
                    {
                        CloseConnection();
                    }
                }
            }
        }

        private void HandleReceive()
        {
            try
            {
                ConnectionToServer.BinaryWriter.Write(Commands.Ready);
                while (ConnectionToServer.TcpClient.Connected)
                {
                    byte responseFromServer = ConnectionToServer.BinaryReader.ReadByte();
                    List<FileProgress> listOfFileProgresses;
                    switch (responseFromServer)
                    {
                        case Commands.SendingRootNode:
                            OnClientLogIssued(new ClientInfoArgs("Receiving root directory from server..."));
                            int lengthOfNodeObject = ConnectionToServer.BinaryReader.ReadInt32();
                            byte[] incommingNodePacket = ConnectionToServer.SslStream.ReadStreamTillEnd(lengthOfNodeObject);

                            Directory rootFtpNode = (Directory) incommingNodePacket.ByteStreamToObject(typeof (Directory));
                            OnServerEvent(new ServerArgs(rootFtpNode));
                            OnClientLogIssued(new ClientInfoArgs("Root directory recieved."));
                            break;
                        case Commands.Shutdown:
                            CloseConnection();
                            break;
                        case Commands.Download:
                            listOfFileProgresses = null;
                            try
                            {

                                //server will return empty string if there is no directory
                                string directoryName = ConnectionToServer.BinaryReader.ReadString();

                                int byteFileArrayLength = ConnectionToServer.BinaryReader.ReadInt32();
                                byte[] byteFiles = ConnectionToServer.BinaryReader.ReadBytes(byteFileArrayLength);
                                List<FTPLibrary.File> files = (List<FTPLibrary.File>) byteFiles.ByteStreamToObject(typeof (List<FTPLibrary.File>));
                                listOfFileProgresses = files.Select(c => new FileProgress(c.FileName, false, true, c.FileSize.LongToString(), 0)).ToList();

                                //send the the list to the UI thread
                                OnListOfProgressFiles(new UploadedEvent.FileListProgressArgs(listOfFileProgresses));

                                try
                                {
                                    if (directoryName != "")
                                        System.IO.Directory.CreateDirectory(HostClientInformation.DownloadPath + @"\" + directoryName);
                                }
                                catch
                                {
                                    //we don't care too much if it fails, we will just place all of the files within the download directory.
                                    directoryName = "";
                                }
                                foreach (FTPLibrary.File f in files)
                                {
                                    //    OnClientLogIssued(new ClientInfoArgs("Downloading " + f.FileName + "."));
                                    listOfFileProgresses.Find(file => file.FileName == f.FileName).Completed = "Downloading..";

                                    long byteArrayLength = ConnectionToServer.BinaryReader.ReadInt64();

                                    if (byteArrayLength == -1)
                                    {
                                        //send log to UI thread
                                        OnClientLogIssued(new ClientInfoArgs("There was an issue downloading the following file " + f.FileName + ". This file will be skipped."));
                                        continue; //this can be used to skip a file, if there is an issue
                                    }
                                
                                //we need to check if the file being downloaded is empty, otherwise we get some very
                                    //strange stream reads with the next file, which will either cause a overflow exception or just cause a read block
                                    if (byteArrayLength != 0)
                                    {
                                        // this method will read all of the bytes and write them to the file, meaning no bytes are stored in memory - system resources are not spent needlessly
                                        //ConnectionToServer.SslStream.WriteStreamToFileTillEnd(byteArrayLength, f, HostClientInformation.DownloadPath, listOfFileProgresses.Find(file => file.FileName == f.FileName));
                                        byte[] fileBuffer = new byte[1 << 15];

                                        long totalRead = 0;

                                        using (var fileWrite = System.IO.File.Create(HostClientInformation.DownloadPath + @"\" + directoryName + @"\" + f.FileName))
                                        {
                                            int dataFrames;
                                            while ((dataFrames = ConnectionToServer.SslStream.Read(fileBuffer, (int) totalRead, fileBuffer.Length - (int) totalRead)) > 0)
                                            {
                                                totalRead += dataFrames;

                                                //beauty of objects, updating progress of a specific file.
                                                long progress = (long)((fileWrite.Length + totalRead) * 100.0 / byteArrayLength);
                                                listOfFileProgresses.Find(file => file.FileName == f.FileName).Progress = progress;

                                                if (totalRead == fileBuffer.Length) // we wait until the buffer is full before we write it to the file
                                                {
                                                    fileWrite.Write(fileBuffer, 0, (int) totalRead);
                                                    fileBuffer = new byte[1 << 15];
                                                    totalRead = 0;
                                                }
                                                if (fileWrite.Length + totalRead == byteArrayLength) //if this condition is met, we can write the remaining bytes and break out of here!
                                                {
                                                    fileWrite.Write(fileBuffer, 0, (int) totalRead);
                                                    break;
                                                }

                                            
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //if the length of a file is 0 and we reach this point, we must change the progress to 100, as the iteration write is skipped.
                                        listOfFileProgresses.Find(file => file.FileName == f.FileName).Progress = 100;
                                        using (new FileStream(HostClientInformation.DownloadPath + @"\" + directoryName + @"\" + f.FileName, FileMode.Create))
                                        {
                                        } //create the empty file with the specified name
                                    }
                                    listOfFileProgresses.Find(file => file.FileName == f.FileName).Completed = "Downloaded..";
                                    // OnClientLogIssued(new ClientInfoArgs("File: " + f.FileName + " completed downloading."));
                                }
                            }
                            catch (Exception e)
                            {
                                listOfFileProgresses?.FindAll(file => file.Completed != "Downloaded..").ForEach(file => file.Completed = "Failed..");
                                if (ConnectionToServer.TcpClient.Connected == false)
                                    throw new Exception();
                            }
                            //perform garbage collection
                            //GC.Collect(); - now not required, we are never storing the read bytes in memory as they are directly written to a file
                            RequestRootNode();
                            break;
                        case Commands.Upload:
                            listOfFileProgresses = null;
                            try
                            {
                                byte action = ConnectionToServer.BinaryReader.ReadByte();
                                switch (action)
                                {
                                    case Commands.Directory:
                                        int directoryByteArrayLength = ConnectionToServer.BinaryReader.ReadInt32();
                                        byte[] directoryByteArray = ConnectionToServer.BinaryReader.ReadBytes(directoryByteArrayLength);
                                        FTPLibrary.Directory directoryToUpload = (FTPLibrary.Directory) directoryByteArray.ByteStreamToObject(typeof (FTPLibrary.Directory)); //both the server are working with the same directory at this point
                                        listOfFileProgresses = directoryToUpload.Files.Values.ToList().Select(c => new FileProgress(c.FileName,true,false,c.FileSize.LongToString(),0)).ToList();
                                        OnListOfProgressFiles(new UploadedEvent.FileListProgressArgs(listOfFileProgresses));
                                        foreach (FTPLibrary.File f in directoryToUpload.Files.Values)
                                        {
                                            //`OnClientLogIssued(new ClientInfoArgs("Uploading file: " + f.FileName + "."));
                                            listOfFileProgresses.Find(file => file.FileName == f.FileName).Completed = "Uploading..";
                                            try
                                            {
                                                using (FileStream fs = new FileStream(f.Path, FileMode.Open))
                                                {
                                                    long length = fs.Length;
                                                    ConnectionToServer.BinaryWriter.Write(length);

                                                    byte[] buffer = new byte[1 << 15]; //32k buffer
                                                    int lengthOfPacket;
                                                    long totalWritten = 0;
                                                    while ((lengthOfPacket = fs.Read(buffer, 0, buffer.Length)) > 0)
                                                    {
                                                        totalWritten += lengthOfPacket;
                                                        //write the packets in 32k buffer streams
                                                        ConnectionToServer.SslStream.Write(buffer, 0, lengthOfPacket);
                                                        long progress = (long)(totalWritten * 100.0 / length);
                                                        listOfFileProgresses.Find(file => file.FileName == f.FileName).Progress = progress;
               
                                                    }
                                                    //if the length of a file is 0 and we reach this point, we must change the progress to 100, as the iteration write is skipped.
                                                    if (length == 0)
                                                        listOfFileProgresses.Find(file => file.FileName == f.FileName).Progress = 100;

                                                    listOfFileProgresses.Find(file => file.FileName == f.FileName).Completed = "Uploaded..";
                                                    ConnectionToServer.Flush();
                                                }
                                                //OnProgressOfFile(new UploadedEvent.FileProgressArgs(0));
                                            }
                                            catch
                                            {
                                                listOfFileProgresses.Find(file => file.FileName == f.FileName).Completed = "Failed..";
                                                OnClientLogIssued(new ClientInfoArgs("There was an issue uploading the following file: " + f.FileName + ". This file will be skipped."));
                                                long errorSkipFile = -1;
                                                ConnectionToServer.BinaryWriter.Write(errorSkipFile);
                                                directoryToUpload.Files.Remove(f.FileName);

                                            }
                                           // OnClientLogIssued(new ClientInfoArgs("File: " + f.FileName + " has been uploaded."));
                                        }
                                        OnDirectoryUploaded(new UploadedEvent.UploadedDirectoryArgs(directoryToUpload));

                                        break;
                                    case Commands.Files:
                                        int fileListByteArrayLength = ConnectionToServer.BinaryReader.ReadInt32();
                                        byte[] fileListByteArray = ConnectionToServer.BinaryReader.ReadBytes(fileListByteArrayLength);
                                        List<FTPLibrary.File> fileList = (List<FTPLibrary.File>)fileListByteArray.ByteStreamToObject(typeof(List<FTPLibrary.File>)); //both the server are working with the same directory at this point
                                        listOfFileProgresses = fileList.Select(c => new FileProgress(c.FileName, true, false, c.FileSize.LongToString(), 0)).ToList();
                                        OnListOfProgressFiles(new UploadedEvent.FileListProgressArgs(listOfFileProgresses));
                                        foreach (FTPLibrary.File f in fileList)
                                        {
                                            listOfFileProgresses.Find(file => file.FileName == f.FileName).Completed = "Uploading..";

                                            try
                                            {
                                                using (FileStream fs = new FileStream(f.Path, FileMode.Open))
                                                {
                                                    long length = fs.Length;
                                                    ConnectionToServer.BinaryWriter.Write(length);

                                                    byte[] buffer = new byte[1 << 15]; //32k buffer
                                                    int lengthOfPacket;
                                                    long totalWritten = 0;
                                                    while ((lengthOfPacket = fs.Read(buffer, 0, buffer.Length)) > 0)
                                                    {
                                                        totalWritten += lengthOfPacket;
                                                        //write the packets in 32k buffer streams
                                                        ConnectionToServer.SslStream.Write(buffer, 0, lengthOfPacket);
                                                        long progress = (long)((totalWritten) * 100.0 / (length));
                                                        listOfFileProgresses.Find(file => file.FileName == f.FileName).Progress = progress;
                                                    }
                                                    //if the length of a file is 0 and we reach this point, we must change the progress to 100, as the iteration write is skipped.
                                                    if (length == 0)
                                                        listOfFileProgresses.Find(file => file.FileName == f.FileName).Progress = 100;
                                                    listOfFileProgresses.Find(file => file.FileName == f.FileName).Completed = "Uploaded..";
                                                    ConnectionToServer.Flush();
                                                }
                                                OnFileUploaded(new UploadedEvent.UploadedFileArgs(f));
                                            }
                                            catch (Exception e)
                                            {
                                                listOfFileProgresses.Find(file => file.FileName == f.FileName).Completed = "Failed..";
                                                OnClientLogIssued(new ClientInfoArgs("There was an issue uploading the following file: " + f.FileName + ". This file will be skipped."));
                                                long errorSkipFile = -1;
                                                ConnectionToServer.BinaryWriter.Write(errorSkipFile);
                                            }
                                        }
                                        break;
                                }                     
                            }
                            catch (Exception e)
                            {
                                listOfFileProgresses?.FindAll(file => file.Completed != "Uploaded..").ForEach(file => file.Completed = "Failed..");
                                OnClientLogIssued(new ClientInfoArgs("Failed to upload the selected files."));
                                if (ConnectionToServer.TcpClient.Connected == false) 
                                    throw new Exception();
                            }
                            RequestRootNode();
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                CloseConnection();
            }
        }


        public void RequestRootNode()
        {
            try
            {
                if (ConnectionToServer != null && ConnectionToServer.TcpClient.Connected)
                {
                    ConnectionToServer.BinaryWriter.Write(Commands.Ready);
                }
            }
            catch
            {
            }
        }
        public void Download(List<FTPLibrary.File> files, FTPLibrary.Directory directory)
        {
            try
            {
                if (files == null) return;
                if (ConnectionToServer == null || !ConnectionToServer.TcpClient.Connected) return;
   
                this.ConnectionToServer.BinaryWriter.Write(Commands.Download);
                if (directory == null)
                {
                    this.ConnectionToServer.BinaryWriter.Write(Commands.Files);

                }
                else
                {
                    this.ConnectionToServer.BinaryWriter.Write(Commands.Directory);
                    string directoryName = directory.DirectoryName;
                    this.ConnectionToServer.BinaryWriter.Write(directoryName);
                }
                byte[] filesToByteArray = files.ObjectToByte();
                ConnectionToServer.BinaryWriter.Write(filesToByteArray.Length);
                ConnectionToServer.SslStream.Write(filesToByteArray);
                //byte[] finished = new byte[1];
                //finished[0] = 54;
                ConnectionToServer.SslStream.Write(Commands.FinishedArray);
            }
            catch
            {
            }
        }

        public void UploadDirectory(FTPLibrary.Directory directoryToUpload)
        {
            try
            {
                if (directoryToUpload == null) return;
                if (ConnectionToServer == null || !ConnectionToServer.TcpClient.Connected) return;

                this.ConnectionToServer.BinaryWriter.Write(Commands.Upload);
                this.ConnectionToServer.BinaryWriter.Write(Commands.Directory);
                byte[] directoryObjToBytes = directoryToUpload.ObjectToByte();
                ConnectionToServer.BinaryWriter.Write(directoryObjToBytes.Length);
                ConnectionToServer.SslStream.Write(directoryObjToBytes);
                ConnectionToServer.BinaryWriter.Write(Commands.Finished);
            }
            catch { }
        }

        public void UploadFiles(List<FTPLibrary.File> filesToUpload)
        {
            try
            {
                if (filesToUpload == null) return;
                if (ConnectionToServer == null || !ConnectionToServer.TcpClient.Connected) return;

                this.ConnectionToServer.BinaryWriter.Write(Commands.Upload);
                this.ConnectionToServer.BinaryWriter.Write(Commands.Files);
                byte[] directoryObjToBytes = filesToUpload.ObjectToByte();
                ConnectionToServer.BinaryWriter.Write(directoryObjToBytes.Length);
                ConnectionToServer.SslStream.Write(directoryObjToBytes);
                ConnectionToServer.BinaryWriter.Write(Commands.Finished);
            }
            catch { }
        }

        private void AuthenticateWithServer()
        {
            OnClientLogIssued(new ClientInfoArgs("Authenticating with server..."));
            ConnectionToServer.BinaryWriter.Write(Commands.Setup);
            ConnectionToServer.BinaryWriter.Write(Commands.Login);
            ConnectionToServer.BinaryWriter.Flush();

            byte receiveAuth = ConnectionToServer.BinaryReader.ReadByte();
            if (receiveAuth == Commands.Authenticate)
            {
                ConnectionToServer.BinaryWriter.Write(Commands.Authenticate);
                ConnectionToServer.BinaryWriter.Flush();
                ConnectionToServer.BinaryWriter.Write(HostClientInformation.UserName);
                ConnectionToServer.BinaryWriter.Write(HostClientInformation.UserPassword);
                ConnectionToServer.BinaryWriter.Flush();
            }
        }


        private void CloseConnection()
        {
            OnClientLogIssued(new ClientInfoArgs("Closing connection with server..."));
            try
            {
                ConnectionToServer.CloseConnection();
            }
            catch (Exception) { }
            OnDisconncted();
            OnClientLogIssued(new ClientInfoArgs("Connection closed..."));
        }
    }
}

