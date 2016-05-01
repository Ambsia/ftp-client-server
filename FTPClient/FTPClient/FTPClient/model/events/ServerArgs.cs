using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FTPLibrary;

namespace FTPClient.model
{
    public class ServerArgs : EventArgs
    {
        public ServerArgs(Directory rootDirectory)
        {
            this.RootNode = rootDirectory;
        }
        public Directory RootNode { get; }
    }

    public delegate void ServerEventHandler(object sender, ServerArgs e);
}
