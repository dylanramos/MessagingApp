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

        private string _connectedUser, _selectedUser;
        private Socket _socket;
        byte[] _buffer;

        public frmChat(string username)
        {
            InitializeComponent();

            _connectedUser = username;
            _buffer = new byte[1024];

            LoadContacts();
        }

        private void ptbSend_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void ptbSend_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void ptbSend_Click(object sender, EventArgs e)
        {
            try
            {
                StartConnection();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            string date = DateTime.Now.ToString("dd MMMM yyyy H:mm");

            _buffer = Encoding.ASCII.GetBytes("Message;" + _connectedUser + ";" + _selectedUser + ";" + rtbMessage.Text + ";" + date + ";");
            _socket.Send(_buffer);
            MessageBox.Show("message envoyé");
            //_socket.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
        }

        private void StartConnection()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.BeginConnect(new IPEndPoint(IPAddress.Parse(SERVER_IP), SERVER_PORT), new AsyncCallback(ConnectCallback), null);
        }

        private void LoadContacts()
        {
            try
            {
                StartConnection();

                _buffer = Encoding.ASCII.GetBytes("Contacts;" + _connectedUser + ";");
                _socket.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConnectCallback(IAsyncResult asyncResult)
        {
            try
            {
                _socket.EndConnect(asyncResult);
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
                _socket.EndSend(asyncResult);

                _buffer = new byte[_socket.ReceiveBufferSize];

                _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);             
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        

        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            _socket.EndReceive(asyncResult);   

            string data = Encoding.ASCII.GetString(_buffer);
            string[] words = data.Split(';');
            string request = words[0];

            switch (request)
            {
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
                            button.Location = new Point(x, onlinePanelY);

                            if(pnlOnlineContacts.InvokeRequired)
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
                            button.Location = new Point(x, offlinePanelY);

                            if(pnlOfflineContacts.InvokeRequired)
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

                    // If the contact is selected we show the message
                    if (_selectedUser == selectedUser)
                    {
                        for (int i = 2; i < words.Length - 1; i++)
                        {
                            List<string> message = words[i].Split('/').ToList();

                            Panel panel = new Panel();
                            panel.Width = 431;
                            panel.Height = 30;
                            panel.AutoSizeMode = AutoSizeMode.GrowOnly;

                            Label fromLabel = new Label();
                            fromLabel.AutoSize = true;
                            fromLabel.Font = new Font("Microsoft YaHei", 12F, FontStyle.Bold);

                            Label dateLabel = new Label();
                            dateLabel.AutoSize = true;
                            dateLabel.Font = new Font("Microsoft YaHei", 12F, FontStyle.Italic);
                            dateLabel.Text = message[1];

                            Label messageLabel = new Label();
                            messageLabel.AutoSize = true;
                            messageLabel.ForeColor = Color.Tan;
                            messageLabel.Text = message[0];
                            

                            if (message[2] == "Sender")
                            {     
                                fromLabel.Text = "Moi";

                                fromLabel.Location = new Point(panel.Width - fromLabel.Width, 0);
                                messageLabel.Location = new Point(panel.Width - messageLabel.Width, 0);
                                dateLabel.Location = new Point(panel.Width - dateLabel.Width, 0);
                            }
                            else
                            {
                                fromLabel.Text = selectedUser;
                            }

                            
                            panel.Controls.Add(messageLabel);

                            if (flpChat.InvokeRequired)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    flpChat.Controls.Add(fromLabel);
                                    flpChat.Controls.Add(panel);
                                    flpChat.Controls.Add(dateLabel);
                                });
                            }
                            else
                            {
                                flpChat.Controls.Add(fromLabel);
                                flpChat.Controls.Add(panel);
                                flpChat.Controls.Add(dateLabel);
                            }
                        }
                    }                  

                    break;
            }

            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }

        private void ContactSelected(object sender, EventArgs e)
        {
            try
            {
                StartConnection();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            flpChat.Visible = true;
            rtbMessage.Visible = true;
            ptbSend.Visible = true;

            flpChat.Controls.Clear();

            // Enables all the buttons
            foreach(Control control in pnlOnlineContacts.Controls)
            {
                Button button = control as Button;

                if(button != null)
                {
                    button.Enabled = true;
                }                
            }

            foreach(Control control in pnlOfflineContacts.Controls)
            {
                Button button = control as Button;

                if (button != null)
                {
                    button.Enabled = true;
                }
            }

            Button clickedButton = sender as Button;
            clickedButton.Enabled = false;
            _selectedUser = clickedButton.Text;

            _buffer = Encoding.ASCII.GetBytes("Messages;" + _connectedUser + ";" + _selectedUser + ";");
            _socket.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            

            Application.Exit();
        }
    }
}
