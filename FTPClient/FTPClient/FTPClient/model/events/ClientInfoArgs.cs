using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPClient.model.events
{
    public class ClientInfoArgs : EventArgs
    {
        public ClientInfoArgs(string information)
        {
            this.Information = information;
        }

        public string Information { get; }
    }

    public delegate void ClientInfoEventHandler(object sender, ClientInfoArgs e);

}
