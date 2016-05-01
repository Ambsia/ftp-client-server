using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Windows.Threading;
using BrightIdeasSoftware;
using FTPLibrary;
using FTPServer.controller;
using FTPServer.model;
using FTPServer.util;
using Directory = FTPLibrary.Directory;

namespace FTPServer.view
{
    public partial class ServerControlPanel : Form
    {

        protected virtual void OnSettingsChanged(ServerSettingEventArgs serverSettingEventArgs)
        {
            _serverController.StatusHandler.SettingHandler.ServerSettingChangedEventHandler?.Invoke(this, serverSettingEventArgs);
        }

        private readonly ServerController _serverController;

        public ServerControlPanel(ServerController serverController)
        {
            
            InitializeComponent();
            timerUpdateInformation.Tick += UpdateServerInformation;
            timerUpdateInformation.Interval = 800;
            this._serverController = serverController;
            this._serverController.StatusHandler.ControlCollection = tbHolder.TabPages;
            this.btnStart.Click += BtnStart;
            this.btnRestart.Click += BtnRestart;
            this.btnStop.Click += BtnStop;
            this.backgroundWorker1.DoWork += this._serverController.StatusHandler.StopServer;
            this.backgroundWorker2.DoWork += this._serverController.StatusHandler.RestartServer;
            this.backgroundWorker3.DoWork += this._serverController.StatusHandler.InitiateServer;

            _serverController.UserHandler.ClienteleListView = lsvUsers;
            _serverController.UserHandler.AdminListView = lsvAdmins;
            _serverController.UserHandler.FillLists();
            treeView1.Nodes.Add(_serverController.DirectoryHandler.FileRepository.CreateDirectoryTree(_serverController.DirectoryHandler.FileRepository.RootDirectory));
            this.btnAdd.Click += _serverController.UserHandler.AddClient;
            this.btnAddAdmin.Click += _serverController.UserHandler.AddClient;
            this.btnRemove.Click += _serverController.UserHandler.RemoveClient;
            this.btnRemoveAdmin.Click += _serverController.UserHandler.RemoveClient;
            this.btnEdit.Click += _serverController.UserHandler.EditClient;
            this.btnEditAdmin.Click += _serverController.UserHandler.EditClient;
            txtPort.Text = _serverController.StatusHandler.ServerInformation.ServerPortAddress.ToString();
            txtCapacity.Text = _serverController.StatusHandler.ServerInformation.ServerCapacity.ToString();
            treeView1.NodeMouseClick += TreeView1_NodeMouseClick;

            btnPushSettings.Click += BtnHandleSettingRequest;
            btnSaveSettings.Click += BtnHandleSettingRequest;
            _serverController.DirectoryHandler.FileRepository.RefreshTree += FileRepository_RefreshTree;
        }

        private void FileRepository_RefreshTree(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate {
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(_serverController.DirectoryHandler.FileRepository.CreateDirectoryTree(_serverController.DirectoryHandler.FileRepository.RootDirectory));
            });
            
        }

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private void BtnHandleSettingRequest(object sender, EventArgs e)
        {
            string buttonName = ((Button) sender).Name;
            try
            {
                OnSettingsChanged(new ServerSettingEventArgs(Int32.Parse(txtPort.Text), (Int32.Parse(txtCapacity.Text) < 0 ? 0 : Int32.Parse(txtCapacity.Text)), buttonName == "btnSaveSettings"));
            }
            catch (Exception)
            {
                MessageBox.Show("These boxes only accept integer values.", "Error", MessageBoxButtons.OK,
                 MessageBoxIcon.Error);
            }
        }

        private void BtnStop(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy) return;
            backgroundWorker1.RunWorkerAsync();
        }

        private void BtnRestart(object sender, EventArgs e)
        {
            if (backgroundWorker2.IsBusy) return;
            backgroundWorker2.RunWorkerAsync();
        }

        private void BtnStart(object sender, EventArgs e)
        {
            if (backgroundWorker3.IsBusy) return;
            backgroundWorker3.RunWorkerAsync();
        }

        private void UpdateServerInformation(object sender, EventArgs e)
        {
            if (_serverController.StatusHandler.ServerListener == null) return;
            Dispatcher.CurrentDispatcher.Invoke(_serverController.StatusHandler.UpdateServerInformation);
            txtPort.Enabled = !_serverController.StatusHandler.ServerListener.IsActive();
            tpSettings.Controls.Cast<Control>()
                .ToList()
                .ForEach(control => control.Enabled = !_serverController.StatusHandler.ServerListener.IsActive());
        }
     
        private void FrmServerControlPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_serverController.StatusHandler.ServerListener.IsActive()) return;
            if (MessageBox.Show("Are you sure you wish to turn off the server?", "Are you sure?",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
            {
                e.Cancel = true;
                return;
            }
            _serverController.StatusHandler.ServerListener.StopServer();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
