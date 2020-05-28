using Client.Views;
using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class frmAccountCreation : Form
    {
        private const string SERVER_IP = "127.0.0.1";
        private const int SERVER_PORT = 3333;

        private bool _formChanged = false; // To check if the form has been closed

        private Socket _socket; // Client socket
        private byte[] _buffer; // To send and receive data

        public frmAccountCreation()
        {
            InitializeComponent();

            // Create account button
            ButtonStyle createButton = new ButtonStyle();
            createButton.Location = new Point(242, 483);
            createButton.Size = new Size(128, 44);
            createButton.Text = "Créer";
            createButton.TabIndex = 3;
            createButton.Click += new EventHandler(this.CreateButtonClicked);


            // Cancel button
            ButtonStyle cancelButton = new ButtonStyle();
            cancelButton.Location = new Point(50, 483);
            cancelButton.Size = new Size(128, 44);
            cancelButton.Text = "Annuler";
            cancelButton.TabIndex = 4;
            cancelButton.Click += new EventHandler(this.CancelButtonClicked);

            this.Controls.Add(createButton);
            this.Controls.Add(cancelButton);
            this.AcceptButton = createButton;
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
        /// When we are writing, the label disappears
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPasswordVerification_TextChanged(object sender, EventArgs e)
        {
            lblPasswordVerification.Text = "";
        }

        /// <summary>
        /// If the input is empty, the label appears
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPasswordVerification_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtPasswordVerification.Text))
            {
                lblPasswordVerification.Text = "Vérification du mot de passe";
            }
        }

        /// <summary>
        /// Comes back to the login form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButtonClicked(object sender, EventArgs e)
        {
            _formChanged = true;

            this.Close();
            frmLogin login = new frmLogin();
            login.Show();
        }

        /// <summary>
        /// Starts the connection with the remote server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateButtonClicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtUsername.Text) && !String.IsNullOrEmpty(txtPassword.Text) && !String.IsNullOrEmpty(txtPasswordVerification.Text))
            {
                if (txtPassword.Text == txtPasswordVerification.Text)
                {
                    try
                    {
                        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        _socket.BeginConnect(new IPEndPoint(IPAddress.Parse(SERVER_IP), SERVER_PORT), new AsyncCallback(ConnectCallback), null);
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

        /// <summary>
        /// Ends the connection request and starts sending data to the remote server
        /// </summary>
        /// <param name="asyncResult"></param>
        private void ConnectCallback(IAsyncResult asyncResult)
        {
            // Ends the connection request
            _socket.EndConnect(asyncResult);

            // Data to send
            _buffer = Encoding.ASCII.GetBytes("AccountCreation;" + txtUsername.Text + ";" + txtPassword.Text + ";");

            // Starts sending the data to the remote server
            _socket.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
        }

        /// <summary>
        /// Ends the send request and starts receiving server data
        /// </summary>
        /// <param name="asyncResult"></param>
        private void SendCallback(IAsyncResult asyncResult)
        {
            // Ends the send request
            _socket.EndSend(asyncResult);

            // Data to receive
            _buffer = new byte[_socket.ReceiveBufferSize];

            // Starts receiving the data
            _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
        }

        /// <summary>
        /// Ends the data receiving and starts processing with the application
        /// </summary>
        /// <param name="asyncResult"></param>
        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            // Ends the data receiving
            _socket.EndReceive(asyncResult);

            string data = Encoding.ASCII.GetString(_buffer);
            string[] words = data.Split(';');
            string request = words[0];

            switch (request)
            {
                case "AccountCreationOk":

                    MessageBox.Show("Le compte a bien été créé.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _formChanged = true;

                    if (this.InvokeRequired)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            this.Close();
                            frmLogin frmLogin = new frmLogin();
                            frmLogin.Show();
                        });
                    }
                    else
                    {
                        this.Close();
                        frmLogin frmLogin = new frmLogin();
                        frmLogin.Show();
                    }

                    break;

                case "AccountCreationNok":

                    MessageBox.Show("Ce nom d'utilisateur existe déjà.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
            }

            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }


        private void frmAccountCreation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_formChanged)
            {
                Application.Exit();
            }
        }
    }
}
