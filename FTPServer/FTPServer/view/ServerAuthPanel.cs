using System;
using System.Windows.Forms;
using FTPServer.model;
using FTPServer.util;

namespace FTPServer.view
{
    public partial class FrmServerAuth : Form
    {
        private readonly AdminClientele _adminClientele;
        public FrmServerAuth(AdminClientele adminClientele)
        {
            InitializeComponent();
            this._adminClientele = adminClientele;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ClientInformation client;

                if (!_adminClientele.RegisteredUsers.TryGetValue(txtUsername.Text, out client) || !Cryptography.CompareValues(txtPassword.Text, client?.Password) || client?.LoggedIn != false)
                    throw new Exception("Cannot access account.");
                _adminClientele.RegisteredUsers[client.Username].LoggedIn = true;
                _adminClientele.SaveDictionaryToFile();
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                this.DialogResult = exception.Message == "Cannot access account." ? DialogResult.Retry : DialogResult.Abort;
            }
        }
        public void SetErrorMessage(string errorMsg)
        {
            lblError.Text = errorMsg;
        }

    }
}
