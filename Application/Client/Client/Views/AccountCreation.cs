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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class frmAccountCreation : Form
    {
        const string SERVER_IP = "127.0.0.1";
        const int SERVER_PORT = 3333;       

        private Socket _serverSocket;
        byte[] _buffer;

        public frmAccountCreation()
        {
            InitializeComponent();

            ButtonStyle createButton = new ButtonStyle();
            createButton.Location = new Point(242, 483);
            createButton.Size = new Size(128, 44);
            createButton.Text = "Créer";
            createButton.Click += new EventHandler(this.CreateButtonClicked);
            this.Controls.Add(createButton);

            ButtonStyle cancelButton = new ButtonStyle();
            cancelButton.Location = new Point(50, 483);
            cancelButton.Size = new Size(128, 44);
            cancelButton.Text = "Annuler";
            cancelButton.Click += new EventHandler(this.CancelButtonClicked);
            this.Controls.Add(cancelButton);
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            lblUsername.Text = "";
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtUsername.Text))
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
            if (String.IsNullOrEmpty(txtPassword.Text))
            {
                lblPassword.Text = "Mot de passe";
            }
        }

        private void txtPasswordVerification_Enter(object sender, EventArgs e)
        {
            lblPasswordVerification.Text = "";
        }

        private void txtPasswordVerification_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtPasswordVerification.Text))
            {
                lblPasswordVerification.Text = "Mot de passe";
            }
        }

        private void CancelButtonClicked(object sender, EventArgs e)
        {
            this.Close();

            frmLogin login = new frmLogin();
            login.Show();
        }

        private void CreateButtonClicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtUsername.Text) && !String.IsNullOrEmpty(txtPassword.Text) && !String.IsNullOrEmpty(txtPasswordVerification.Text))
            {
                if (txtPassword.Text == txtPasswordVerification.Text)
                {
                    try
                    {
                        _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        _serverSocket.BeginConnect(new IPEndPoint(IPAddress.Parse(SERVER_IP), SERVER_PORT), new AsyncCallback(ConnectCallback), null); 
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Les mots de passe doivent être identiques.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                _serverSocket.EndConnect(asyncResult);

                // Begin sending the data to the remote server
                _buffer = Encoding.ASCII.GetBytes("AccountCreation;" + txtUsername.Text + ";" + txtPassword.Text + ";");

                _serverSocket.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendCallback(IAsyncResult asyncResult)
        {
            _serverSocket.EndSend(asyncResult);

            byte[] buffer = new byte[_serverSocket.ReceiveBufferSize];
            _serverSocket.Receive(buffer);

            string data = Encoding.ASCII.GetString(buffer);
            string[] words = data.Split(';');
            string request = words[0];

            switch (request)
            {
                case "AccountCreationOk":

                    MessageBox.Show("Le compte a bien été créé.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Invoke((MethodInvoker)delegate
                    {
                        this.Close();
                        frmLogin frmLogin = new frmLogin();
                        frmLogin.Show();
                    });

                    break;

                case "AccountCreationNok":

                    MessageBox.Show("Ce nom d'utilisateur existe déjà.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
            }

            _serverSocket.Shutdown(SocketShutdown.Both);
            _serverSocket.Close();
        }
    }
}
