using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private SQLiteConnection dbConnection;
        private SQLiteCommand command;
        private SQLiteDataReader reader;

        /// <summary>
        /// DataBaseConnection constructor
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
            dbConnection = new SQLiteConnection("Data Source=MessagingApp.sqlite;Version=3;");
            dbConnection.Open();
        }

        private void CreateTables()
        {
            command = new SQLiteCommand(dbConnection);

            command.CommandText = "CREATE TABLE Users (" +
                "UserId INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "UserName VARCHAR(45), " +
                "UserPassword VARCHAR(100));";

            command.CommandText += "CREATE TABLE Messages (" +
                "MessageId INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "MessageText TEXT(250), " +
                "ReceiverUserId INT, " +
                "UserId INT," +
                "CONSTRAINT FK_UserId FOREIGN KEY(UserId) REFERENCES Users(UserId));";

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Gets the requested data from the database
        /// </summary>
        /// <param name="sqlRequest"></param>
        /// <returns></returns>
        private SQLiteDataReader ExecuteQuery(string sqlRequest)
        {
            command = new SQLiteCommand(sqlRequest, dbConnection);
            return command.ExecuteReader();
        }

        /// <summary>
        /// Creates a new user in the database
        /// </summary>
        /// <param name="user"></param>
        public void CreateAccount(string username, string password)
        {
            ExecuteQuery("INSERT INTO Users(UserName, UserPassword) VALUES('" + username + "', '" + password + "')");
        }

        public bool CheckUserExists(string username)
        {
            bool exists = false;

            reader = ExecuteQuery("SELECT UserName AS username FROM Users WHERE UserName = '" + username + "'");

            while (reader.Read())
            {
                if(reader["username"].ToString() != "")
                {
                    exists = true;
                }
            }

            return exists;
        }

        public User GetUserCredentials(string username)
        {
            User verifiedUser = null;
            reader = ExecuteQuery("SELECT UserName, UserPassword FROM Users WHERE UserName = '" + username + "'");

            while (reader.Read())
            {
                verifiedUser = new User(reader["UserName"].ToString(), reader["UserPassword"].ToString(), false);
            }

            return verifiedUser;
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            reader = ExecuteQuery("SELECT UserName FROM Users");

            while (reader.Read())
            {
                if (reader["UserName"].ToString() != "")
                {
                    User user = new User(reader["UserName"].ToString(), "", false);
                    users.Add(user);
                }
            }

            return users;
        }
    }
}
