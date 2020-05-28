namespace Server.Classes
{
    class User
    {
        /// <summary>
        /// User constructor
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public User(string username, string password, bool online)
        {
            Username = username;
            Password = password;
            Online = online;
        }

        /// <summary>
        /// User's username
        /// </summary>
        public string Username
        {
            get;
            private set;
        }

        /// <summary>
        /// User's hashed password
        /// </summary>
        public string Password
        {
            get;
            private set;
        }

        /// <summary>
        /// To know if the user is online or not
        /// </summary>
        public bool Online
        {
            get;
            set;
        }
    }
}
