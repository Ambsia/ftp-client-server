using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FTPServer.util;

namespace FTPServer.model
{
    public class Clientele
    {

        public NewClientEventHandler ModifiedClientEventHandler;

        protected virtual void OnClientModified(ClientEventArgs client)
        {
            ModifiedClientEventHandler?.Invoke(this, client);
        }

        public Dictionary<string, ClientInformation> RegisteredUsers;
        private readonly string _fileName;

        public void SaveDictionaryToFile()
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (FileStream fileStream = new FileStream(this._fileName, FileMode.Create, FileAccess.Write))
                {
                    binaryFormatter.Serialize(fileStream, RegisteredUsers.Values.ToArray());
                }
                ConsoleBuffer.AddToActionLog("Dictionary saved as list to file.");
            }
            catch (Exception e)
            {
                ConsoleBuffer.AddToActionLog("Exeception Occured, could not write to file." + e.ToString());
            }
        }

        public void LoadDictionaryFromFile()
        {
            try
            {
                using (FileStream fileStream = new FileStream(this._fileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    ClientInformation[] clientInfoAsArray = (ClientInformation[])binaryFormatter.Deserialize(fileStream);

                    this.RegisteredUsers = clientInfoAsArray.ToDictionary(client => client.Username, client => client);
                }
                ConsoleBuffer.AddToActionLog("File loaded as dictionary." + "Dictionary count: " + this.RegisteredUsers.Count + ".");
            }
            catch (Exception e)
            {
                ConsoleBuffer.AddToActionLog("Exeception Occured, could not load file." + e.Message);
            }
        }

        public virtual void TryAdd(ClientInformation clientInformation)
        {
            if (UserExists(clientInformation.Username)) return;
            this.RegisteredUsers.Add(clientInformation.Username, clientInformation);
            this.SaveDictionaryToFile();
            OnClientModified(new ClientEventArgs(clientInformation, ClientEventArgs.ClientAction.Add));
        }


        public virtual void TryRemove(string clientKey)
        {
            if (!UserExists(clientKey)) return;
            this.RegisteredUsers.Remove(clientKey);
            this.SaveDictionaryToFile();
            OnClientModified(new ClientEventArgs(new ClientInformation(clientKey,""), ClientEventArgs.ClientAction.Remove));
        }

        public virtual void TryEdit(ClientInformation clientInformation, string originalUsername)
        {
            if (!UserExists(clientInformation.Username)) return;
            TryRemove(originalUsername);
            TryAdd(clientInformation);
            this.SaveDictionaryToFile();
            //  OnClientModified(new ClientEventArgs(originalDetails, ClientEventArgs.ClientAction.Edit));
        }
        public void ClearDictionary()
        {
            this.RegisteredUsers.Clear();
            SaveDictionaryToFile();
        }
        public Clientele(string fileName)
        {
            RegisteredUsers = new Dictionary<string, ClientInformation>();
            this._fileName = fileName;
            this.LoadDictionaryFromFile();
        }

        public bool UserExists(string key) { return this.RegisteredUsers.ContainsKey(key); }

        public int ConnectedUserCount()
        {
            return RegisteredUsers.Count > 0 ? RegisteredUsers.Count(kvp => kvp.Value.LoggedIn == true) : 0;
        }

        public List<ListViewItem> ClienteleAsList()
        {
            List<ListViewItem> itemListOfClinetele = new List<ListViewItem>();
            List<string> listOfKeys = new List<string>(RegisteredUsers.Keys);
            for (int i = 0; i < RegisteredUsers.Count; i++)
            {
                ClientInformation clientInformation;
                if (RegisteredUsers.TryGetValue(listOfKeys[i], out clientInformation))
                {
                    itemListOfClinetele.Add(new ListViewItem(new[] { clientInformation.Username, clientInformation.LoggedIn == true ? "Online" : "Offline" }));
                }
            }
            return itemListOfClinetele;
        }
    }
}
