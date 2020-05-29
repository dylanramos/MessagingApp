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
     
            LoadContacts();  
        }

        /// <summary>
        /// To spilt the message if it is too long
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        static IEnumerable<string> SplitMessage(string message)
        {
            int chunkSize = 30;

            return Enumerable.Range(0, message.Length / chunkSize).Select(i => message.Substring(i * chunkSize, chunkSize));
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
        /// When we hover the button with the mouse its style changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ptbReload_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Reloads the data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ptbReload_Click(object sender, EventArgs e)
        {     
            LoadMessages(); // Reloads the messages
        }

        /// <summary>
        /// Starts sending the data to the remote server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ptbSend_Click(object sender, EventArgs e)
        {
            char[] deniedChars = { ';', '/', '"', '(', ')', '=', ',', '\'', '\\' };

            if (!String.IsNullOrEmpty(rtbMessage.Text))
            {
                if (rtbMessage.Text.IndexOfAny(deniedChars) == -1)
                {
                    StartConnection();

                    string date = DateTime.Now.ToString("dd MMMM yyyy H:mm");

                    _buffer = Encoding.ASCII.GetBytes("Message;" + _connectedUser + ";" + _selectedUser + ";" + rtbMessage.Text + ";" + date + ";");

                    try
                    {
                        _socket.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Le serveur distant est inaccessible.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Les caractères ; / \" ( ) = , ' \\ sont interdits.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            rtbMessage.Clear();
        }

        /// <summary>
        /// Starts sending the data to the remote server to get the contacts
        /// </summary>
        private void LoadContacts()
        {
            StartConnection();

            _buffer = Encoding.ASCII.GetBytes("Contacts;" + _connectedUser + ";");

            try
            {
                _socket.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
            }
            catch (Exception)
            {
                MessageBox.Show("Le serveur distant est inaccessible.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        /// <summary>
        /// Starts sending the data to the remote server to get the messages
        /// </summary>
        private void LoadMessages()
        {
            StartConnection();

            _buffer = Encoding.ASCII.GetBytes("Messages;" + _connectedUser + ";" + _selectedUser + ";");

            try
            {
                _socket.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
            }
            catch (Exception)
            {
                MessageBox.Show("Le serveur distant est inaccessible.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
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
                Application.Exit();
            }
        }

        /// <summary>
        /// Ends the connection request
        /// </summary>
        /// <param name="asyncResult"></param>
        private void ConnectCallback(IAsyncResult asyncResult)
        {
            try
            {
                // Ends the connection request
                _socket.EndConnect(asyncResult);
            }
            catch (Exception)
            {
                MessageBox.Show("Le serveur distant est inaccessible.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        /// <summary>
        /// Ends the send request and starts receiving server data
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
                case "Logout":

                    Application.Exit();

                    break;

                case "Contacts":

                    int x = 0;
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
                            

                            if (pnlOnlineContacts.InvokeRequired)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    pnlOnlineContacts.Controls.Add(button);

                                    x = (pnlOnlineContacts.Width / 2) - (button.Width / 2);
                                    // Adds the button to the online panel
                                    button.Location = new Point(x, onlinePanelY);
                                });
                            }
                            else
                            {
                                pnlOnlineContacts.Controls.Add(button);
                                x = (pnlOnlineContacts.Width / 2) - (button.Width / 2);
                                // Adds the button to the online panel
                                button.Location = new Point(x, onlinePanelY);
                            }

                            onlinePanelY += 50;
                        }
                        else
                        {
                            

                            if (pnlOfflineContacts.InvokeRequired)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    pnlOfflineContacts.Controls.Add(button);

                                    x = (pnlOfflineContacts.Width / 2) - (button.Width / 2);
                                    // Adds the button to the offline panel
                                    button.Location = new Point(x, offlinePanelY);
                                });
                            }
                            else
                            {
                                pnlOfflineContacts.Controls.Add(button);
                                x = (pnlOfflineContacts.Width / 2) - (button.Width / 2);
                                // Adds the button to the offline panel
                                button.Location = new Point(x, offlinePanelY);
                            }

                            offlinePanelY += 50;
                        }
                    }

                    break;

                case "Messages":

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

                    int yPosition = 0;

                    for (int i = 1; i < words.Length - 1; i++)
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

                        // If the message is too long we format it
                        if(message[0].Length > 30)
                        {
                            string newMessage = "";

                            IEnumerable<string> spiltedMessage = SplitMessage(message[0]);

                            foreach (string s in spiltedMessage)
                            {
                                newMessage += s + "-\n";
                            }

                            newMessage = newMessage.Remove(newMessage.Length - 2); // Deletes the last -
                            messageLabel.Text = newMessage;
                        }
                        else
                        {
                            messageLabel.Text = message[0];
                        }                   

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
                                    yPosition += fromLabel.Height;
                                    messageLabel.Location = new Point(pnlChat.Width - messageLabel.Width - 38, yPosition);
                                    yPosition += messageLabel.Height;
                                    dateLabel.Location = new Point(pnlChat.Width - dateLabel.Width - 38, yPosition);
                                    yPosition += dateLabel.Height + 10;
                                });
                            }
                            else
                            {

                                fromLabel.Text = "Moi";

                                fromLabel.Location = new Point(pnlChat.Width - fromLabel.Width - 38, yPosition);
                                yPosition += fromLabel.Height;
                                messageLabel.Location = new Point(pnlChat.Width - messageLabel.Width - 38, yPosition);
                                yPosition += messageLabel.Height;
                                dateLabel.Location = new Point(pnlChat.Width - dateLabel.Width - 38, yPosition);
                                yPosition += dateLabel.Height + 10;
                            }
                        }
                        else
                        {
                            // The receiver messages are on the left
                            if (pnlChat.InvokeRequired)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    fromLabel.Text = _selectedUser;

                                    fromLabel.Location = new Point(0, yPosition);
                                    yPosition += fromLabel.Height;
                                    messageLabel.Location = new Point(0, yPosition);
                                    yPosition += messageLabel.Height;
                                    dateLabel.Location = new Point(0, yPosition);
                                    yPosition += dateLabel.Height + 10;
                                });
                            }
                            else
                            {
                                fromLabel.Text = _selectedUser;

                                fromLabel.Location = new Point(0, yPosition);
                                yPosition += fromLabel.Height;
                                messageLabel.Location = new Point(0, yPosition);
                                yPosition += messageLabel.Height;
                                dateLabel.Location = new Point(0, yPosition);
                                yPosition += dateLabel.Height + 10;
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
            pnlChat.Visible = true;
            rtbMessage.Visible = true;
            ptbSend.Visible = true;
            ptbReload.Visible = true;

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

            LoadMessages(); // Reloads the messages
        }      

        /// <summary>
        /// When the form is closed, we send to the server that the user has to be disconnected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            StartConnection();

            _buffer = Encoding.ASCII.GetBytes("Logout;" + _connectedUser + ";");

            try
            {
                _socket.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
            }
            catch (Exception)
            {
                MessageBox.Show("Le serveur distant est inaccessible.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }  
    }
}
