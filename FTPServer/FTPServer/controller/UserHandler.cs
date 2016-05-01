using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FTPServer.model;
using FTPServer.Properties;
using FTPServer.util;
using Label = System.Windows.Forms.Label;

namespace FTPServer.controller
{
    public class UserHandler
    {
        public Clientele Clientele { get; }

        public AdminClientele AdminClientele { get; }

        public ListView ClienteleListView { get; set; }

        public ListView AdminListView { get; set; }

        public UserHandler(string clientele, string adminClientele)
        {
            this.Clientele = new Clientele(clientele);
            this.AdminClientele = new AdminClientele(adminClientele);

            Clientele.ModifiedClientEventHandler += UpdateCorrectViewList;
            AdminClientele.ModifiedClientEventHandler += UpdateCorrectViewList;


        }

        public void FillLists()
        {
            ClienteleListView.Items.AddRange(Clientele.ClienteleAsList().ToArray());
            AdminListView.Items.AddRange(AdminClientele.ClienteleAsList().ToArray());
        }

        public void EditClient(object sender, EventArgs e)
        {
            string buttonName = ((Button)sender).Name;

            ListView correctListView = buttonName == "btnEdit" ? ClienteleListView : AdminListView;
            if (correctListView.SelectedItems.Count <= 0) return;

            string originalUsername = correctListView.SelectedItems[0]?.Text;
            DialogResult result;
            ClientInformation clientInformation = new ClientInformation(originalUsername, "");
            do
            {
                result = ShowInputDialog(ref clientInformation);
                switch (result)
                {
                    case DialogResult.OK:
                        if (buttonName == "btnEdit")
                        {
                            Clientele.TryEdit(clientInformation, originalUsername);
                        }
                        else
                        {
                            AdminClientele.TryEdit(clientInformation, originalUsername);
                        }
                        break;
                    case DialogResult.Retry:
                        MessageBox.Show("Passwords must match.", "Warning", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        break;
                    case DialogResult.Abort:
                        MessageBox.Show("All fields must be filled in.", "Warning", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        break;
                }
            } while (result != DialogResult.OK && result != DialogResult.Cancel);
        }

        public void AddClient(object sender, EventArgs e)
        {
            string buttonName =  ((Button) sender).Name;

            DialogResult result;
            ClientInformation clientInformation = new ClientInformation("", "");
            do
            {
                result = ShowInputDialog(ref clientInformation);
                switch (result)
                {
                    case DialogResult.OK:
                        if (buttonName == "btnAdd")
                        {
                            Clientele.TryAdd(clientInformation);
                        }
                        else
                        {
                            AdminClientele.TryAdd(clientInformation);
                        }
                        break;
                    case DialogResult.Retry:
                        MessageBox.Show("Passwords must match.", "Warning", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        break;
                    case DialogResult.Abort:
                        MessageBox.Show("All fields must be filled in.", "Warning", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        break;
                }
            } while (result != DialogResult.OK && result != DialogResult.Cancel);
        }

        public void RemoveClient(object sender, EventArgs e)
        {
          //  AdminClientele.ClearDictionary();
            string buttonName = ((Button)sender).Name;
            ListView correctListView = buttonName == "btnRemove" ? ClienteleListView : AdminListView;

            if (correctListView.SelectedItems.Count <= 0) return;
            if (MessageBox.Show("Are you sure you wish to remove " + correctListView.SelectedItems[0]?.Text + "?", "Remove user", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (buttonName == "btnRemove")
                {
                    Clientele.TryRemove(correctListView.SelectedItems[0]?.Text);
                }
                else
                {
                    AdminClientele.TryRemove(correctListView.SelectedItems[0]?.Text);
                }
            }
        }


        private void UpdateCorrectViewList(object sender, ClientEventArgs clientEventArgs)
        {
            ListView correctListView = sender is AdminClientele ? AdminListView : ClienteleListView;
            //we must invoke the actual control not the thread, invoking a delegate to modify the list. 
            correctListView?.Invoke(new MethodInvoker(delegate
            {
                switch (clientEventArgs.ActionToBeTaken)
                {
                    case ClientEventArgs.ClientAction.Add:
                        correctListView.Items.Add(
                            new ListViewItem(new[]
                            {
                                clientEventArgs.NewClient.Username,
                                clientEventArgs.NewClient.LoggedIn ? "Online" : "Offline"
                            }));
                        break;
                    case ClientEventArgs.ClientAction.Edit:
                        ListViewItem itemToEdit = correctListView.FindItemWithText(correctListView.SelectedItems[0]?.Text);
                        itemToEdit.SubItems[0].Text = clientEventArgs.NewClient.Username;
                        break;
                    case ClientEventArgs.ClientAction.Remove:
                        correctListView.FindItemWithText(clientEventArgs.NewClient.Username).Remove();
                        break;

                }
            }));
        }

        private static DialogResult ShowInputDialog(ref ClientInformation clientInformation)
        {
            System.Drawing.Size size = new System.Drawing.Size(175, 110);
            Form inputBox = new Form
            {
                ClientSize = size,
                Text = "Manage User"
            };

            Label lblUsername = new Label()
            {
                Location = new System.Drawing.Point(5, 7),
                Size = new Size(58, 13),
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Text = "Username:"
            };
            inputBox.Controls.Add(lblUsername);

            TextBox txtUsername = new TextBox
            {
                //  Size = new System.Drawing.Size(size.Width - 20, 2),
                Location = new System.Drawing.Point(lblUsername.Width + 10, 5),
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,
                Text = clientInformation.Username
            };
            inputBox.Controls.Add(txtUsername);

            Label lblPassword = new Label()
            {
                Location = new System.Drawing.Point(5, 30),
                Size = new Size(58, 13),
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Text = "Password:"
            };
            inputBox.Controls.Add(lblPassword);
            TextBox txtPassword = new TextBox
            {
                // Size = new System.Drawing.Size(size.Width - 15, 28),
                Location = new System.Drawing.Point(lblUsername.Width + 10, 28),
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,

                PasswordChar = '*',
                Text = ""
            };
            inputBox.Controls.Add(txtPassword);

            Label lblConfirmPassword = new Label()
            {
                Location = new System.Drawing.Point(5, 53),
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Size = new Size(58, 13),
                Text = "Confirm:"
            };
            inputBox.Controls.Add(lblConfirmPassword);
            TextBox txtPasswordConfirm = new TextBox
            {
                // Size = new System.Drawing.Size(size.Width - 15, 28),
                Location = new System.Drawing.Point(lblUsername.Width + 10, 51),
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,
                PasswordChar = '*',
                Text = ""
            };
            inputBox.Controls.Add(txtPasswordConfirm);

            Button okButton = new Button
            {
                DialogResult = System.Windows.Forms.DialogResult.OK,
                Name = "okButton",
                Size = new System.Drawing.Size(70, 30),
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom,
                Text = "&OK",
                Location = new System.Drawing.Point(5, 73)
            };
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel,
                Name = "cancelButton",
                Size = new System.Drawing.Size(90, 30),
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right,

                Text = "&Cancel",
                Location = new System.Drawing.Point(79, 73)
            };

            inputBox.Controls.Add(cancelButton);
            inputBox.Size = new Size(235, 145);
            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;


            DialogResult result = inputBox.ShowDialog();
            if (txtPassword.Text != txtPasswordConfirm.Text)
            {
                result = DialogResult.Retry;
            }

            if (result != DialogResult.Cancel && (txtPassword.Text == "" || txtPassword.Text == "" || txtPasswordConfirm.Text == ""))
            {
                result = DialogResult.Abort;
            }

            clientInformation.Username = txtUsername.Text.Trim();
            clientInformation.Salt = Cryptography.GenerateSalt();
            clientInformation.Password = Cryptography.HashPassword(txtPassword.Text.Trim(), clientInformation.Salt);

            return result;
        }

    }
}
