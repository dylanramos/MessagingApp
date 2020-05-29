using Client.Forms;
using Client.Views;
using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class frmLogin : Form
    {
        private const string SERVER_IP = "127.0.0.1";
        private const int SERVER_PORT = 3333;

        private bool _formChanged = false; // To check if the form has been closed

        private Socket _socket; // Client socket
        private byte[] _buffer; // To send and receive data

        public frmLogin()
        {
            InitializeComponent();

            // Login button
            ButtonStyle loginButton = new ButtonStyle();
            loginButton.Location = new Point(242, 453);
            loginButton.Size = new Size(128, 44);
            loginButton.Text = "Se connecter";
            loginButton.TabIndex = 2;
            loginButton.Click += new EventHandler(this.LoginButtonClicked);

            this.Controls.Add(loginButton);
            this.AcceptButton = loginButton;
        }

        /// <summary>
        /// When we are writing, the label disappears
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            lblUsername.Text = "";
        }

        /// <summary>
        /// If the input is empty, the label appears
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtUsername.Text))
            {
                lblUsername.Text = "Nom d'utilisateur";
            }
        }

        /// <summary>
        /// When we are writing, the label disappears
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            lblPassword.Text = "";
        }

        /// <summary>
        /// If the input is empty, the label appears
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtPassword.Text))
            {
                lblPassword.Text = "Mot de passe";
            }
        }

        /// <summary>
        /// Opens the account creation form and closes the login form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblAccountCreation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _formChanged = true;

            this.Close();
            frmAccountCreation accountCreation = new frmAccountCreation();
            accountCreation.Show();
        }

        /// <summary>
        /// Checks the inserted characters and starts the connection with the remote server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButtonClicked(object sender, EventArgs e)
        {
            char[] deniedChars = { ';', '/', '"', '(', ')', '=', ',', '\'', '\\' };

            if (!String.IsNullOrEmpty(txtUsername.Text) && !String.IsNullOrEmpty(txtPassword.Text))
            {
                if (txtUsername.Text.IndexOfAny(deniedChars) == -1 && txtPassword.Text.IndexOfAny(deniedChars) == -1)
                {
                    if (txtPassword.Text.Length >= 10)
                    {
                        try
                        {
                            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            _socket.BeginConnect(new IPEndPoint(IPAddress.Parse(SERVER_IP), SERVER_PORT), new AsyncCallback(ConnectCallback), null);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Le serveur distant est inaccessible.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Votre mot de passe ne respecte pas la taille minimum de 10 caractères.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Les caractères ; / \" ( ) = , ' \\ sont interdits.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ends the connection request and starts sending the data to the remote server
        /// </summary>
        /// <param name="asyncResult"></param>
        private void ConnectCallback(IAsyncResult asyncResult)
        {
            try
            {
                // Ends the connection request
                _socket.EndConnect(asyncResult);

                // Data to send
                _buffer = Encoding.ASCII.GetBytes("Login;" + txtUsername.Text + ";" + txtPassword.Text + ";");

                // Starts sending the data to the remote server
                _socket.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
            }
            catch (Exception)
            {
                MessageBox.Show("Le serveur distant est inaccessible.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        /// <summary>
        /// Ends the send request and starts receiving the server data
        /// </summary>
        /// <param name="asyncResult"></param>
        private void SendCallback(IAsyncResult asyncResult)
        {
            try
            {
                // Ends the send request
                _socket.EndSend(asyncResult);

                // Data to receive
                _buffer = new byte[_socket.ReceiveBufferSize];

                // Starts receiving the data
                _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            }
            catch (Exception)
            {
                MessageBox.Show("Le serveur distant est inaccessible.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        /// <summary>
        /// Ends the data receiving and starts processing with the application
        /// </summary>
        /// <param name="asyncResult"></param>
        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            try
            {
                // Ends the data receiving
                _socket.EndReceive(asyncResult);
            }
            catch (Exception)
            {
                MessageBox.Show("Le serveur distant est inaccessible.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

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

        /// <summary>
        /// If the page is not changed the application stops
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_formChanged)
            {
                Application.Exit();
            }
        }
    }
}
