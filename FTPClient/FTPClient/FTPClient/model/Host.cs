using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.WindowsAPICodePack.Shell;

namespace FTPClient
{
    public class Host : INotifyPropertyChanged
    {

        private string _ip = "IP Address...";
        private string _port = "Port...";
        private string _userName = "Username...";
        private string _userPassword = "Password...";
        private string _downloadPath = KnownFolders.Downloads.Path;
        private string FileName { get; }
        readonly Regex _regCheckUsername = new Regex(@"^[A-Za-z]{4,12}$");
        readonly Regex _ipRegex = new Regex(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
        readonly Regex _portRegex = new Regex(@"^\d{1,6}$");

        public const int DetailsUnmatched = 0;
        public const int DetailsMatched = 1;
        public const int IpUnmatched = 2;
        public const int PortUnmatched = 3;
        public const int UsernameUnmatched = 5;
        public const int DirectoryDoesNotExist = 6;

        public Host(string fileName)
        {
            this.FileName = fileName;
            this.LoadDictionaryFromFile();
        }

        public int CheckDetails()
        {
            if (_ipRegex.IsMatch(_ip) && _portRegex.IsMatch(_port) && _regCheckUsername.IsMatch(_userName) && Directory.Exists(_downloadPath))
            {
                return DetailsMatched;
            }
            if (!_ipRegex.IsMatch(_ip) && !_portRegex.IsMatch(_port) && !_regCheckUsername.IsMatch(_userName) && !Directory.Exists(_downloadPath))
            {
                return DetailsUnmatched;
            }
            if (!_ipRegex.IsMatch(_ip))
            {
                return IpUnmatched;
            }
            if (!_portRegex.IsMatch(_port))
            {
                return PortUnmatched;
            }
            if (!_regCheckUsername.IsMatch(_userName))
            {
                return UsernameUnmatched;
            }
            if (!Directory.Exists(_downloadPath))
            {
                return DirectoryDoesNotExist;
                
            }

            return -1;
        }


        public string Ip
        {
            get
            {
                return _ip;
            }
            set
            {
                if (!_ipRegex.IsMatch(value) && value.ToString() != "IP Address...") {
                    throw new Exception("Incorrect address..");
                }
                if (_ip == value) return;
                _ip = value;
                OnPropertyChanged("IP");
            }
        }


        public string Port
        {
            get
            {
                return _port;
            }
            set
            {
               if (!_portRegex.IsMatch(value) && value != "Port...")
                {
                    throw new Exception("Incorrect port..");
                }
                if (_port == value) return;
                _port = value;
                OnPropertyChanged("Port");
            }
        }

        public string UserName
        {
            get { return this._userName; }
            set
            {
                if (!_regCheckUsername.IsMatch(value) && value != "Username...")
                {
                    _userName = value;
                    throw new Exception("Username incorrect; must be atleast 4 characters, but no more than 12.");
                }
                if (_userName == value) return;
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }

        public string UserPassword
        {
            get { return this._userPassword; }
            set
            {
                if (_userPassword == value) return;
                _userPassword = value;
                OnPropertyChanged("UserPassword");
            }
        }

        public string DownloadPath
        {
            get { return this._downloadPath; }
            set
            {
                if (!Directory.Exists(value))
                {
                    _downloadPath = value;
                    throw new Exception("Directory doesn't exist.");
                }
                if (_downloadPath == value) return;

                _downloadPath = value;
                OnPropertyChanged("DownloadPath");
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public void SaveDictionaryToFile()
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (FileStream fileStream = new FileStream(FileName, FileMode.Create, FileAccess.Write))
                {
                    this.UserPassword = "";
                    binaryFormatter.Serialize(fileStream, JsonConvert.SerializeObject(this));
                }
            }
            catch (Exception e)
            {
            }
        }

        public void LoadDictionaryFromFile()
        {
            try
            {
                using (FileStream fileStream = new FileStream(FileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    var jsonString = (string)binaryFormatter.Deserialize(fileStream);

                    this.Ip = JsonConvert.DeserializeObject<Host>(jsonString).Ip;
                    this.Port = JsonConvert.DeserializeObject<Host>(jsonString).Port;

                    this.UserName = JsonConvert.DeserializeObject<Host>(jsonString).UserName;
                    this.DownloadPath = JsonConvert.DeserializeObject<Host>(jsonString).DownloadPath;


                }
                //  ConsoleBuffer.AddToActionLog("File loaded as dictionary." + "Dictionary count: " + this._directoryDictionary.Count + ".");
            }
            catch (Exception e)
            {
            }
        }
    }
}
