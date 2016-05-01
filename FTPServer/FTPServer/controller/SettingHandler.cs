using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FTPServer.model;
using FTPServer.util;

namespace FTPServer.controller
{
    public class SettingHandler
    {
        public ChangeSettingHandler ServerSettingChangedEventHandler;

        private readonly ServerInformation _serverInformation;
        public SettingHandler(ServerInformation serverInformation)
        {
            this._serverInformation = serverInformation;
            this.ServerSettingChangedEventHandler += PushSettingsToServer;
        }

        private void PushSettingsToServer(object sender, ServerSettingEventArgs eventArgs)
        {
            //used to check if the newly set port is available or can be used,
            //prevents crashes by simulating a server startup in a safe environment 
            TcpListener testListener;
            
            try
            {
                testListener = new TcpListener(ServerInformation.GetIpAddress(), eventArgs.Port);
                testListener.Start();
                testListener.Stop();
                _serverInformation.ServerPortAddress = eventArgs.Port;
                _serverInformation.ServerCapacity = eventArgs.Capacity;
                if (eventArgs.Saving)
                {
                    _serverInformation.SaveServerSettings();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("That port is not available.","Warning",  MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                testListener = null;
                GC.Collect(2);
            }
        }



    }


}

