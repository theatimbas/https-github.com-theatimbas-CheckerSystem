using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using PFinderCommon;

namespace ELibraryDataLogic
{
    public class JsonFileDataService : IFinderDataService
    {
        private static string filePath = "account.json";
        private List<UserAccount> users = new();

        public JsonFileDataService(string? pathOverride = null)
        {
            if (!string.IsNullOrWhiteSpace(pathOverride))
                filePath = pathOverride;

            LoadFromFile();
        }

        private void LoadFromFile()
        {
            if (!File.Exists(filePath))
            {
                users = new List<UserAccount>();
                return;
            }

            try
            {
                string json = File.ReadAllText(filePath);
                users = JsonSerializer.Deserialize<List<UserAccount>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<UserAccount>();
            }
            catch
            {
                users = new List<UserAccount>();
            }
        }

        private void SaveToFile()
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(filePath, json);
        }

        public List<UserAccount> GetAccounts()
        {
            List<UserAccount> accountsCopy = new List<UserAccount>();
            foreach (var u in users)
            {
                accountsCopy.Add(new UserAccount
                {
                    UserName = u.UserName,
                    Password = u.Password,
                    Favorites = new List<string>(u.Favorites ?? new List<string>())
                });
            }
            return accountsCopy;
        }

        public UserAccount? GetAccountByUsername(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return null;

            foreach (var user in users)
            {
                if (user.UserName.Equals(userName.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return user;
                }
            }
            return null;
        }

        public void CreateAccount(UserAccount user)
        {
            if (user == null || IsUserAlreadyRegistered(user.UserName)) return;

            users.Add(new UserAccount
            {
                UserName = user.UserName.Trim(),
                Password = user.Password.Trim(),
                Favorites = new List<string>(user.Favorites ?? new List<string>())
            });

            SaveToFile();
        }

        public void UpdateAccount(UserAccount user)
        {
            var existing = GetAccountByUsername(user.UserName);
            if (existing != null)
            {
                existing.Password = user.Password.Trim();
                existing.Favorites = user.Favorites ?? new List<string>();
                SaveToFile();
            }
        }

        public bool DeleteAccount(string userName)
        {
            var user = GetAccountByUsername(userName);
            if (user != null)
            {
                users.Remove(user);
                SaveToFile();
                return true;
            }
            return false;
        }

        public bool ValidateAccount(string userName, string password)
        {
            var user = GetAccountByUsername(userName);
            return user != null && user.Password == password.Trim();
        }

        public bool IsUserAlreadyRegistered(string userName)
        {
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
            {
                return new List<string>(user.Favorites);
            }
            return new List<string>();
        }

        public bool AddFavorite(string userName, string book)
        {
            if (string.IsNullOrWhiteSpace(book)) return false;

            var user = GetAccountByUsername(userName);
            if (user == null) return false;

            if (user.Favorites == null)
                user.Favorites = new List<string>();

            if (!Library.BookExists(book)) return false;

            foreach (var fav in user.Favorites)
            {
                if (fav.Equals(book, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            user.Favorites.Add(book);
            SaveToFile();
            return true;
        }

        public bool RemoveFavorite(string userName, string book)
        {
            var user = GetAccountByUsername(userName);
            if (user == null || user.Favorites == null) return false;

            int beforeCount = user.Favorites.Count;
            List<string> newFavorites = new List<string>();

            foreach (var fav in user.Favorites)
            {
                if (!fav.Equals(book, StringComparison.OrdinalIgnoreCase))
                {
                    newFavorites.Add(fav);
                }
            }

            user.Favorites = newFavorites;

            if (user.Favorites.Count < beforeCount)
            {
                SaveToFile();
                return true;
            }

            return false;
        }

        public List<string> GetGenres()
        {
            return Library.GetGenres();
        }

        public List<string> GetBooksByGenre(string genre)
        {
            return Library.GetBooksByGenre(genre);
        }

        public List<string> SearchBooksTitle(string keyword)
        {
            return Library.SearchBooks(keyword);
        }
    }
}
