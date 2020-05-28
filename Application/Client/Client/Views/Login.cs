using Client.Forms;
using Client.Views;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class frmLogin : Form
    {
        private const string SERVER_IP = "127.0.0.1";
        private const int SERVER_PORT = 3333;

        private bool _formChanged = false;

        private Socket _socket;
        private byte[] _buffer = new byte[1024];

        public frmLogin()
        {
            InitializeComponent();

            ButtonStyle loginButton = new ButtonStyle();
            loginButton.Location = new Point(242, 453);
            loginButton.Size = new Size(128, 44);
            loginButton.Text = "Se connecter";
            loginButton.TabIndex = 2;
            loginButton.Click += new EventHandler(this.LoginButtonClicked);

            this.Controls.Add(loginButton);
            this.AcceptButton = loginButton;
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

        private void txtUsername_TextChanged(object sender, EventArgs e)
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

        private void txtPassword_TextChanged(object sender, EventArgs e)
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
                    _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    _socket.BeginConnect(new IPEndPoint(IPAddress.Parse(SERVER_IP), SERVER_PORT), new AsyncCallback(ConnectCallback), null);

                    _buffer = Encoding.ASCII.GetBytes("Login;" + txtUsername.Text + ";" + txtPassword.Text + ";");
                    _socket.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);         
                }
                catch (Exception)
                {
                    MessageBox.Show("Le serveur distant est inaccessible.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } 
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
        }

        private void ConnectCallback(IAsyncResult asyncResult)
        {
            _socket.EndConnect(asyncResult);
        }

        private void SendCallback(IAsyncResult asyncResult)
        {
            _socket.EndSend(asyncResult);

            _buffer = new byte[_socket.ReceiveBufferSize];
            _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);               
        }

        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            _socket.EndReceive(asyncResult);

            string data = Encoding.ASCII.GetString(_buffer);
            string[] words = data.Split(';');
            string request = words[0];

            switch (request)
            {
                case "LoginOk":

                    _formChanged = true;
                    frmChat chat = new frmChat(txtUsername.Text);

                    if (this.InvokeRequired)
                    {
                        Invoke((MethodInvoker)delegate
                        {        
                            this.Close();
                            chat.Show();
                        });
                    }
                    else
                    {
                        this.Close();
                        chat.Show();
                    } 

                    break;

                case "LoginNok":

                    MessageBox.Show("Les identifiants sont incorrects.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);    

                    break;

                case "UserConnected":

                    MessageBox.Show("Cet utlisateur est déjà connecté.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
            }

            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }

        private void lblAccountCreation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _formChanged = true;

            this.Close();
            frmAccountCreation accountCreation = new frmAccountCreation();
            accountCreation.Show();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!_formChanged)
            {
                Application.Exit();
            }
        }
    }
}
