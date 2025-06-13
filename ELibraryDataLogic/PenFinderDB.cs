using PFinderCommon;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace ELibraryDataLogic
{
    public class PenFinderDB : IFinderDataService
    {
        private static readonly string connectionString =
            "Data Source=desktop-781f9v1\\SQLEXPRESS;Initial Catalog=DBPenFinder;Integrated Security=True;TrustServerCertificate=True;";

        public bool RegisterAccount(string userName, string password)
        {
            if (IsUserAlreadyRegistered(userName))
                return false;

            using var connection = new SqlConnection(connectionString);
            var cmd = new SqlCommand(
                "INSERT INTO UserAccounts (UserName, Password) VALUES (@UserName, @Password)", connection);
            cmd.Parameters.AddWithValue("@UserName", userName);
            cmd.Parameters.AddWithValue("@Password", password);
            connection.Open();
            cmd.ExecuteNonQuery();

            return true;
        }

        public UserAccount? GetAccountByUsername(string userName)
        {
            using var connection = new SqlConnection(connectionString);
            var cmd = new SqlCommand("SELECT * FROM UserAccounts WHERE UserName = @UserName", connection);
            cmd.Parameters.AddWithValue("@UserName", userName);
            connection.Open();

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new UserAccount
                {
                    UserName = reader["UserName"]?.ToString() ?? string.Empty,
                    Password = reader["Password"]?.ToString() ?? string.Empty,
                    Favorites = GetFavorites(userName)
                };
            }
            return null;
        }

        public void UpdateAccount(UserAccount account)
        {
            using var connection = new SqlConnection(connectionString);
            var cmd = new SqlCommand(
                "UPDATE UserAccounts SET Password = @Password WHERE UserName = @UserName", connection);
            cmd.Parameters.AddWithValue("@Password", account.Password);
            cmd.Parameters.AddWithValue("@UserName", account.UserName);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public void CreateAccount(UserAccount userAccount)
        {
            RegisterAccount(userAccount.UserName, userAccount.Password);
            // Save favorites if your schema allows (not implemented here)
        }

        public void RemoveAccount(string userName)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var deleteFavoritesCmd = new SqlCommand("DELETE FROM UserFavorites WHERE UserName = @UserName", connection, transaction);
            deleteFavoritesCmd.Parameters.AddWithValue("@UserName", userName);
            deleteFavoritesCmd.ExecuteNonQuery();

            var deleteUserCmd = new SqlCommand("DELETE FROM UserAccounts WHERE UserName = @UserName", connection, transaction);
            deleteUserCmd.Parameters.AddWithValue("@UserName", userName);
            deleteUserCmd.ExecuteNonQuery();

            transaction.Commit();
        }

        public bool ValidateAccount(string userName, string password)
        {
            using var connection = new SqlConnection(connectionString);
            var cmd = new SqlCommand("SELECT COUNT(*) FROM UserAccounts WHERE UserName = @UserName AND Password = @Password", connection);
            cmd.Parameters.AddWithValue("@UserName", userName);
            cmd.Parameters.AddWithValue("@Password", password);
            connection.Open();

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        public bool IsUserAlreadyRegistered(string userName)
        {
            using var connection = new SqlConnection(connectionString);
            var cmd = new SqlCommand("SELECT COUNT(*) FROM UserAccounts WHERE UserName = @UserName", connection);
            cmd.Parameters.AddWithValue("@UserName", userName);
            connection.Open();

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        public List<string> GetFavorites(string userName)
        {
            var favorites = new List<string>();
            using var connection = new SqlConnection(connectionString);
            var cmd = new SqlCommand("SELECT BookTitle FROM UserFavorites WHERE UserName = @UserName", connection);
            cmd.Parameters.AddWithValue("@UserName", userName);
            connection.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                favorites.Add(reader["BookTitle"]?.ToString() ?? string.Empty);
            }
            return favorites;
        }

        public List<UserAccount> GetAccounts()
        {
            var accounts = new List<UserAccount>();
            using var connection = new SqlConnection(connectionString);
            var cmd = new SqlCommand("SELECT * FROM UserAccounts", connection);
            connection.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                accounts.Add(new UserAccount
                {
                    UserName = reader["UserName"]?.ToString() ?? string.Empty,
                    Password = reader["Password"]?.ToString() ?? string.Empty,
                    Favorites = GetFavorites(reader["UserName"]?.ToString() ?? string.Empty)
                });
            }
            return accounts;
        }

        public bool AddFavorite(string userName, string book)
        {
            var favs = GetFavorites(userName);
            if (favs.Contains(book))
                return false;

            using var connection = new SqlConnection(connectionString);
            var cmd = new SqlCommand("INSERT INTO UserFavorites (UserName, BookTitle) VALUES (@UserName, @BookTitle)", connection);
            cmd.Parameters.AddWithValue("@UserName", userName);
            cmd.Parameters.AddWithValue("@BookTitle", book);
            connection.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool RemoveFavorite(string userName, string book)
        {
            using var connection = new SqlConnection(connectionString);
            var cmd = new SqlCommand("DELETE FROM UserFavorites WHERE UserName = @UserName AND BookTitle = @BookTitle", connection);
            cmd.Parameters.AddWithValue("@UserName", userName);
            cmd.Parameters.AddWithValue("@BookTitle", book);
            connection.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
