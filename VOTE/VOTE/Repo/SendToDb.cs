﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VOTE.Model
{
    class SendToDb
    {
        
        static int userId;
        public int InsertINtoUsers(string email, string password, string role)
        {
            string checkEmailQuery = "SELECT COUNT(*) FROM users WHERE Email = @Email";
            string insertQuery = "INSERT INTO users (Email, Password, Role) VALUES (@Email, @Password, @Role); SELECT LAST_INSERT_ID();";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(Dbconn.connectionString))
                {
                    // First, check if the email already exists
                    MySqlCommand checkEmailCommand = new MySqlCommand(checkEmailQuery, connection);
                    checkEmailCommand.Parameters.AddWithValue("@Email", email);
                    connection.Open();

                    int emailCount = Convert.ToInt32(checkEmailCommand.ExecuteScalar());

                    // If email already exists, return a special value or throw an exception
                    if (emailCount > 0)
                    {
                        Console.WriteLine("Email already exists.");
                        return -1; 
                    }

                    // If email does not exist, proceed with the insert
                    MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@Email", email);
                    insertCommand.Parameters.AddWithValue("@Password", password);
                    insertCommand.Parameters.AddWithValue("@Role", role);

                    // Execute the insert and retrieve the last inserted ID
                    userId = Convert.ToInt32(insertCommand.ExecuteScalar());
                    MessageBox.Show(userId.ToString());
                    return userId;                 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting into users: " + ex.Message);
                throw;
            }
        }


        public void InsertINtoVoters(string firstName, string lastName, string location, string NID)
        {
            string query = "INSERT INTO voters (VoterFName, VoterLName, VoterLocation, IDNum, UserID) VALUES (@VoterFName, @VoterLName, @VoterLocation, @IDNum, @UserID);";
            try { 
            using (MySqlConnection connection = new MySqlConnection(Dbconn.connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@VoterFName", firstName);
                command.Parameters.AddWithValue("@VoterLName", lastName);
                command.Parameters.AddWithValue("@VoterLocation", location);
                command.Parameters.AddWithValue("@IDNum", NID);
                command.Parameters.AddWithValue("@UserID", userId);
                connection.Open();
                command.ExecuteNonQuery(); // to insert
            }
        }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting into voters: " + ex.Message);
                throw;
            }
        }
        public void InsertIntoParty(string partyName, string partyAcronym, DateTime? foundedDate,
            string headquartersLocation, string partyLeader, string membershipCriteria, string partyInfo,
            int membershipSize, string electionParticipation, string fundingSources, byte[] legalCertification, int userId)
        {
            string query = "INSERT INTO parties (PartyName, PartyAcronym, FoundedDate, " +
                "HeadquartersLocation, PartyLeader, MembershipCriteria, PartyInfo, MembershipSize, ElectionParticipation, " +
                "FundingSources, LegalCertification, UserID) VALUES (@PartyName, @PartyAcronym, @FoundedDate, @HeadquartersLocation, " +
                "@PartyLeader, @MembershipCriteria, @PartyInfo, @MembershipSize, @ElectionParticipation, @FundingSources, " +
                "@LegalCertification , @UserID);";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Dbconn.connectionString))// automatically closes the connection
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@PartyName", partyName);
                    command.Parameters.AddWithValue("@PartyAcronym", partyAcronym);
                    command.Parameters.AddWithValue("@FoundedDate",foundedDate.HasValue ? foundedDate.Value : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@HeadquartersLocation", headquartersLocation);
                    command.Parameters.AddWithValue("@PartyLeader", partyLeader);
                    command.Parameters.AddWithValue("@MembershipCriteria", membershipCriteria);
                    command.Parameters.AddWithValue("@PartyInfo", partyInfo);
                    command.Parameters.AddWithValue("@MembershipSize", membershipSize);
                    command.Parameters.AddWithValue("@ElectionParticipation", electionParticipation);
                    command.Parameters.AddWithValue("@FundingSources", fundingSources);
                    command.Parameters.AddWithValue("@LegalCertification", legalCertification);
                    command.Parameters.AddWithValue("@UserID", userId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting into parties: " + ex.Message);
                throw;
            }
        }
     


    }
}
