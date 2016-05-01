using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using FTPServer.controller;
using FTPServer.util;
using FTPServer.view;
using System.Windows.Forms;
using FTPLibrary;
using FTPServer.model;
using Directory = FTPLibrary.Directory;

namespace FTPServer.startup
{
    class Server
    {
        static void Main(string[] args)
        {
            UserHandler userHandler = new UserHandler(Environment.CurrentDirectory + "\\userinfo.dat", Environment.CurrentDirectory + "\\admindata.dat");
            DirectoryInfo dirInfo = new DirectoryInfo("main_dir");
            DirectoryHandler directoryHandler =
                new DirectoryHandler(new FileRepository("directory_info.json",new Directory(
                    dirInfo.Name,
                    dirInfo.FullName,
                    new List<Client>(userHandler.AdminClientele.RegisteredUsers.Keys.ToList().Select(key => new Client(key))), 
                    new Client(userHandler.AdminClientele.RegisteredUsers.ElementAt(0).Value.Username))));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FrmServerAuth frmServerAuth = new FrmServerAuth(userHandler.AdminClientele)
            {
                StartPosition = FormStartPosition.Manual,
                Location = new Point(10, 10)
            };
            DialogResult dialogResult;
            do
            {
               // userHandler.AdminClientele.TryAdd(new ClientInformation("Alex", "pass"));
                dialogResult = frmServerAuth.ShowDialog();
                switch (dialogResult)
                {
                    case DialogResult.OK:
                        Application.Run(new ServerControlPanel(new ServerController(userHandler,directoryHandler))
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(frmServerAuth.Location.X,frmServerAuth.Location.Y)
                        });
                        break;
                    case DialogResult.Retry:
                        frmServerAuth.SetErrorMessage("Incorrect username or password.");
                        break;
                    case DialogResult.Abort:
                        frmServerAuth.SetErrorMessage("Unexpected error occured.");
                        break;
                }
            } while (dialogResult == DialogResult.Retry || dialogResult == DialogResult.Abort);
        }
    }
}
