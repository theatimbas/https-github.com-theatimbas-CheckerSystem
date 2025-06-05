using PFinderCommon;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace ELibraryDataLogic
{
    public class PenFinderDB : IFinderDataService
    {
        static string connectionString = "Data Source=DESKTOP-781F9V1\\SQLEXPRESS;Initial Catalog=PenFinderDB;Integrated Security=True;TrustServerCertificate=True;";
        static SqlConnection sqlConnection;

        public PenFinderDB()
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        public void CreateAccount(UserAccount newUser)
        {
            string insertQuery = "INSERT INTO UserAccounts (UserName, Password, Age) VALUES (@UserName, @Password, @Age)";
            using SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection);
            cmd.Parameters.AddWithValue("@UserName", newUser.UserName);
            cmd.Parameters.AddWithValue("@Password", newUser.Password);
            cmd.Parameters.AddWithValue("@Age", newUser.Age);

            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public List<UserAccount> GetAccounts()
        {
            string selectQuery = "SELECT UserName, Password, Age FROM UserAccounts";
            using SqlCommand cmd = new SqlCommand(selectQuery, sqlConnection);
            List<UserAccount> users = new();

            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new UserAccount
                {
                    UserName = reader["UserName"].ToString(),
                    Password = reader["Password"].ToString(),
                    Age = Convert.ToInt32(reader["Age"]),
                    Favorites = GetFavorites(reader["UserName"].ToString())
                });
            }
            sqlConnection.Close();
            return users;
        }

        public void UpdateAccount(UserAccount updatedUser)
        {
            string updateQuery = "UPDATE UserAccounts SET Password = @Password, Age = @Age WHERE UserName = @UserName";
            using SqlCommand cmd = new SqlCommand(updateQuery, sqlConnection);
            cmd.Parameters.AddWithValue("@Password", updatedUser.Password);
            cmd.Parameters.AddWithValue("@Age", updatedUser.Age);
            cmd.Parameters.AddWithValue("@UserName", updatedUser.UserName);

            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void RemoveAccount(string userName)
        {
            string deleteFavorites = "DELETE FROM UserFavorites WHERE UserName = @UserName";
            string deleteUser = "DELETE FROM UserAccounts WHERE UserName = @UserName";

            using SqlCommand favCmd = new SqlCommand(deleteFavorites, sqlConnection);
            using SqlCommand userCmd = new SqlCommand(deleteUser, sqlConnection);
            favCmd.Parameters.AddWithValue("@UserName", userName);
            userCmd.Parameters.AddWithValue("@UserName", userName);

            sqlConnection.Open();
            favCmd.ExecuteNonQuery();
            userCmd.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public bool ValidateAccount(string userName, string password, int age)
        {
            string query = "SELECT COUNT(*) FROM UserAccounts WHERE UserName = @UserName AND Password = @Password AND Age = @Age";
            using SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@UserName", userName);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@Age", age);

            sqlConnection.Open();
            int count = (int)cmd.ExecuteScalar();
            sqlConnection.Close();

            return count > 0;
        }

        public bool IsUserAlreadyRegistered(string userName)
        {
            string query = "SELECT COUNT(*) FROM UserAccounts WHERE UserName = @UserName";
            using SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@UserName", userName);

            sqlConnection.Open();
            int count = (int)cmd.ExecuteScalar();
            sqlConnection.Close();

            return count > 0;
        }

        public bool RegisterAccount(string userName, string password, int age)
        {
            if (IsUserAlreadyRegistered(userName)) return false;

            CreateAccount(new UserAccount
            {
                UserName = userName,
                Password = password,
                Age = age
            });

            return true;
        }

        public UserAccount GetUser(string userName)
        {
            string query = "SELECT * FROM UserAccounts WHERE UserName = @UserName";
            using SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@UserName", userName);

            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            UserAccount user = null;
            if (reader.Read())
            {
                user = new UserAccount
                {
                    UserName = reader["UserName"].ToString(),
                    Password = reader["Password"].ToString(),
                    Age = Convert.ToInt32(reader["Age"]),
                    Favorites = GetFavorites(userName)
                };
            }

            sqlConnection.Close();
            return user;
        }

        public bool AddFavorite(string userName, string book)
        {
            string query = "INSERT INTO UserFavorites (UserName, BookTitle) VALUES (@UserName, @BookTitle)";
            using SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@UserName", userName);
            cmd.Parameters.AddWithValue("@BookTitle", book);

            sqlConnection.Open();
            int rows = cmd.ExecuteNonQuery();
            sqlConnection.Close();

            return rows > 0;
        }

        public bool RemoveFavorite(string userName, string book)
        {
            string query = "DELETE FROM UserFavorites WHERE UserName = @UserName AND BookTitle = @BookTitle";
            using SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@UserName", userName);
            cmd.Parameters.AddWithValue("@BookTitle", book);

            sqlConnection.Open();
            int rows = cmd.ExecuteNonQuery();
            sqlConnection.Close();

            return rows > 0;
        }

        public List<string> GetFavorites(string userName)
        {
            string query = "SELECT TitleFavorites FROM UserFavorites WHERE Username = @Username";
            using SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@Username", userName);

            sqlConnection.Open();
            object result = cmd.ExecuteScalar();
            sqlConnection.Close();

            if (result != null)
            {
                string favorites = result.ToString();
                return new List<string>(favorites.Split(", ", StringSplitOptions.RemoveEmptyEntries));
            }

            return new List<string>();
        }

    }
}
