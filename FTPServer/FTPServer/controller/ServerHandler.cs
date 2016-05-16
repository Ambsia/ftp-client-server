using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FTPServer.util;
using System.Windows.Forms;
using FTPLibrary;
using FTPServer.model;
using Directory = FTPLibrary.Directory;

namespace FTPServer.controller
{
    public class ServerHandler
    {
        public TabControl.TabPageCollection  ControlCollection { get; set; }
        public ServerInformation ServerInformation { get; }
        public ServerListener ServerListener { get;}
        public SettingHandler SettingHandler { get; }
        public UserHandler UserHandler { get; }
        public DirectoryHandler DirectoryHandler { get; }

        public ServerHandler(UserHandler userHandler)
        {
            this.ServerInformation = new ServerInformation();
            ServerInformation.LoadServerSettings();
            this.SettingHandler = new SettingHandler(ServerInformation);
            this.UserHandler = userHandler;

            DirectoryInfo dirInfo = new DirectoryInfo(ServerInformation.DefaultDirectory);
            this.DirectoryHandler = new DirectoryHandler(new FileRepository("directory_info.json", new Directory(
                dirInfo.Name,
                dirInfo.FullName,
                new List<Client>(userHandler.AdminClientele.RegisteredUsers.Keys.ToList().Select(key => new Client(key))),
                new Client(userHandler.AdminClientele.RegisteredUsers.ElementAt(0).Value.Username))));

            this.ServerListener = new ServerListener(ServerInformation, userHandler.Clientele, DirectoryHandler.FileRepository);
        }

        public void InitiateServer(object sender, DoWorkEventArgs e)
        {
            ServerListener.StartServer();
        }

        public void RestartServer(object sender, DoWorkEventArgs e)
        {
            ServerListener.RestartServer();
        }
        public void StopServer(object sender, DoWorkEventArgs e)
        {
            ServerListener.StopServer();
        }

        public void UpdateServerInformation()
        {
            if (ControlCollection == null || ServerListener.Clientele == null) return;
            ServerInformation.Update();

            ControlCollection[0].Controls[0].Controls[0].Controls["lblStatus"].Text = ServerListener.UpTime.ElapsedMilliseconds > 0 ? "Online" : "Offline";
            ControlCollection[0].Controls[0].Controls[0].Controls["lblIP"].Text = "" + ServerInformation.GetIpAddress();
            ControlCollection[0].Controls[0].Controls[0].Controls["lblPort"].Text = "" + ServerInformation.ServerPortAddress;
            ControlCollection[0].Controls[0].Controls[0].Controls["lblCPUUsage"].Text = ServerInformation.CpuUsage;
            ControlCollection[0].Controls[0].Controls[0].Controls["lblMemoryUsage"].Text = "" + ServerInformation.MemoryUsage;
            ControlCollection[0].Controls[0].Controls[0].Controls["lblCapacity"].Text = "" + ServerInformation.ServerCapacity;
            ControlCollection[0].Controls[0].Controls[0].Controls["lblBytesSent"].Text = "" + ServerInformation.TotalSentBytes;
            ControlCollection[0].Controls[0].Controls[0].Controls["lblBytesReceived"].Text = "" + ServerInformation.TotalReceivedBytes;
            ControlCollection[0].Controls[0].Controls[0].Controls["lblUsers"].Text = ServerListener.Clientele.ConnectedUserCount() > 0 || ServerListener.Clientele.RegisteredUsers.Count > 0
                ? ServerListener.Clientele.ConnectedUserCount() + "/" + ServerListener.Clientele.RegisteredUsers.Count
                : "0/" + ServerListener.Clientele.RegisteredUsers.Count;
            ControlCollection[0].Controls[0].Controls["txtServerActions"].Text = String.Join(String.Empty, ConsoleBuffer.ServerStatusLog.ToArray().Reverse());
            ControlCollection[3].Controls["txtActionLog"].Text = String.Join(String.Empty, ConsoleBuffer.ServerActionLog.ToArray().Reverse());
            ControlCollection[0].Controls[0].Controls[0].Controls["lblUptime"].Text = ServerListener.UpTime.ElapsedMilliseconds > 0
                ? string.Format("{0:D2}:{1:D2}:{2:D2}",
                TimeSpan.FromMilliseconds(ServerListener.UpTime.ElapsedMilliseconds).Hours,
                TimeSpan.FromMilliseconds(ServerListener.UpTime.ElapsedMilliseconds).Minutes,
                TimeSpan.FromMilliseconds(ServerListener.UpTime.ElapsedMilliseconds).Seconds) + ""
                : "00:00:00";
        }
    }
}
