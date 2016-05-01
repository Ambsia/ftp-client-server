using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPServer.model
{

    public class ServerSettingEventArgs : EventArgs
    {
        public int Port { get; }
        public int Capacity { get; }
        public bool Saving { get; }
        public ServerSettingEventArgs(int port, int capacity, bool saving)
        {
            this.Port = port;
            this.Capacity = capacity;
            this.Saving = saving;
        }
    }

    public delegate void ChangeSettingHandler(object sender, ServerSettingEventArgs e);

}