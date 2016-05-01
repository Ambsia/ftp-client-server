using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FTPServer.controller
{

    public class TcpListenerWrapper : TcpListener
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpListener"/> class that listens for incoming connection attempts on the specified local IP address and port number.
        /// </summary>
        /// <param name="localaddr">An <see cref="T:System.Net.IPAddress"/> that represents the local IP address. </param><param name="port">The port on which to listen for incoming connection attempts. </param><exception cref="T:System.ArgumentNullException"><paramref name="localaddr"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="port"/> is not between <see cref="F:System.Net.IPEndPoint.MinPort"/> and <see cref="F:System.Net.IPEndPoint.MaxPort"/>. </exception>
        public TcpListenerWrapper(IPAddress localaddr, int port) : base(localaddr, port)
        {
        }

        public bool IsActive
        {
            get { return base.Active; }
        }
    }
}
