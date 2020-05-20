using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Classes
{
    class User
    {
        private string _username;
        private string _password;
        private bool _online;

        /// <summary>
        /// User constructor
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public User(string username, string password, bool online)
        {
            _username = username;
            _password = password;
            _online = online;
        }

        /// <summary>
        /// Gets the username
        /// </summary>
        public string Username
        {
            get { return _username; }
        }

        /// <summary>
        /// Gets the password
        /// </summary>
        public string Password
        {
            get { return _password; }
        }

        /// <summary>
        /// To know if the user is online or not
        /// </summary>
        public bool IsOnline
        {
            get { return _online; }
            set { _online = value; }
        }
    }
}
