using System;
using FTPServer.controller;
using FTPServer.util;

namespace FTPServer.model
{
    [Serializable]
    public class ClientInformation
    {
        private string _salt;
       [NonSerialized] private bool _loggedIn;
       [NonSerialized] private ClientHandler _clientConnection;
        public ClientHandler ClientConnection
        {
            get { return this._clientConnection; }
            set { this._clientConnection = value; }
        }
        public bool LoggedIn
        {
            get { return this._loggedIn; }
            set { this._loggedIn = value; }
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt
        {
            get { return this._salt; }
            set { this._salt = value; }
        }
        public ClientInformation(string user, string unhashedPassword)
        {
            this.Username = user;
            this._salt = Cryptography.GenerateSalt();
            this.Password = Cryptography.HashPassword(unhashedPassword, _salt);
        }

        public override string ToString()
        {
            return Environment.NewLine + "Username= " + this.Username + Environment.NewLine +
                   "Password= " + this.Password + Environment.NewLine +
                   "Salt= " + this._salt + Environment.NewLine +
                   "Logged in= " + this._loggedIn;
        }
    }
}
