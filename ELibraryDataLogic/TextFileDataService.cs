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
        private List<UserAccount> users = new();

        public TextFileDataService(string filePath)
        {
            this.filePath = filePath;
            LoadFromFile();
        }

        private void LoadFromFile()
        {
            users = new List<UserAccount>();
            if (!File.Exists(filePath)) return;

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length >= 3)
                {
                    users.Add(new UserAccount
                    {
                        UserName = parts[0],
                        Password = parts[1],
                        Favorites = parts[2].Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                    });
                }
            }
        }

        private void SaveToFile()
        {
            var lines = users.Select(u =>
                $"{u.UserName}|{u.Password}|{string.Join(",", u.Favorites)}");
            File.WriteAllLines(filePath, lines);
        }

        public UserAccount? GetAccountByUsername(string username) =>
            users.FirstOrDefault(u => u.UserName == username);

        public void CreateAccount(UserAccount user)
        {
            if (users.All(u => u.UserName != user.UserName))
            {
                users.Add(user);
                SaveToFile();
            }
        }

        public void UpdateAccount(UserAccount user)
        {
            var existing = GetAccountByUsername(user.UserName);
            if (existing != null)
            {
                existing.Password = user.Password;
                existing.Favorites = user.Favorites;
                SaveToFile();
            }
        }

        public void RemoveAccount(string userName)
        {
            var user = GetAccountByUsername(userName);
            if (user != null)
            {
                users.Remove(user);
                SaveToFile();
            }
        }

        public bool RegisterAccount(string userName, string password)
        {
            if (IsUserAlreadyRegistered(userName)) return false;

            CreateAccount(new UserAccount
            {
                UserName = userName,
                Password = password,
                Favorites = new List<string>()
            });
            return true;
        }

        public bool ValidateAccount(string userName, string password) =>
            GetAccountByUsername(userName)?.Password == password;

        public bool IsUserAlreadyRegistered(string userName) =>
            users.Any(u => u.UserName == userName);

        public bool AddFavorite(string userName, string book)
        {
            var user = GetAccountByUsername(userName);
            if (user == null || user.Favorites.Contains(book)) return false;

            user.Favorites.Add(book);
            SaveToFile();
            return true;
        }

        public bool RemoveFavorite(string userName, string book)
        {
            var user = GetAccountByUsername(userName);
            if (user == null || !user.Favorites.Contains(book)) return false;

            user.Favorites.Remove(book);
            SaveToFile();
            return true;
        }

        public List<string> GetFavorites(string userName) =>
            GetAccountByUsername(userName)?.Favorites ?? new List<string>();

        public List<UserAccount> GetAccounts() => users;
    }
}
