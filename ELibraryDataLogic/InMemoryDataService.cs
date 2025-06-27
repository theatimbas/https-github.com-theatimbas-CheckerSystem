using System;
using System.Collections.Generic;
using System.Linq;
using PFinderCommon;

namespace ELibraryDataLogic
{
    public class InMemoryDataService : IFinderDataService
    {
        private readonly List<UserAccount> users = new();

        public List<UserAccount> GetAccounts()
        {
            List<UserAccount> accounts = new();
            foreach (var user in users)
            {
                accounts.Add(new UserAccount
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    Favorites = new List<string>(user.Favorites ?? new List<string>())
                });
            }
            return accounts;
        }

        public UserAccount? GetAccountByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            foreach (var user in users)
            {
                if (user.UserName.Equals(username.Trim(), StringComparison.OrdinalIgnoreCase))
                    return user;
            }

            return null;
        }

        public void CreateAccount(UserAccount user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.UserName) || IsUserAlreadyRegistered(user.UserName))
                return;

            users.Add(new UserAccount
            {
                UserName = user.UserName.Trim(),
                Password = user.Password.Trim(),
                Favorites = user.Favorites ?? new List<string>()
            });
        }

        public bool RegisterAccount(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                return false;

            if (IsUserAlreadyRegistered(userName))
                return false;

            CreateAccount(new UserAccount
            {
                UserName = userName,
                Password = password,
                Favorites = new List<string>()
            });

            return true;
        }

        public void UpdateAccount(UserAccount user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.UserName))
                return;

            var existingUser = GetAccountByUsername(user.UserName);
            if (existingUser != null)
            {
                existingUser.Password = user.Password;
                existingUser.Favorites = user.Favorites ?? new List<string>();
            }
        }

        public bool DeleteAccount(string userName)
        {
            var user = GetAccountByUsername(userName);
            if (user != null)
            {
                users.Remove(user);
                return true;
            }
            return false;
        }

        public bool ValidateAccount(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                return false;

            var user = GetAccountByUsername(userName);
            return user != null && user.Password == password;
        }

        public bool IsUserAlreadyRegistered(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return false;

            foreach (var user in users)
            {
                if (user.UserName.Equals(userName.Trim(), StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        public List<string> GetFavorites(string userName)
        {
            var user = GetAccountByUsername(userName);
            if (user != null && user.Favorites != null)
                return new List<string>(user.Favorites);

            return new List<string>();
        }

        public bool AddFavorite(string userName, string book)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(book))
                return false;

            var user = GetAccountByUsername(userName);
            if (user == null || user.Favorites == null)
                return false;

            if (!Library.BookExists(book))
                return false;

            if (user.Favorites.Contains(book))
                return false;

            user.Favorites.Add(book);
            return true;
        }

        public bool RemoveFavorite(string userName, string book)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(book))
                return false;

            var user = GetAccountByUsername(userName);
            if (user?.Favorites == null || !user.Favorites.Contains(book))
                return false;

            user.Favorites.Remove(book);
            return true;
        }

        public List<string> GetGenres()
        {
            return Library.GetGenres();
        }

        public List<string> GetBooksByGenre(string genre)
        {
            if (string.IsNullOrWhiteSpace(genre))
                return new List<string>();

            return Library.GetBooksByGenre(genre);
        }

        public List<string> SearchBooksTitle(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<string>();

            return Library.SearchBooks(keyword);
        }
    }
}
