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

namespace Client.Forms
{
    public partial class frmChat : Form
    {
        const string SERVER_IP = "127.0.0.1";
        const int SERVER_PORT = 3333;

        private Socket _serverSocket;
        byte[] _buffer;

        public frmChat(string username)
        {
            InitializeComponent();

            this.ConnectedUser = username;

            try
            {
                _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _serverSocket.BeginConnect(new IPEndPoint(IPAddress.Parse(SERVER_IP), SERVER_PORT), new AsyncCallback(ConnectCallback), null);

                _buffer = Encoding.ASCII.GetBytes("Contacts;" + this.ConnectedUser + ";");
                _serverSocket.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ConnectedUser
        {
            get;
            set;
        }

        private void ConnectCallback(IAsyncResult asyncResult)
        {
            try
            {
                _serverSocket.EndConnect(asyncResult);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void SendCallback(IAsyncResult asyncResult)
        {
            try
            {
                _serverSocket.EndSend(asyncResult);

                byte[] buffer = new byte[_serverSocket.ReceiveBufferSize];
                _serverSocket.Receive(buffer);
                string data = Encoding.ASCII.GetString(buffer);
                string[] words = data.Split(';');

                int x = 50;
                int onlinePanelY = 50, offlinePanelY = 50;

                for (int i = 0; i < words.Length - 1; i++)
                {
                    List<string> users = words[i].Split('/').ToList();

                    ButtonStyle button = new ButtonStyle();
                    button.Text = users[0];
                    button.AutoSize = true;                   

                    if (users[1] == "Online")
                    {
                        button.Location = new Point(x, onlinePanelY);

                        Invoke((MethodInvoker)delegate
                        {
                            pnlOnlineContacts.Controls.Add(button);
                        });

                        onlinePanelY += 50;
                    }
                    else
                    {
                        button.Location = new Point(x, offlinePanelY);

                        Invoke((MethodInvoker)delegate
                        {
                            pnlOfflineContacts.Controls.Add(button);
                        });

                        offlinePanelY += 50;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
