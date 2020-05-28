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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Forms
{
    public partial class frmChat : Form
    {
        private const string SERVER_IP = "127.0.0.1";
        private const int SERVER_PORT = 3333;

        private string _connectedUser, _selectedUser;
        private Socket _socket; // Client socket
        private byte[] _buffer; // To send and receive data

        public frmChat(string username)
        {
            InitializeComponent();

            _connectedUser = username;

            StartConnection();
            LoadContacts();
        }

        /// <summary>
        /// When we hover the button with the mouse its style changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ptbSend_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// When we leave the button with the mouse its style changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ptbSend_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Starts sending the data to the remote server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ptbSend_Click(object sender, EventArgs e)
        {
            StartConnection();

            string date = DateTime.Now.ToString("dd MMMM yyyy H:mm");

            _buffer = Encoding.ASCII.GetBytes("Message;" + _connectedUser + ";" + _selectedUser + ";" + rtbMessage.Text + ";" + date + ";");
            _socket.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);

            rtbMessage.Clear();
        }

        /// <summary>
        /// Starts the connection with the remote server
        /// </summary>
        private void StartConnection()
        {
            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _socket.BeginConnect(new IPEndPoint(IPAddress.Parse(SERVER_IP), SERVER_PORT), new AsyncCallback(ConnectCallback), null);
            }
            catch (Exception)
            {
                MessageBox.Show("Le serveur distant est inaccessible.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Starts sending the data to the remote server
        /// </summary>
        private void LoadContacts()
        {
            _buffer = Encoding.ASCII.GetBytes("Contacts;" + _connectedUser + ";");
            _socket.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
        }

        /// <summary>
        /// Ends the connection request
        /// </summary>
        /// <param name="asyncResult"></param>
        private void ConnectCallback(IAsyncResult asyncResult)
        {
            // Ends the connection request
            _socket.EndConnect(asyncResult);
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
                case "Logout":

                    Application.Exit();

                    break;

                case "Contacts":

                    int x = 50;
                    int onlinePanelY = 50, offlinePanelY = 50;

                    for (int i = 1; i < words.Length - 1; i++)
                    {
                        List<string> users = words[i].Split('/').ToList();

                        ButtonStyle button = new ButtonStyle();
                        button.Text = users[0];
                        button.AutoSize = true;
                        button.Click += new EventHandler(ContactSelected);

                        if (users[1] == "Online")
                        {
                            // Adds the button to the online panel
                            button.Location = new Point(x, onlinePanelY);

                            if (pnlOnlineContacts.InvokeRequired)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    pnlOnlineContacts.Controls.Add(button);
                                });
                            }
                            else
                            {
                                pnlOnlineContacts.Controls.Add(button);
                            }

                            onlinePanelY += 50;
                        }
                        else
                        {
                            // Adds the button to the offline panel
                            button.Location = new Point(x, offlinePanelY);

                            if (pnlOfflineContacts.InvokeRequired)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    pnlOfflineContacts.Controls.Add(button);
                                });
                            }
                            else
                            {
                                pnlOfflineContacts.Controls.Add(button);
                            }

                            offlinePanelY += 50;
                        }
                    }

                    break;

                case "Messages":

                    string selectedUser = words[1];

                    if (pnlChat.InvokeRequired)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            pnlChat.Controls.Clear();
                        });
                    }
                    else
                    {
                        pnlChat.Controls.Clear();
                    }

                    // If the contact is selected we show the message
                    if (_selectedUser == selectedUser)
                    {
                        int yPosition = 0;

                        for (int i = 2; i < words.Length - 1; i++)
                        {
                            List<string> message = words[i].Split('/').ToList();

                            // From label
                            Label fromLabel = new Label();
                            fromLabel.AutoSize = true;
                            fromLabel.Font = new Font("Microsoft YaHei", 12F, FontStyle.Bold);

                            // Message label
                            Label messageLabel = new Label();
                            messageLabel.AutoSize = true;
                            messageLabel.ForeColor = Color.Tan;
                            messageLabel.Text = message[0];

                            // Date label
                            Label dateLabel = new Label();
                            dateLabel.AutoSize = true;
                            dateLabel.Font = new Font("Microsoft YaHei", 12F, FontStyle.Italic);
                            dateLabel.Text = message[1];

                            if (pnlChat.InvokeRequired)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    pnlChat.Controls.Add(fromLabel);
                                    pnlChat.Controls.Add(messageLabel);
                                    pnlChat.Controls.Add(dateLabel);
                                });
                            }
                            else
                            {
                                pnlChat.Controls.Add(fromLabel);
                                pnlChat.Controls.Add(messageLabel);
                                pnlChat.Controls.Add(dateLabel);
                            }

                            if (message[2] == "Sender")
                            {
                                // The sender messages are on the right
                                if (pnlChat.InvokeRequired)
                                {
                                    Invoke((MethodInvoker)delegate
                                    {
                                        fromLabel.Text = "Moi";

                                        fromLabel.Location = new Point(pnlChat.Width - fromLabel.Width - 38, yPosition);
                                        yPosition += messageLabel.Height;
                                        messageLabel.Location = new Point(pnlChat.Width - messageLabel.Width - 38, yPosition);
                                        yPosition += dateLabel.Height;
                                        dateLabel.Location = new Point(pnlChat.Width - dateLabel.Width - 38, yPosition);
                                        yPosition += fromLabel.Height + 10;
                                    });
                                }
                                else
                                {

                                    fromLabel.Text = "Moi";

                                    fromLabel.Location = new Point(pnlChat.Width - fromLabel.Width - 38, yPosition);
                                    yPosition += messageLabel.Height;
                                    messageLabel.Location = new Point(pnlChat.Width - messageLabel.Width - 38, yPosition);
                                    yPosition += dateLabel.Height;
                                    dateLabel.Location = new Point(pnlChat.Width - dateLabel.Width - 38, yPosition);
                                    yPosition += fromLabel.Height + 10;
                                }
                            }
                            else
                            {
                                // The receiver messages are on the left
                                if (pnlChat.InvokeRequired)
                                {
                                    Invoke((MethodInvoker)delegate
                                    {
                                        fromLabel.Text = selectedUser;

                                        fromLabel.Location = new Point(0, yPosition);
                                        yPosition += messageLabel.Height;
                                        messageLabel.Location = new Point(0, yPosition);
                                        yPosition += dateLabel.Height;
                                        dateLabel.Location = new Point(0, yPosition);
                                        yPosition += fromLabel.Height + 10;
                                    });
                                }
                                else
                                {
                                    fromLabel.Text = selectedUser;

                                    fromLabel.Location = new Point(0, yPosition);
                                    yPosition += messageLabel.Height;
                                    messageLabel.Location = new Point(0, yPosition);
                                    yPosition += dateLabel.Height;
                                    dateLabel.Location = new Point(0, yPosition);
                                    yPosition += fromLabel.Height + 10;
                                }
                            }
                        }

                        // If there are messages to show
                        if (pnlChat.Controls.Count > 0)
                        {
                            // To show the latest messages (puts the scrollbar down)
                            if (pnlChat.InvokeRequired)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    Control control = pnlChat.Controls[pnlChat.Controls.Count - 1];
                                    pnlChat.ScrollControlIntoView(control);
                                });
                            }
                            else
                            {
                                Control control = pnlChat.Controls[pnlChat.Controls.Count - 1];
                                pnlChat.ScrollControlIntoView(control);
                            }
                        }
                    }

                    break;
            }

            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }

        /// <summary>
        /// Shows all the contacts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContactSelected(object sender, EventArgs e)
        {
            StartConnection();

            pnlChat.Visible = true;
            rtbMessage.Visible = true;
            ptbSend.Visible = true;

            if (pnlChat.InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    pnlChat.Controls.Clear();
                });
            }
            else
            {
                pnlChat.Controls.Clear();
            }


            Button clickedButton = sender as Button;
            _selectedUser = clickedButton.Text;

            _buffer = Encoding.ASCII.GetBytes("Messages;" + _connectedUser + ";" + _selectedUser + ";");
            _socket.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
        }

        /// <summary>
        /// When the form is closed, we send to the server that the user has to be disconnected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            StartConnection();

            try
            {
                _buffer = Encoding.ASCII.GetBytes("Logout;" + _connectedUser + ";");
                _socket.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
            }
            catch (Exception)
            {
                Application.Exit();
            }
        }
    }
}
