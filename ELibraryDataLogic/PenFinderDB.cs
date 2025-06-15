using PFinderCommon;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace ELibraryDataLogic
{
    public class PenFinderDB : IFinderDataService
    {
        private static readonly string connectionString =
            "Data Source=desktop-781f9v1\\SQLEXPRESS;Initial Catalog=PenFinderDB;Integrated Security=True;TrustServerCertificate=True;";

        public List<UserAccount> GetAccounts()
        {
            var accounts = new List<UserAccount>();
            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("SELECT UserName, Password FROM UserAccounts", conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string userName = reader.GetString(0);
                accounts.Add(new UserAccount
                {
                    UserName = userName,
                    Password = reader.GetString(1),
                    Favorites = GetFavorites(userName)
                });
            }
            return accounts;
        }

        public UserAccount GetAccountByUsername(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return null;

            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("SELECT UserName, Password FROM UserAccounts WHERE UserName = @UserName", conn);
            cmd.Parameters.AddWithValue("@UserName", userName.Trim());
            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string uname = reader.GetString(0);
                return new UserAccount
                {
                    UserName = uname,
                    Password = reader.GetString(1),
                    Favorites = GetFavorites(uname)
                };
            }
            return null;
        }

        public void CreateAccount(UserAccount userAccount)
        {
            RegisterAccount(userAccount.UserName, userAccount.Password);
        }

        public bool RegisterAccount(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                return false;

            userName = userName.Trim();
            password = password.Trim();

            if (IsUserAlreadyRegistered(userName))
                return false;

            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(
                "INSERT INTO UserAccounts (UserName, Password) VALUES (@UserName, @Password)", conn);

            cmd.Parameters.AddWithValue("@UserName", userName);
            cmd.Parameters.AddWithValue("@Password", password);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public void UpdateAccount(UserAccount account)
        {
            if (account == null) return;

            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(
                "UPDATE UserAccounts SET Password = @Password WHERE UserName = @UserName", conn);

            cmd.Parameters.AddWithValue("@Password", account.Password.Trim());
            cmd.Parameters.AddWithValue("@UserName", account.UserName.Trim());
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void RemoveAccount(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return;

            userName = userName.Trim();

            using var conn = new SqlConnection(connectionString);
            conn.Open();
            using var tx = conn.BeginTransaction();

            using var deleteFavs = new SqlCommand(
                "DELETE FROM UserFavorites WHERE UserName = @UserName", conn, tx);
            deleteFavs.Parameters.AddWithValue("@UserName", userName);
            deleteFavs.ExecuteNonQuery();

            using var deleteUser = new SqlCommand(
                "DELETE FROM UserAccounts WHERE UserName = @UserName", conn, tx);
            deleteUser.Parameters.AddWithValue("@UserName", userName);
            deleteUser.ExecuteNonQuery();

            tx.Commit();
        }

        public bool ValidateAccount(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                return false;

            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(
                "SELECT COUNT(*) FROM UserAccounts WHERE UserName = @UserName AND Password = @Password", conn);

            cmd.Parameters.AddWithValue("@UserName", userName.Trim());
            cmd.Parameters.AddWithValue("@Password", password.Trim());
            conn.Open();

            return ((int)cmd.ExecuteScalar()) > 0;
        }

        public bool IsUserAlreadyRegistered(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return false;

            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(
                "SELECT COUNT(*) FROM UserAccounts WHERE UserName = @UserName", conn);

            cmd.Parameters.AddWithValue("@UserName", userName.Trim());
            conn.Open();

            return ((int)cmd.ExecuteScalar()) > 0;
        }

        public List<string> GetFavorites(string userName)
        {
            var list = new List<string>();

            if (string.IsNullOrWhiteSpace(userName))
                return list;

            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(
                "SELECT BookTitle FROM UserFavorites WHERE UserName = @UserName", conn);

            cmd.Parameters.AddWithValue("@UserName", userName.Trim());
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(reader.GetString(0));
            }

            return list;
        }

        public bool AddFavorite(string userName, string book)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(book))
                return false;

            var existing = GetFavorites(userName.Trim());
            if (existing.Contains(book.Trim()))
                return false;

            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(
                "INSERT INTO UserFavorites (UserName, BookTitle) VALUES (@UserName, @BookTitle)", conn);

            cmd.Parameters.AddWithValue("@UserName", userName.Trim());
            cmd.Parameters.AddWithValue("@BookTitle", book.Trim());
            conn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool RemoveFavorite(string userName, string book)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(book))
                return false;

            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(
                "DELETE FROM UserFavorites WHERE UserName = @UserName AND BookTitle = @BookTitle", conn);

            cmd.Parameters.AddWithValue("@UserName", userName.Trim());
            cmd.Parameters.AddWithValue("@BookTitle", book.Trim());
            conn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }

        public List<string> GetGenres()
        {
            var list = new List<string>();

            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("SELECT Name FROM Genres", conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(reader.GetString(0));
            }

            return list;
        }

        public List<string> GetBooksByGenre(string genre)
        {
            var list = new List<string>();

            if (string.IsNullOrWhiteSpace(genre))
                return list;

            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(@"
                SELECT b.Title 
                FROM Books b
                JOIN Genres g ON b.GenreId = g.Id
                WHERE g.Name = @Genre", conn);

            cmd.Parameters.AddWithValue("@Genre", genre.Trim());
            conn.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(reader.GetString(0));
            }

            return list;
        }
        public List<string> SearchBooksTitle(string keyword)
        {
            List<string> result = new List<string>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT Title FROM Books WHERE Title LIKE @search", connection);
                command.Parameters.AddWithValue("@search", "%" + keyword + "%");

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(reader.GetString(0));
                    }
                }
            }
            return result;
        }

    }
}
