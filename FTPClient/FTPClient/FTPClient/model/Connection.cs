using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace FTPClient.model
{
    //encapsulated connection
    public class Connection : IDisposable
    {
        //implements disposable
        public Connection(TcpClient tcpClient)
        {
            TcpClient = tcpClient;
            NetworkStream = TcpClient.GetStream();
            SslStream = new SslStream(NetworkStream, false, ValidateCert);
            SslStream.AuthenticateAsClient("FTPClient");

            BinaryReader = new BinaryReader(SslStream, Encoding.UTF8);
            BinaryWriter = new BinaryWriter(SslStream, Encoding.UTF8);
        }

        public TcpClient TcpClient { get; private set; }
        public NetworkStream NetworkStream { get; private set; }
        public BinaryReader BinaryReader { get; private set; }
        public SslStream SslStream { get; private set; }
        public BinaryWriter BinaryWriter { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void CloseConnection()
        {
            TcpClient.Close();
        }

        private static bool ValidateCert(object sender, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true; //allow untrusted certificates
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
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