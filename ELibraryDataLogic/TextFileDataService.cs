using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PFinderCommon;

namespace ELibraryDataLogic
{
    public class TextFileDataService : IFinderDataService
    {
        private readonly string filePath;
        private readonly List<UserAccount> users = new();

        public TextFileDataService(string filePath)
        {
            this.filePath = filePath;
            LoadFromFile();
        }
        private void LoadFromFile()
        {
            users.Clear();

            if (!File.Exists(filePath)) return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split('|');

                if (parts.Length >= 2)
                {
                    var user = new UserAccount
                    {
                        UserName = parts[0].Trim(),
                        Password = parts[1].Trim(),
                        Favorites = parts.Length > 2
                            ? parts[2].Split(',', StringSplitOptions.RemoveEmptyEntries)
                                      .Select(f => f.Trim()).ToList()
                            : new List<string>()
                    };
                    users.Add(user);
                }
            }
        }
        private void SaveToFile()
        {
            var lines = users.Select(user =>
                $"{user.UserName}|{user.Password}|{string.Join(",", user.Favorites ?? new())}");
            File.WriteAllLines(filePath, lines);
        }
        public List<UserAccount> GetAccounts()
        {
            return users.Select(user => new UserAccount
            {
                UserName = user.UserName,
                Password = user.Password,
                Favorites = new List<string>(user.Favorites ?? new())
            }).ToList();
        }
        public UserAccount? GetAccountByUsername(string userName)
        {
            return users.FirstOrDefault(user =>
                user.UserName.Equals(userName.Trim(), StringComparison.OrdinalIgnoreCase));
        }
        public void CreateAccount(UserAccount user)
        {
            if (string.IsNullOrWhiteSpace(user?.UserName) || IsUserAlreadyRegistered(user.UserName))
                return;

            users.Add(new UserAccount
            {
                UserName = user.UserName.Trim(),
                Password = user.Password.Trim(),
                Favorites = new List<string>(user.Favorites ?? new())
            });

            SaveToFile();
        }
        public void UpdateAccount(UserAccount user)
        {
            var existing = GetAccountByUsername(user.UserName);
            if (existing != null)
            {
                existing.Password = user.Password.Trim();
                existing.Favorites = user.Favorites ?? new();
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
            return users.Any(user =>
                user.UserName.Equals(userName.Trim(), StringComparison.OrdinalIgnoreCase));
        }
        public List<string> GetFavorites(string userName)
        {
            var user = GetAccountByUsername(userName);
            return user?.Favorites != null ? new(user.Favorites) : new();
        }
        public bool AddFavorite(string userName, string book)
        {
            if (string.IsNullOrWhiteSpace(book)) return false;

            var user = GetAccountByUsername(userName);
            if (user == null) return false;

            user.Favorites ??= new();

            if (!Library.BookExists(book)) return false;

            if (user.Favorites.Any(f => f.Equals(book, StringComparison.OrdinalIgnoreCase)))
                return false;

            user.Favorites.Add(book);
            SaveToFile();
            return true;
        }
        public bool RemoveFavorite(string userName, string book)
        {
            var user = GetAccountByUsername(userName);
            if (user?.Favorites == null) return false;

            int initialCount = user.Favorites.Count;
            user.Favorites = user.Favorites
                .Where(favorite => !favorite.Equals(book, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (user.Favorites.Count < initialCount)
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
