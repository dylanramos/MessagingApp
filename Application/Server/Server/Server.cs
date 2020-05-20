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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class frmServer : Form
    {
        const int PORT_NUMBER = 3333;

        static Listener _listener;
        static List<Socket> _sockets;

        DatabaseConnection databaseConnection = new DatabaseConnection();

        private Socket _serverSocket, _clientSocket;
        private byte[] _buffer;

        private delegate void log(string text); // To add a log from a different thread

        public frmServer()
        {
            InitializeComponent();
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            string dateTime = "[" + DateTime.Now.ToString("dd MMMM yyyy, H:mm:ss") + "] ";
            string log = dateTime + "Le serveur a bien été démarré.";

            AddLog(log);

            _sockets = new List<Socket>();
            _listener = new Listener(PORT_NUMBER);
            _listener.SocketAccepted += new Listener.SocketAcceptedHandler(Listener_SocketAccepted);
            _listener.Start();

            //ClientsListening();
        }

        private void Listener_SocketAccepted(Socket socket)
        {
            Client client = new Client(socket);
            client.Received += new Client.ClientReceivedHandler(Client_Received);
            client.Disconnected += new Client.ClientDisconnectedHandler(Client_Disconnected);

            Invoke((MethodInvoker)delegate
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Text = client.EndPoint.ToString();
                listViewItem.SubItems.Add(client.Id);
                lsvClients.Items.Add(listViewItem);
            });

            string dateTime = "[" + DateTime.Now.ToString("dd MMMM yyyy, H:mm:ss") + "] ";
            string log = dateTime + socket.RemoteEndPoint + " est connecté";

            if (lstlogs.InvokeRequired)
            {
                lstlogs.Invoke(new log(AddLog), log);
            }
            else
            {
                AddLog(log);
            }

            _sockets.Add(socket);
        }

        private void Client_Received(Client sender, byte[] data)
        {
            
        }

        private void Client_Disconnected(Client sender)
        {
            Invoke((MethodInvoker)delegate
            {
                for (int i = 0; i < lsvClients.Items.Count; i++)
                {
                    Client client = lsvClients.Items[i].Tag as Client;

                    if(client.Id == sender.Id)
                    {
                        lsvClients.Items.RemoveAt(i);
                        break;
                    }
                }
            });
        }

        private void ClientsListening()
        {
            try
            {
                _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT_NUMBER));
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
                string username = words[1];
                string password = words[2];

                string dateTime = "[" + DateTime.Now.ToString("dd MMMM yyyy, H:mm:ss") + "] ";

                switch (request)
                {
                    case "Login":
                        //CHECK BDD
                        log = dateTime + username + " c'est connecté avec l'adresse IP " + _clientSocket.RemoteEndPoint.ToString();
                        break;

                    case "AccountCreation":

                        User user = new User(username, password, true);
                        bool userExists;

                        userExists = databaseConnection.CheckUserExists(user);

                        if (userExists == true)
                        {
                            // Begin sending the data to the remote client
                            _buffer = Encoding.ASCII.GetBytes("NOK");
                            _serverSocket.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
                        }
                        else
                        {
                            databaseConnection.CreateAccount(user);
                            log = dateTime + "Le compte de " + username + " a bien été créé.";

                            // Begin sending the data to the remote client
                            _buffer = Encoding.ASCII.GetBytes("OK");
                            _serverSocket.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
                        }

                        break;
                }

                if (log != "")
                {
                    if (lstlogs.InvokeRequired)
                    {
                        lstlogs.Invoke(new log(AddLog), log);
                    }
                    else
                    {
                        AddLog(log);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //_serverSocket.Close();
            //ClientsListening();
        }

        private void SendCallback(IAsyncResult asyncResult)
        {
            _serverSocket.EndSend(asyncResult);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddLog(string text)
        {
            lstlogs.Items.Add(text);
        }
    }
}
