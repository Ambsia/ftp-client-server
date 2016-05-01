using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPClient.model
{
    public class ServerErrorArgs : EventArgs
    {
        public ServerErrorArgs(string errorMsg)
        {
            this.ErrorMessage = errorMsg;
        }
        public string ErrorMessage { get; }
    }

    public delegate void ServerErrorEventHandler(object sender, ServerErrorArgs e);
}
