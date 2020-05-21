﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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

        private Socket _serverSocket, _clientSocket;
        private byte[] _buffer;

        private List<User> _users;

        private delegate void log(string text); // To add a log from a different thread

        public frmServer()
        {
            InitializeComponent();
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            _databaseConnection = new DatabaseConnection();
            _users = new List<User>();

            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT_NUMBER));
            string dateTime = "[" + DateTime.Now.ToString("dd MMMM yyyy, H:mm:ss") + "] ";
            string log = dateTime + "Le serveur a bien été démarré.";

            AddLog(log);

            ClientsListening();
        }

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

            string log = "", request;

            try
            {
                string data = Encoding.ASCII.GetString(_buffer);
                string[] words = data.Split(';');

                request = words[0];               

                string dateTime = "[" + DateTime.Now.ToString("dd MMMM yyyy, H:mm:ss") + "] ";

                switch (request)
                {
                    case "Login":

                        string usernameToCheck = words[1];
                        string passwordToCheck = words[2];

                        User realUser = _databaseConnection.GetUserCredentials(usernameToCheck);

                        if(realUser != null)
                        {                      
                            if (passwordToCheck == realUser.Password)
                            {
                                _users.Add(realUser);

                                log = dateTime + "L'utilisateur " + realUser.Username + "est maintenant connecté avec l'addresse IP " + _clientSocket.RemoteEndPoint;
                                lstlogs.Invoke(new log(AddLog), log);

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
                        
                        break;

                    case "AccountCreation":

                        string username = words[1];
                        string password = words[2];
                        bool userExists;

                        userExists = _databaseConnection.CheckUserExists(username);

                        if (userExists == true)
                        {
                            // Sending the request to the remote client
                            _buffer = Encoding.ASCII.GetBytes("AccountCreationNok;");
                            _clientSocket.Send(_buffer);
                        }
                        else
                        {
                            _databaseConnection.CreateAccount(username, password);

                            log = dateTime + "Le compte de " + username + " a bien été créé.";
                            lstlogs.Invoke(new log(AddLog), log);

                            // Sending the request to the remote client
                            _buffer = Encoding.ASCII.GetBytes("AccountCreationOk;");
                            _clientSocket.Send(_buffer);
                        }

                        break;
                }   

                _clientSocket.Shutdown(SocketShutdown.Both);
                _clientSocket.Close();

                ClientsListening();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Adds the log to the oder logs list
        /// </summary>
        /// <param name="text"></param>
        private void AddLog(string text)
        {
            lstlogs.Items.Add(text);
        }
    }
}
