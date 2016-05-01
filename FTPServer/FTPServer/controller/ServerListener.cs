using FTPServer.model;
using FTPServer.util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FTPLibrary;

namespace FTPServer.controller
{
    public class ServerListener
    {
        public Stopwatch UpTime { get; }
        public Clientele Clientele { get; }
        public Thread ServerThread { get; private set; }
        public FileRepository FileRepository { get; }
        public TcpListenerWrapper TcpListener { get; set; }

        public ServerInformation ServerInformation { get; }

        public ServerListener(ServerInformation information, Clientele clientele, FileRepository fileRepository)
        {
            this.Clientele = clientele;
            this.UpTime = new Stopwatch();
            this.FileRepository = fileRepository;
            this.ServerInformation = information;

        }

        public bool IsActive()
        {
            return TcpListener != null && TcpListener.IsActive;
        }

        public void StartServer()
        {
            if (!IsActive())
            {
                try
                {
                    this.TcpListener = new TcpListenerWrapper(ServerInformation.GetIpAddress(),
                        ServerInformation.ServerPortAddress);
                    this.TcpListener.Start();
                    UpTime.Start();
                    ConsoleBuffer.AddToStatusLog("Server started on socket: " + this.TcpListener.LocalEndpoint);
                    this.ServerThread = new Thread(this.StartToListen);
                    this.ServerThread.Start();
                }
                catch (Exception)
                {
                    //ignored
                }
            }
        }

        private void StartToListen()
        {
            ConsoleBuffer.AddToActionLog("Waiting for a client connection.");
            try
            {
                while (this.IsActive())
                {
                    if (Clientele.RegisteredUsers.ToList().Where(cli => cli.Value.LoggedIn).ToList().Count >= ServerInformation.ServerCapacity) continue;

                    TcpClient client = this.TcpListener.AcceptTcpClient();

                    ConsoleBuffer.AddToActionLog("Client connection accepted from: " +
                                                 ((IPEndPoint) client.Client.RemoteEndPoint).Address);
                    ClientHandler c = new ClientHandler(FileRepository, Clientele, client);

                    Thread.Sleep(10);
                }
            }
            catch (Exception e)
            {

            }
    }

        public void RestartServer()
        {

            this.StopServer();
            this.StartServer();
        }

        public void StopServer()
        {
            if (this.IsActive())
            {
                this.Clientele.RegisteredUsers.ToList().ForEach(client => client.Value.LoggedIn = false); //all users are going to be logged out..
                this.Clientele.RegisteredUsers.ToList().ForEach(client => client.Value.ClientConnection?.ClientConnection?.TcpClient?.Close()); //close every active client
                this.ServerThread.Abort();
                this.TcpListener.Stop();
                this.UpTime.Reset();
                ConsoleBuffer.AddToStatusLog("Server has been stopped.");
            }
        }
    }


}

