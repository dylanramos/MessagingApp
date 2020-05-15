using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

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
                "UserId INT PRIMARY KEY AUTOINCREMENT, " +
                "UserName VARCHAR(45), " +
                "UserPassword VARCHAR(45));";

            command.CommandText += "CREATE TABLE Messages (" +
                "MessageId INT PRIMARY KEY AUTOINCREMENT, " +
                "MessageText TEXT(250), " +
                "ReceiverUserId INT, " +
                "UserId INT," +
                "CONSTRAINT FK_UserId FOREIGN KEY(UserId) REFERENCES Users(UserId));";
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

        private void CreateAccount()
        {

        }
    }
}
