using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.Data.SqlClient;//

namespace ELibraryDataLogic
{
    internal class PenFinderDB : IFinderDataService
    {
        static string connectionString = "Data Source =DESKTOP-M4OSVQ7; Initial Catalog = PFinder; Integrated Security = True; TrustServerCertificate=True;";
        static SqlConnection sqlConnection;

        public PenFinderDB()
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        public bool AddFavorite(string userName, string book)
        {
            throw new NotImplementedException();
        }

        public void CreateAccount(UserAccount newUser)
        {
            var insertStatement = "INSERT INTO UserAccounts VALUES (@UserName, @Password, @Age)";

            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@UserName", newUser.UserName);
            insertCommand.Parameters.AddWithValue("@Password", newUser.Password);
            insertCommand.Parameters.AddWithValue("@Age", newUser.Age);

            sqlConnection.Open();

            insertCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public List<UserAccount> GetAccounts()
        {
            throw new NotImplementedException();
        }

        public List<string> GetFavorites(string userName)
        {
            throw new NotImplementedException();
        }

        public UserAccount GetUser(string userName)
        {
            throw new NotImplementedException();
        }

        public bool IsUserAlreadyRegistered(string userName)
        {
            throw new NotImplementedException();
        }

        public bool RegisterAccount(string userName, string password, int age)
        {
            throw new NotImplementedException();
        }

        public void RemoveAccount(string userName)
        {
            throw new NotImplementedException();
        }

        public bool RemoveFavorite(string userName, string book)
        {
            throw new NotImplementedException();
        }

        public void UpdateAccount(UserAccount updatedUser)
        {
            throw new NotImplementedException();
        }

        public bool ValidateAccount(string userName, string password, int age)
        {
            throw new NotImplementedException();
        }
    }
}
