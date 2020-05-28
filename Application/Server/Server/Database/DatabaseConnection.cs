using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using Server.Classes;

namespace Server.Database
{
    /// <summary>
    /// Contains the methods and the attributes to connect and deal with the database.
    /// </summary>
    class DatabaseConnection
    {
        // Connection to the database
        private SQLiteConnection _dbConnection;
        private SQLiteCommand _command;
        private SQLiteDataReader _reader;

        /// <summary>
        /// Database connection constructor
        /// </summary>
        public DatabaseConnection()
        {
            if (!File.Exists("MessagingApp.sqlite"))
            {
                SQLiteConnection.CreateFile("MessagingApp.sqlite");

                this.Initiate();

                this.CreateTables();
            }
            else
            {
                this.Initiate();
            }
        }

        /// <summary>
        /// Connects to the database
        /// </summary>
        private void Initiate()
        {
            _dbConnection = new SQLiteConnection("Data Source=MessagingApp.sqlite;Version=3;");
            _dbConnection.Open();
        }

        /// <summary>
        /// Creates the users table and the messages table
        /// </summary>
        private void CreateTables()
        {
            _command = new SQLiteCommand(_dbConnection);

            _command.CommandText = "CREATE TABLE Users (" +
                "UserId INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "UserName VARCHAR(45), " +
                "UserPassword VARCHAR(100));";

            _command.CommandText += "CREATE TABLE Messages (" +
                "MessageId INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "MessageText TEXT(250), " +
                "MessageDate VARCHAR(20), " +
                "ReceiverUserId INT, " +
                "UserId INT," +
                "CONSTRAINT FK_UserId FOREIGN KEY(UserId) REFERENCES Users(UserId));";

            _command.ExecuteNonQuery();
        }

        /// <summary>
        /// Gets the requested data from the database
        /// </summary>
        /// <param name="sqlRequest"></param>
        /// <returns></returns>
        private SQLiteDataReader ExecuteQuery(string sqlRequest)
        {
            _command = new SQLiteCommand(sqlRequest, _dbConnection);
            return _command.ExecuteReader();
        }

        /// <summary>
        /// Creates a new user in the database
        /// </summary>
        /// <param name="user"></param>
        public void CreateAccount(string username, string password)
        {
            ExecuteQuery("INSERT INTO Users(UserName, UserPassword) VALUES('" + username + "', '" + password + "')");
        }

        /// <summary>
        /// Checks if the user already exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool CheckUserExists(string username)
        {
            bool exists = false;

            _reader = ExecuteQuery("SELECT UserName AS username FROM Users WHERE UserName = '" + username + "'");

            while (_reader.Read())
            {
                if (_reader["username"].ToString() != "")
                {
                    exists = true;
                }
            }

            return exists;
        }

        /// <summary>
        /// Gets the user credentials for the login
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUserCredentials(string username)
        {
            User verifiedUser = null;
            _reader = ExecuteQuery("SELECT UserName, UserPassword FROM Users WHERE UserName = '" + username + "'");

            while (_reader.Read())
            {
                verifiedUser = new User(_reader["UserName"].ToString(), _reader["UserPassword"].ToString(), false);
            }

            return verifiedUser;
        }

        /// <summary>
        /// Gets all the usernames
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            _reader = ExecuteQuery("SELECT UserName FROM Users");

            while (_reader.Read())
            {
                if (_reader["UserName"].ToString() != "")
                {
                    User user = new User(_reader["UserName"].ToString(), "", false);
                    users.Add(user);
                }
            }

            return users;
        }

        /// <summary>
        /// Saves the sent message
        /// </summary>
        /// <param name="senderUser"></param>
        /// <param name="receiverUser"></param>
        /// <param name="message"></param>
        /// <param name="date"></param>
        public void SaveMessage(string senderUser, string receiverUser, string message, string date)
        {
            int senderUserId = 0, receiverUserId = 0;

            // Gets the sender user id
            _reader = ExecuteQuery("SELECT UserId FROM Users WHERE UserName = '" + senderUser + "'");

            while (_reader.Read())
            {
                senderUserId = int.Parse(_reader["UserId"].ToString());
            }

            // Gets the receiver user id
            _reader = ExecuteQuery("SELECT UserId FROM Users WHERE UserName = '" + receiverUser + "'");

            while (_reader.Read())
            {
                receiverUserId = int.Parse(_reader["UserId"].ToString());
            }

            ExecuteQuery("INSERT INTO Messages(MessageText, MessageDate, ReceiverUserId, UserId) VALUES('" + message + "', '" + date + "', " + receiverUserId + ", " + senderUserId + ")");
        }

        /// <summary>
        /// Gets all the messages from two users
        /// </summary>
        /// <param name="senderUser"></param>
        /// <param name="receiverUser"></param>
        /// <returns></returns>
        public string GetMessages(string senderUser, string receiverUser)
        {
            string messages = "";

            int senderUserId = 0, receiverUserId = 0;

            // Gets the sender user id
            _reader = ExecuteQuery("SELECT UserId FROM Users WHERE UserName = '" + senderUser + "'");

            while (_reader.Read())
            {
                senderUserId = int.Parse(_reader["UserId"].ToString());
            }

            // Gets the receiver user id
            _reader = ExecuteQuery("SELECT UserId FROM Users WHERE UserName = '" + receiverUser + "'");

            while (_reader.Read())
            {
                receiverUserId = int.Parse(_reader["UserId"].ToString());
            }

            // Gets the messages
            _reader = ExecuteQuery("SELECT UserId, MessageText, MessageDate FROM Messages WHERE UserId IN (" + senderUserId + ", " + receiverUserId + ") AND ReceiverUserId IN (" + senderUserId + ", " + receiverUserId + ")");

            while (_reader.Read())
            {
                if (int.Parse(_reader["UserId"].ToString()) == senderUserId)
                {
                    messages += _reader["MessageText"].ToString() + "/" + _reader["MessageDate"].ToString() + "/Sender;";
                }
                else
                {
                    messages += _reader["MessageText"].ToString() + "/" + _reader["MessageDate"].ToString() + "/Receiver;";
                }
            }

            return messages;
        }
    }
}
