﻿using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace VOTE.Model
{
    internal class Dbconn
    {
        const string connectionString = "Server=localhost;Database=voting_app;Uid=root;Pwd=;";
        static int userId;

        public int InsertINtoUsers(string email, string password, string role)
        {
            string query = "INSERT INTO users (Email, Password, Role) VALUES (@Email, @Password, @Role); SELECT LAST_INSERT_ID();";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Role", role);
                connection.Open();

                // Execute the command and retrieve the last inserted ID
                userId = Convert.ToInt32(command.ExecuteScalar());
                return userId; // Return the UserID
            }
        }
        

        public void InsertINtoVoters(string firstName, string lastName, string location, string NID)
        {
            string query = "INSERT INTO voters (VoterFName, VoterLName, VoterLocation, IDNum, UserID) VALUES (@VoterFName, @VoterLName, @VoterLocation, @IDNum, @UserID);";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@VoterFName", firstName);
                command.Parameters.AddWithValue("@VoterLName", lastName);
                command.Parameters.AddWithValue("@VoterLocation", location);
                command.Parameters.AddWithValue("@IDNum", NID);
                command.Parameters.AddWithValue("@UserID", userId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }

}