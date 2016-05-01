using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPServer.model
{
    public class ClientEventArgs : EventArgs
    {
        public ClientInformation NewClient { get; }
        public bool ToBeRemoved { get; }
        public bool Edited { get; }
        public ClientAction ActionToBeTaken { get; }
    
        public ClientEventArgs(ClientInformation client, ClientAction actionToBeTaken)
        {
            this.NewClient = client;
            this.ActionToBeTaken = actionToBeTaken;

        }
        public enum ClientAction
        {
            Remove,
            Add,
            Edit
        }
    }
    public delegate void NewClientEventHandler(object sender, ClientEventArgs e);
}
