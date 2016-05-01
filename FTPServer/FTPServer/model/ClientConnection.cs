using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FTPServer.model
{
    public class ClientConnection : IDisposable
    {
        private readonly X509Certificate2 _sslCertificate = new X509Certificate2(Path.GetFullPath(@"ftpserver.pfx"), @"");

        public TcpClient TcpClient { get; private set; }
        public NetworkStream NetworkStream { get; private set; }
        public BinaryReader BinaryReader { get; private set; }
        public SslStream SslStream { get; private set; }
        public BinaryWriter BinaryWriter { get; private set; }
        public ClientConnection(TcpClient tcpClient)
        {
            this.TcpClient = tcpClient;
            this.NetworkStream = tcpClient.GetStream();
            
            this.SslStream = new SslStream(this.NetworkStream);
            SslStream.AuthenticateAsServer(_sslCertificate, false, SslProtocols.Tls, true);
            this.BinaryReader = new BinaryReader(SslStream, Encoding.UTF8);
            this.BinaryWriter = new BinaryWriter(SslStream, Encoding.UTF8);
        }
        public void CloseConnection()
        {
      
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                TcpClient.Close();
                NetworkStream.Close();
                BinaryReader.Close();
                BinaryWriter.Close();
                SslStream.Close();
                TcpClient = null;
                NetworkStream = null;
                BinaryReader = null;
                BinaryWriter = null;
                SslStream = null;
            }
        }

        public void Flush()
        {
            NetworkStream.Flush();
            SslStream.Flush();
            BinaryWriter.Flush();

        }
    }
}

