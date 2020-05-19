using Client.Forms;
using Client.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class frmLogin : Form
    {
        const int SERVER_PORT = 3333;

        private Socket _clientSocket;

        public frmLogin()
        {
            InitializeComponent();

            ButtonStyle loginButton = new ButtonStyle();
            loginButton.Location = new Point(242, 453);
            loginButton.Size = new Size(128, 44);
            loginButton.Text = "Se connecter";
            loginButton.Click += new EventHandler(this.LoginButtonClicked);
            this.Controls.Add(loginButton);
        }

        private void lblAccountCreation_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            lblAccountCreation.ForeColor = Color.Black; 
        }

        private void lblAccountCreation_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            lblAccountCreation.ForeColor = Color.Tan;
        }      

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            lblUsername.Text = "";
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtUsername.Text))
            {
                lblUsername.Text = "Nom d'utilisateur";
            }   
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            lblPassword.Text = "";
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtPassword.Text))
            {
                lblPassword.Text = "Mot de passe";
            }    
        }

        private void lblAccountCreation_Click(object sender, EventArgs e)
        {
            this.Close();
            frmAccountCreation accountCreation = new frmAccountCreation();
            accountCreation.Show();
        }

        private void LoginButtonClicked(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(txtUsername.Text) && !String.IsNullOrEmpty(txtPassword.Text))
            {
                try
                {
                    _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    _clientSocket.BeginConnect(new IPEndPoint(IPAddress.Loopback, SERVER_PORT), new AsyncCallback(ConnectCallback), null);

                    byte[] buffer = Encoding.ASCII.GetBytes("Login;" + txtUsername.Text + ";" + txtPassword.Text);
                    _clientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);

                    this.Close();

                    frmChat chat = new frmChat();
                    chat.Show();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } 
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
        }

        private void ConnectCallback(IAsyncResult asyncResult)
        {
            try
            {
                _clientSocket.EndConnect(asyncResult);
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendCallback(IAsyncResult asyncResult)
        {
            _clientSocket.EndSend(asyncResult);
        }
    }
}
