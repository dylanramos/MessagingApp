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
    class Listener
    {
        Socket _socket;

        public Listener(int port)
        {
            Port = port;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public int Port
        {
            get;
            private set;
        }

        public bool Listening
        {
            get;
            private set;
        }

        public void Start()
        {
            if(Listening)
            {
                return;
            }

            _socket.Bind(new IPEndPoint(0, Port));
            _socket.Listen(0);

            _socket.BeginAccept(Callback, null);
            Listening = true;
        }

        public void Stop()
        {
            if(!Listening)
            {
                return;
            }

            _socket.Close();
            _socket.Dispose();

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Callback(IAsyncResult asyncResult)
        {
            try
            {
                Socket clientSocket = _socket.EndAccept(asyncResult);

                if(SocketAccepted != null)
                {
                    SocketAccepted(clientSocket);
                }

                _socket.BeginAccept(Callback, null);
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public delegate void SocketAcceptedHandler(Socket socket);
        public event SocketAcceptedHandler SocketAccepted;
    }
}
