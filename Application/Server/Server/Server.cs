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

        private Socket _serverSocket, _clientSocket;
        private byte[] _buffer;

        private delegate void log(string text); // To add a log from a different thread

        public frmServer()
        {
            InitializeComponent();
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            lstlogs.Items.Add("Le serveur a bien été démarré.");

            ClientsListening();
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
            string log = "", request;

            try
            {       
                string data = Encoding.ASCII.GetString(_buffer);
                string[] words = data.Split(';');

                request = words[0];

                switch(request)
                {
                    case "Login":
                        string username = words[1];
                        //CHECK BDD
                        log = username + " c'est connecté avec l'adresse IP " + _clientSocket.RemoteEndPoint.ToString();
                        break;
                }

                if (lstlogs.InvokeRequired)
                {
                    lstlogs.Invoke(new log(AddLog), log);
                }
                else
                {
                    AddLog(log);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _serverSocket.Close();
            ClientsListening();
        }

        private void AddLog(string text)
        {
            lstlogs.Items.Add(text);
        }
    }
}
