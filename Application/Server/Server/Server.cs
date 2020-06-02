using Server.Classes;
using Server.Database;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    public partial class frmServer : Form
    {
        private const int PORT_NUMBER = 3333; // Server port

        private DatabaseConnection _databaseConnection; // Database connection

        private List<User> _users;

        private Socket _serverSocket, _clientSocket;
        private byte[] _buffer; // To send and receive data

        public frmServer()
        {
            InitializeComponent();

            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT_NUMBER));
        }

        /// <summary>
        /// Starts the server and stores all the users
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdStart_Click(object sender, EventArgs e)
        {
            cmdStart.Enabled = false;
            cmdStop.Enabled = true;

            _databaseConnection = new DatabaseConnection();
            _users = new List<User>();

            _users = _databaseConnection.GetUsers();

            string dateTime = "[" + DateTime.Now.ToString("dd MMMM yyyy, H:mm:ss") + "] ";
            string log = dateTime + "Le serveur a bien été démarré.";

            AddLog(log);

            ClientsListening();
        }

        /// <summary>
        /// Stops the server and disconnects all the clients
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdStop_Click(object sender, EventArgs e)
        {
            cmdStop.Enabled = false;
            cmdStart.Enabled = true;

            if (_serverSocket.Connected)
            {
                _clientSocket.Shutdown(SocketShutdown.Both);
                _clientSocket.Close();

                _serverSocket.Shutdown(SocketShutdown.Both);
                _serverSocket.Close();
            }

            string dateTime = "[" + DateTime.Now.ToString("dd MMMM yyyy, H:mm:ss") + "] ";
            string log = dateTime + "Le serveur a bien été arrêté.";

            AddLog(log);

            lsvConnectedUsers.Items.Clear();
            _users.Clear();
        }

        /// <summary>
        /// Starts to listen to clients
        /// </summary>
        private void ClientsListening()
        {
            try
            {
                _serverSocket.Listen(0);
                _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Accepts the remote client connection and begins receiving the data
        /// </summary>
        /// <param name="asyncResult"></param>
        private void AcceptCallback(IAsyncResult asyncResult)
        {
            try
            {
                // Ends the connection request
                _clientSocket = _serverSocket.EndAccept(asyncResult);

                _buffer = new byte[_clientSocket.ReceiveBufferSize];
                _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Processes with the remote client response
        /// </summary>
        /// <param name="asyncResult"></param>
        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            // Ends the data receiving
            _clientSocket.EndReceive(asyncResult);

            string request;

            try
            {
                string data = Encoding.ASCII.GetString(_buffer);
                string[] words = data.Split(';');

                request = words[0];

                switch (request)
                {
                    case "Login":

                        Login(words[1], words[2]);

                        break;

                    case "Logout":

                        Logout(words[1]);

                        break;

                    case "AccountCreation":

                        AccountCreation(words[1], words[2]);

                        break;

                    case "Contacts":

                        GetContacts(words[1]);

                        break;

                    case "Message":

                        UserMessageSent(words[1], words[2], words[3], words[4]);
                        GetAllMessages(words[1], words[2]);

                        break;

                    case "Messages":

                        GetAllMessages(words[1], words[2]);

                        break;
                }

                ClientsListening();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Connects the user
        /// </summary>
        /// <param name="usernameToCheck"></param>
        /// <param name="passwordToCheck"></param>
        private void Login(string usernameToCheck, string passwordToCheck)
        {
            string dateTime = "[" + DateTime.Now.ToString("dd MMMM yyyy, H:mm:ss") + "] ";
            string log = dateTime + "L'utilisateur " + usernameToCheck + " s'est connecté";

            // Password hash
            var sha1 = new SHA1CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(passwordToCheck);
            var hashedPassword = sha1.ComputeHash(data);
            string password = Encoding.ASCII.GetString(hashedPassword);

            User realUser = _databaseConnection.GetUserCredentials(usernameToCheck);     

            if (realUser != null)
            {
                if (password == realUser.Password)
                {
                    // Sets the user online
                    foreach (User user in _users)
                    {
                        if (user.Username == realUser.Username)
                        {
                            if (user.Online)
                            {
                                // Sends to the client that the credentials are wrong
                                _buffer = Encoding.ASCII.GetBytes("UserConnected;");
                                _clientSocket.Send(_buffer);
                            }
                            else
                            {
                                user.Online = true;

                                ListViewItem listViewItem = new ListViewItem();
                                listViewItem.Text = realUser.Username;
                                listViewItem.SubItems.Add(_clientSocket.LocalEndPoint.ToString());

                                // Adds the connected user to the connected users list
                                if (lsvConnectedUsers.InvokeRequired)
                                {
                                    Invoke((MethodInvoker)delegate
                                    {
                                        lsvConnectedUsers.Items.Add(listViewItem);
                                    });
                                }
                                else
                                {
                                    lsvConnectedUsers.Items.Add(listViewItem);
                                }

                                AddLog(log);

                                // Sends to the client that the credentials are right
                                _buffer = Encoding.ASCII.GetBytes("LoginOk;");
                                _clientSocket.Send(_buffer);
                            }

                            break;
                        }
                    }
                }
                else
                {
                    // Sends to the client that the credentials are wrong
                    _buffer = Encoding.ASCII.GetBytes("LoginNok;");
                    _clientSocket.Send(_buffer);
                }
            }
            else
            {
                // Sends to the client that the credentials are wrong
                _buffer = Encoding.ASCII.GetBytes("LoginNok;");
                _clientSocket.Send(_buffer);
            }
        }

        /// <summary>
        /// Disconnects the user and updates the connected users list
        /// </summary>
        /// <param name="userToDisconnect"></param>
        private void Logout(string userToDisconnect)
        {
            string dateTime = "[" + DateTime.Now.ToString("dd MMMM yyyy, H:mm:ss") + "] ";
            string log = dateTime + "L'utilisateur " + userToDisconnect + " s'est déconnecté.";

            foreach (User user in _users)
            {
                // Removes the disconnected user
                if (user.Username == userToDisconnect)
                {
                    user.Online = false;

                    // Updates the connected users list
                    if (lsvConnectedUsers.InvokeRequired)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            for (int i = lsvConnectedUsers.Items.Count - 1; i >= 0; i--)
                            {
                                if (lsvConnectedUsers.Items[i].Text == userToDisconnect)
                                {
                                    lsvConnectedUsers.Items[i].Remove();
                                }
                            }
                        });
                    }
                    else
                    {
                        for (int i = lsvConnectedUsers.Items.Count - 1; i >= 0; i--)
                        {
                            if (lsvConnectedUsers.Items[i].Text == userToDisconnect)
                            {
                                lsvConnectedUsers.Items[i].Remove();

                                break;
                            }
                        }
                    }

                    // Sends the data to the remote client
                    _buffer = Encoding.ASCII.GetBytes("Logout;");
                    _clientSocket.Send(_buffer);

                    break;
                }
            }

            AddLog(log);
        }

        /// <summary>
        /// Creates a new user account if it is not existing
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        private void AccountCreation(string username, string password)
        {
            string dateTime = "[" + DateTime.Now.ToString("dd MMMM yyyy, H:mm:ss") + "] ";
            string log = dateTime + "Le compte de " + username + " a bien été créé.";

            bool userExists;

            userExists = _databaseConnection.CheckUserExists(username);

            if (userExists == true)
            {
                // Sends the data to the remote client
                _buffer = Encoding.ASCII.GetBytes("AccountCreationNok;");
                _clientSocket.Send(_buffer);
            }
            else
            {
                // Password hash
                var sha1 = new SHA1CryptoServiceProvider();
                var data = Encoding.ASCII.GetBytes(password);
                var hashedPassword = sha1.ComputeHash(data);

                _databaseConnection.CreateAccount(username, Encoding.ASCII.GetString(hashedPassword));

                // Adds the new user to the list
                User newUser = new User(username, "", false);
                _users.Add(newUser);

                AddLog(log);

                // Sends the data to the remote client
                _buffer = Encoding.ASCII.GetBytes("AccountCreationOk;");
                _clientSocket.Send(_buffer);
            }
        }

        /// <summary>
        /// Sends all the contacts to the remote client
        /// </summary>
        /// <param name="senderUser"></param>
        private void GetContacts(string senderUser)
        {
            string dataToSend = "Contacts;";

            // Connected users username
            foreach (User user in _users)
            {
                if (senderUser != user.Username && user.Online == true)
                {
                    dataToSend += user.Username + "/Online;";
                }
            }

            // Disconnected users username
            foreach (User user in _users)
            {
                if (senderUser != user.Username && user.Online == false)
                {
                    dataToSend += user.Username + "/Offline;";
                }
            }

            // Sends the data to the remote client
            _buffer = Encoding.ASCII.GetBytes(dataToSend);
            _clientSocket.Send(_buffer);
        }

        /// <summary>
        /// Saves the sent message in the database
        /// </summary>
        /// <param name="senderUser"></param>
        /// <param name="receiverUser"></param>
        /// <param name="message"></param>
        /// <param name="date"></param>
        private void UserMessageSent(string senderUser, string receiverUser, string message, string date)
        {
            // Saves the message in the database
            _databaseConnection.SaveMessage(senderUser, receiverUser, message, date);

            string dateTime = "[" + DateTime.Now.ToString("dd MMMM yyyy, H:mm:ss") + "] ";
            string log = dateTime + senderUser + " a envoyé un message à " + receiverUser + ".";

            AddLog(log);
        }

        /// <summary>
        /// Sends all the conversations to the remote client
        /// </summary>
        /// <param name="senderUser"></param>
        /// <param name="receiverUser"></param>
        private void GetAllMessages(string senderUser, string receiverUser)
        {
            string dataToSend = "Messages;";
            dataToSend += _databaseConnection.GetMessages(senderUser, receiverUser);

            // Sends the data to the remote client
            _buffer = Encoding.ASCII.GetBytes(dataToSend);
            _clientSocket.Send(_buffer);
        }

        /// <summary>
        /// Closes the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Adds the log to the other logs list
        /// </summary>
        /// <param name="log"></param>
        private void AddLog(string log)
        {
            if (lstlogs.InvokeRequired)
            {
                // To do the operation from another thread
                Invoke((MethodInvoker)delegate
                {
                    lstlogs.Items.Add(log);
                });
            }
            else
            {
                lstlogs.Items.Add(log);
            }
        }
    }
}
