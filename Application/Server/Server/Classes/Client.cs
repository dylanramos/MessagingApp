using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server.Classes
{
    class Client
    {
        private Socket _socket;

        public Client(Socket acceptedSocket)
        {
            _socket = acceptedSocket;
            Id = Guid.NewGuid().ToString();
            EndPoint = (IPEndPoint)_socket.RemoteEndPoint;
            _socket.BeginReceive(new byte[] { 0 }, 0, 0, 0, Callback, null);
        }

        public string Id
        {
            get;
            private set;
        }

        public IPEndPoint EndPoint
        {
            get;
            private set;
        }

        private void Callback(IAsyncResult asyncResult)
        {
            try
            {
                _socket.EndReceive(asyncResult);

                byte[] buffer = new byte[_socket.ReceiveBufferSize];

                int receivedData = _socket.Receive(buffer, buffer.Length, 0);

                if(Received != null)
                {
                    Received(this, buffer);
                }

                _socket.BeginReceive(new byte[] { 0 }, 0, 0, 0, Callback, null);
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();

                if(Disconnected != null)
                {
                    Disconnected(this);
                }
            }
        }

        public void Close()
        {
            _socket.Close();
            _socket.Dispose();
        }

        public delegate void ClientReceivedHandler(Client sender, byte[] data);
        public delegate void ClientDisconnectedHandler(Client sender);

        public event ClientReceivedHandler Received;
        public event ClientDisconnectedHandler Disconnected;
    }
}
