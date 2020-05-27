using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Server.Classes;
using Server.Database;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class frmServer : Form
    {
        const int PORT_NUMBER = 3333;

        private DatabaseConnection _databaseConnection;

        private List<User> _Users;

        private Socket _serverSocket, _clientSocket;
        private byte[] _buffer;

        public frmServer()
        {
            InitializeComponent();

            _Users = new List<User>();

            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT_NUMBER));
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            cmdStart.Enabled = false;
            cmdStop.Enabled = true;

            _databaseConnection = new DatabaseConnection();

            _Users = _databaseConnection.GetUsers();

            
            string dateTime = "[" + DateTime.Now.ToString("dd MMMM yyyy, H:mm:ss") + "] ";
            string log = dateTime + "Le serveur a bien été démarré.";

            AddLog(log);

            ClientsListening();
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            cmdStop.Enabled = false;
            cmdStart.Enabled = true;

            if (_serverSocket.Connected != false)
            {
                _serverSocket.Shutdown(SocketShutdown.Both);
                _serverSocket.Close();
            }

            string dateTime = "[" + DateTime.Now.ToString("dd MMMM yyyy, H:mm:ss") + "] ";
            string log = dateTime + "Le serveur a bien été arrêté.";

            AddLog(log);

            lsvConnectedUsers.Items.Clear();
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

        private void AcceptCallback(IAsyncResult asyncResult)
        {        
            try
            {
                _clientSocket = _serverSocket.EndAccept(asyncResult);        

                _buffer = new byte[_clientSocket.ReceiveBufferSize];
                _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReceiveCallback(IAsyncResult asyncResult)
        {
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
            User realUser = _databaseConnection.GetUserCredentials(usernameToCheck);

            if (realUser != null)
            {
                if (passwordToCheck == realUser.Password)
                {
                    // Removes the user from the disconnected users list and adds him to the connected users list
                    foreach (User user in _Users)
                    {
                        if (user.Username == realUser.Username)
                        {
                            user.Online = true;

                            break;
                        }
                    }

                    // Sending the request to the remote client
                    _buffer = Encoding.ASCII.GetBytes("LoginOk;");
                    _clientSocket.Send(_buffer);
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
        /// Disconnects the user
        /// </summary>
        /// <param name="userToDisconnect"></param>
        private void Logout(string userToDisconnect)
        {
            string dateTime = "[" + DateTime.Now.ToString("dd MMMM yyyy, H:mm:ss") + "] ";
            string log = dateTime + "L'utilisateur " + userToDisconnect + " s'est déconnecté.";   

            foreach (User user in _Users)
            {
                // Removes the disconnected user
                if (user.Username == userToDisconnect)
                {
                    _Users.Remove(user);

                    if(lsvConnectedUsers.InvokeRequired)
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
                    

                    _buffer = Encoding.ASCII.GetBytes("Logout;");
                    _clientSocket.Send(_buffer);

                    break;
                }
            }

            AddLog(log);
        }

        private void AccountCreation(string username, string password)
        {
            string dateTime = "[" + DateTime.Now.ToString("dd MMMM yyyy, H:mm:ss") + "] ";
            string log = dateTime + "Le compte de " + username + " a bien été créé.";
            
            bool userExists;

            userExists = _databaseConnection.CheckUserExists(username);

            if (userExists == true)
            {
                // Sends the request to the remote client
                _buffer = Encoding.ASCII.GetBytes("AccountCreationNok;");
                _clientSocket.Send(_buffer);
            }
            else
            {
                _databaseConnection.CreateAccount(username, password);

                // Adds the new user to the list
                User newUser = new User(username, "", false);
                _Users.Add(newUser);

                AddLog(log);

                // Sends the request to the remote client
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
            string dateTime = "[" + DateTime.Now.ToString("dd MMMM yyyy, H:mm:ss") + "] ";
            string log = dateTime + "L'utilisateur " + senderUser + " s'est connecté";
            AddLog(log);

            string dataToSend = "Contacts;";

            ListViewItem listViewItem = new ListViewItem();
            listViewItem.Text = senderUser;
            listViewItem.SubItems.Add(_clientSocket.LocalEndPoint.ToString());

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

            // Sends the connected users username
            foreach (User user in _Users)
            {
                if (senderUser != user.Username && user.Online == true)
                {
                    dataToSend += user.Username + "/Online;";
                }
            }

            // Sends the disconnected users username
            foreach (User user in _Users)
            {
                if (senderUser != user.Username && user.Online == false)
                {
                    dataToSend += user.Username + "/Offline;";
                }      
            }

            _buffer = Encoding.ASCII.GetBytes(dataToSend);
            _clientSocket.Send(_buffer);
        }

        private void UserMessageSent(string senderUser, string receiverUser, string message, string date)
        {
            // Saves the message in the database
            _databaseConnection.SaveMessage(senderUser, receiverUser, message, date);
        }

        private void GetAllMessages(string senderUser, string receiverUser)
        {
            string dataToSend = "Messages;" + receiverUser + ";";
            dataToSend += _databaseConnection.GetMessages(senderUser, receiverUser);

            _buffer = Encoding.ASCII.GetBytes(dataToSend);
            _clientSocket.Send(_buffer);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }     

        /// <summary>
        /// Adds the log to the other logs list
        /// </summary>
        /// <param name="text"></param>
        private void AddLog(string text)
        {
            if(lstlogs.InvokeRequired)
            {
                // To do the operation from another thread
                Invoke((MethodInvoker)delegate
                {
                    lstlogs.Items.Add(text);
                });
            }
            else
            {
                lstlogs.Items.Add(text);
            }
        }
    }
}
