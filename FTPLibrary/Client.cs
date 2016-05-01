using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPLibrary
{
    [Serializable]
    public class Client
    {
        public string Username { get; }
        public Client(string username)
        {
            Username = username;
        }
    }
}
