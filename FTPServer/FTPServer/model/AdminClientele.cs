using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPServer.model
{
    //making it clear that there are two types of clientele, could just have clientele..
    public class AdminClientele : Clientele
    {

        public new NewClientEventHandler ModifiedClientEventHandler;

        protected new virtual void OnClientModified(ClientEventArgs client)
        {
            ModifiedClientEventHandler?.Invoke(this, client);
        }


        public AdminClientele(string fileName) : base(fileName)
        {
        }

        public override void TryRemove(string clientKey)
        {
            if (!UserExists(clientKey)) return;
            this.RegisteredUsers.Remove(clientKey);
            this.SaveDictionaryToFile();
            OnClientModified(new ClientEventArgs(new ClientInformation(clientKey, ""), ClientEventArgs.ClientAction.Remove));
        }

        public override void TryAdd(ClientInformation clientInformation)
        {
            if (UserExists(clientInformation.Username)) return;
            this.RegisteredUsers.Add(clientInformation.Username, clientInformation);
            this.SaveDictionaryToFile();
            OnClientModified(new ClientEventArgs(clientInformation, ClientEventArgs.ClientAction.Add));
        }

        public override void TryEdit(ClientInformation clientInformation, string originalUsername)
        {
            if (UserExists(clientInformation.Username)) return;
            TryRemove(originalUsername);
            TryAdd(clientInformation);
            this.SaveDictionaryToFile();
            //OnClientModified(new ClientEventArgs(originalDetails, ClientEventArgs.ClientAction.Edit));
        }
    }
}
