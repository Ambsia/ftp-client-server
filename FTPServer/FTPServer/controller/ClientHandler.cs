using FTPServer.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Documents.DocumentStructures;
using FTPLibrary;
using FTPServer.util;
using File = FTPLibrary.File;

namespace FTPServer.controller
{
    public class ClientHandler
    {


        public FileRepository FileRepository { get; }
        public Clientele Clientele { get; }
        public ClientConnection ClientConnection { get; private set; }
        private readonly TcpClient _tcpClient;
        private ClientInformation _clientInformation;

        public ClientHandler(FileRepository fileRepository, Clientele clientele, TcpClient tcpClient)
        {
            this._tcpClient = tcpClient;
            Thread f = new Thread(ClientLoop);
            f.SetApartmentState(ApartmentState.STA);
            f.Start();
            this.FileRepository = fileRepository;
            this.Clientele = clientele;
        }

        /* this method handles the download request, by taking reading the requested files,
            sending them back to the client - so they are both definitely working on the suggested
            files. Then we iterate over the files and start writing the contents of each opened file stream,
            reading straight from the file stream; prevents the usage of a necessary amount of memory. for example,
            if we were reading all of the bytes contained in the file into memory, if the file was 32GB we would strugle 
            for resources, doing this allows the server to handle any file size.
            */
        internal void HandleDownload()
        {
            ConsoleBuffer.AddToActionLog("Download initiated from: " + _clientInformation.Username + ".");
            byte action = ClientConnection.BinaryReader.ReadByte();
            string directoryName = "";
            if (action == Commands.Directory)
            { //read and write back the directory name, the client would of lost it at this point
                directoryName = ClientConnection.BinaryReader.ReadString();
            }
            int byteFileArrayLength = ClientConnection.BinaryReader.ReadInt32();
            byte[] incommingPackets = ClientConnection.SslStream.ReadStreamTillEnd(byteFileArrayLength);
            List<File> files = (List<File>) incommingPackets.ByteStreamToObject(typeof (List<File>));

            ClientConnection.BinaryWriter.Write(Commands.Download);
            ClientConnection.BinaryWriter.Write(directoryName);

            byte[] filesToByteArray = files.ObjectToByte();
            ClientConnection.BinaryWriter.Write(filesToByteArray.Length);
            ClientConnection.BinaryWriter.Write(filesToByteArray);
            //write the entire byte array 
            foreach (File f in files)
            {
                ConsoleBuffer.AddToActionLog("File: " + f.FileName + " is being transferred to: " + _clientInformation.Username + ".");
                try
                {
                    using (FileStream fs = new FileStream(f.Path, FileMode.Open))
                    {
                        long length = fs.Length;
                        ClientConnection.BinaryWriter.Write(length);

                        byte[] buffer = new byte[1 << 15]; //32k buffer
                        int lengthOfPacket;
                        while ((lengthOfPacket = fs.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            //write the packets in 32k buffer streams
                            ClientConnection.SslStream.Write(buffer, 0, lengthOfPacket);
                        }
                        ClientConnection.Flush();
                    }
                    ConsoleBuffer.AddToActionLog("File: " + f.FileName + " transferred to: " + _clientInformation.Username + ".");
                }
                catch (Exception e)
                {
                    ConsoleBuffer.AddToActionLog("File: " + f.FileName + " failed to transfer to: " + _clientInformation.Username + ".");
                    long errorSkipFile = -1;
                    ClientConnection.BinaryWriter.Write(errorSkipFile);
                }
            }
            //GC.Collect();
        }
        /* this method handles the upload request, first we switch whether the client is uploading a directory or a list of files,
            we send whatever they want back to them - ensuring we're working on the same thing. We proceed to start checking if 
            there are files/directory's that need overriding. 
            We then start writing the stream to file, meaning the file stream is closed once the agreed upon length of file stream has been reached.
            This, again ensures we are not overly using system resources because we are only storing a set amount of bytes in memory at any one time, 
            in this case we are using 1<<15 (32kb) buffer.*/
        internal void HandleUpload()
        {
            byte action = ClientConnection.BinaryReader.ReadByte();
            ConsoleBuffer.AddToActionLog("Upload initiated from: " + _clientInformation.Username + ".");
            switch (action)
            {
                case Commands.Directory:
                    int lengthOfByteDirectory = ClientConnection.BinaryReader.ReadInt32();
                    byte[] directoryByteArray = ClientConnection.SslStream.ReadStreamTillEnd(lengthOfByteDirectory);
                    FTPLibrary.Directory directory = (FTPLibrary.Directory)directoryByteArray.ByteStreamToObject(typeof(FTPLibrary.Directory));
                    ClientConnection.BinaryWriter.Write(Commands.Upload);
                    ClientConnection.BinaryWriter.Write(Commands.Directory); // let the user know we're ready to accept the files
                    ConsoleBuffer.AddToActionLog("Directory: " + directory.DirectoryName + " with a total of " + directory.Files.Count + " files are being receieved from:" + _clientInformation.Username + ".");
                    // here we send the very same object back to the cient, this ensures we recieve the files we first was given for and nothing else.
                    ClientConnection.BinaryWriter.Write(directoryByteArray.Length);
                    ClientConnection.BinaryWriter.Write(directoryByteArray);

                    //first, before we start downloading files we must create the directories
                    if (FileRepository.RootDirectory.SubDirectories.ContainsKey(directory.DirectoryName))
                    {
                        FileRepository.RootDirectory.SubDirectories.Remove(directory.DirectoryName);
                    }

                    FileRepository.RootDirectory.AddDirectory(directory);
                    System.IO.Directory.CreateDirectory(FileRepository.RootDirectory.FullPath + @"\" + directory.DirectoryName);

                    foreach (FTPLibrary.File f in directory.Files.Values)
                    {
                        ConsoleBuffer.AddToActionLog("File: " + f.FileName + " is being received from: " + _clientInformation.Username + ".");
                        long byteArrayLength = ClientConnection.BinaryReader.ReadInt64();
                        if (byteArrayLength == -1) continue; //this can be used to skip a file, if there is an issue with the current
                        
                        if (byteArrayLength != 0) 
                            ClientConnection.SslStream.WriteStreamToFileTillEnd(byteArrayLength, f, directory.FullPath);
                        else
                        {
                            using (new FileStream(directory.FullPath + @"\" + f.FileName, FileMode.Create))
                            {
                            } //create the empty file with the specified name
                        }
                        f.Path = directory.FullPath + @"\" + f.FileName;
                        ConsoleBuffer.AddToActionLog("File: " + f.FileName + " receveied from: " + _clientInformation.Username + ".");
                    }

                    FileRepository.FillDictionary();
                    break;
                case Commands.Files:
                    int fileListByteArrayLength = ClientConnection.BinaryReader.ReadInt32();
                    byte[] fileListByteArray = ClientConnection.SslStream.ReadStreamTillEnd(fileListByteArrayLength);
                    List<FTPLibrary.File> listOfFiles = (List<FTPLibrary.File>)fileListByteArray.ByteStreamToObject(typeof(List<FTPLibrary.File>));
                    ClientConnection.BinaryWriter.Write(Commands.Upload);
                    ClientConnection.BinaryWriter.Write(Commands.Files);

                    ClientConnection.BinaryWriter.Write(fileListByteArray.Length);
                    ClientConnection.BinaryWriter.Write(fileListByteArray);

                    foreach (FTPLibrary.File f in listOfFiles)
                    {
                        ConsoleBuffer.AddToActionLog("File: " + f.FileName + " is being received from: " + _clientInformation.Username + ".");
                        long byteArrayLength = ClientConnection.BinaryReader.ReadInt64();
                        if (byteArrayLength == -1) continue; //this can be used to skip a file, if there is an issue with the current

                        if (byteArrayLength != 0)
                            ClientConnection.SslStream.WriteStreamToFileTillEnd(byteArrayLength, f, FileRepository.RootDirectory.FullPath);
                        else
                        {
                            using (new FileStream(FileRepository.RootDirectory.FullPath + @"\" + f.FileName, FileMode.Create))
                            {
                            } //create the empty file with the specified name
                        }

                        f.Path = FileRepository.RootDirectory.FullPath + @"\" + f.FileName;
                        FileRepository.RootDirectory.AddFile(f);
                        ConsoleBuffer.AddToActionLog("File: " + f.FileName + " receveied from: " + _clientInformation.Username + ".");
                    }
                    FileRepository.FillDictionary();
                    break;
            }
        }

        /* this is the authentication process each client must pass before they are authenticated, bcrypt and inner salts are used to keep the details
           unreadable. Binary reader and writer are used during this process, which are directly using the SSL stream. Making the data unreadable by packet sniffers*/
        internal void ClientSetup(byte command)
        {
            ClientConnection.BinaryWriter.Write(Commands.Authenticate);
            byte authenticateResponse = ClientConnection.BinaryReader.ReadByte();
            if (authenticateResponse == Commands.Authenticate)
            {
                string userName = ClientConnection.BinaryReader.ReadString();
                string userPass = ClientConnection.BinaryReader.ReadString();
                switch (command)
                {
                    case Commands.Login:
                        if (Clientele.RegisteredUsers.TryGetValue(userName, out _clientInformation))
                        {
                            if (_clientInformation.LoggedIn != true)
                            {
                                if (Cryptography.CompareValues(userPass, _clientInformation.Password))
                                {
                                    _clientInformation.LoggedIn = true;
                                    _clientInformation.ClientConnection = this;
                                    Clientele.SaveDictionaryToFile();
                                    ClientConnection.BinaryWriter.Write(Commands.Ready); //let client know that server is ready to communicate
                                    ConsoleBuffer.AddToActionLog("The following user has logged in.");
                                    ConsoleBuffer.PrintUserDetails(_clientInformation);
                                }
                                else
                                {
                                    ConsoleBuffer.AddToActionLog("Incorrect password entered for user " + _clientInformation.Username + ".");
                                    ClientConnection.BinaryWriter.Write(Commands.ErrorPasswrong);
                                    ClientConnection.CloseConnection();
                                }
                            }
                            else
                            {
                                _clientInformation = null;
                                ClientConnection.BinaryWriter.Write(Commands.ErrorUserAlreadyLoggedIn);
                                ClientConnection.CloseConnection();
                            }
                        }
                        else
                        {
                            ClientConnection.BinaryWriter.Write(Commands.ErrorUserDoesNotExist);
                            ClientConnection.CloseConnection();
                        }
                        break;
                    case Commands.Logout:
                        if (_clientInformation.LoggedIn)
                        {
                            ClientConnection.CloseConnection();
                        }
                        else
                        {
                            ClientConnection.BinaryWriter.Write(Commands.ErrorUserNotLoggedIn);
                            ClientConnection.CloseConnection();
                        }
                        break;
                    default:
                        ClientConnection.CloseConnection();
                        ClientConnection.BinaryWriter.Write(Commands.ErrorUnknownCommand); break;
                }
            }
            else
            {
                ClientConnection.CloseConnection();
            }
        }


        /*this method handles the connection between the client, a
        switch it used to determine the path the client is taking. 
        command bytes are used to inform the server of the request*/
        internal void ClientLoop()
        {

            try
            {
               // IPEndPoint ep = _tcpClient.Client.RemoteEndPoint as IPEndPoint;
             //   ConsoleBuffer.AddToActionLog("Connection started, sender address = " + ep.Address.ToString());
                using (ClientConnection = new ClientConnection(_tcpClient))
                {
                    using (ClientConnection.BinaryReader)
                    {
                        while (ClientConnection.TcpClient.Connected)
                        {
                            byte command = ClientConnection.BinaryReader.ReadByte();
                            switch (command)
                            {
                                case Commands.Ready:
                                    ClientConnection.BinaryWriter.Write(Commands.SendingRootNode);
                                    byte[] rootDirAsBytes = FileRepository.RootDirectory.ObjectToByte();
                                    ClientConnection.BinaryWriter.Write(rootDirAsBytes.Length);
                                    ClientConnection.BinaryWriter.Flush();
                                    ClientConnection.NetworkStream.Flush();
                                    ClientConnection.SslStream.Write(rootDirAsBytes);
                                    byte[] finished = new byte[1];
                                    finished[0] = 54;
                                    ClientConnection.SslStream.Write(finished);
                                    break;
                                case Commands.Setup:
                                    byte accountAction = ClientConnection.BinaryReader.ReadByte();
                                    ClientSetup(accountAction);
                                    break;
                                case Commands.Upload:
                                    try
                                    {
                                        HandleUpload();
                                    }
                                    catch (Exception exception)
                                    {
                                        ConsoleBuffer.AddToActionLog("Failed to save incomming file.");
                                        if (!ClientConnection.TcpClient.Connected)
                                            throw new Exception();
                                    }
                                    break;
                                case Commands.Download:
                                    try
                                    {
                                        HandleDownload();
                                    }
                                    catch (Exception exception)
                                    {
                                        ConsoleBuffer.AddToActionLog("Failed to send file.");
                                        if(!ClientConnection.TcpClient.Connected)
                                            throw new Exception();
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                ClientConnection?.CloseConnection();

                if (_clientInformation != null)
                {
                    _clientInformation.LoggedIn = false;
                }
                // ignored
            }
        }
    }
}
