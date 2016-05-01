using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPServer.controller
{
    public class ServerController
    {
        public ServerHandler StatusHandler { get; }
        public UserHandler UserHandler { get; }

        public DirectoryHandler DirectoryHandler { get; }

        public ServerController(UserHandler userHandler, DirectoryHandler directoryHandler)
        {
            this.UserHandler = userHandler;
            this.DirectoryHandler = directoryHandler;
            this.StatusHandler = new ServerHandler(UserHandler, DirectoryHandler);
        }
    }
}
