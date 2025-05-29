using System;
using System.Collections.Generic;
using System.IO;

namespace ELibraryDataLogic
{
    public class TextFileDataService : IFinderDataService
    {
        private readonly string filePath = "accounts.txt";
        private List<UserAccount> accounts = new List<UserAccount>();

        public TextFileDataService() => LoadFromFile();

        private void LoadFromFile()
        {
            if (!File.Exists(filePath)) return;

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length < 3) continue;

                var favorites = new List<string>();
                if (parts.Length > 3)
                    favorites.AddRange(parts[3].Split(',', StringSplitOptions.RemoveEmptyEntries));

                accounts.Add(new UserAccount
                {
                    UserName = parts[0],
                    Password = parts[1],
                    Age = int.Parse(parts[2]),
                    Favorites = favorites
                });
            }
        }

        private void SaveToFile()
        {
            var lines = new List<string>();
            foreach (var acc in accounts)
            {
                string favs = string.Join(",", acc.Favorites);
                lines.Add($"{acc.UserName}|{acc.Password}|{acc.Age}|{favs}");
            }
            File.WriteAllLines(filePath, lines);
        }

        public List<UserAccount> GetAccounts() => accounts;

        public UserAccount GetUser(string userName) =>
            accounts.Find(u => u.UserName == userName);

        public void CreateAccount(UserAccount userAccount)
        {
            if (GetUser(userAccount.UserName) != null) return;
            accounts.Add(userAccount);
            SaveToFile();
        }

        public void UpdateAccount(UserAccount userAccount)
        {
            int index = accounts.FindIndex(u => u.UserName == userAccount.UserName);
            if (index != -1)
            {
                accounts[index] = userAccount;
                SaveToFile();
            }
        }

        public void RemoveAccount(string userName)
        {
            var account = GetUser(userName);
            if (account != null)
            {
                accounts.Remove(account);
                SaveToFile();
            }
        }

        public bool RegisterAccount(string userName, string password, int age)
        {
            if (IsUserAlreadyRegistered(userName)) return false;

            CreateAccount(new UserAccount
            {
                UserName = userName,
                Password = password,
                Age = age,
                Favorites = new List<string>()
            });

            return true;
        }

        public bool IsUserAlreadyRegistered(string userName) =>
            GetUser(userName) != null;

        public bool ValidateAccount(string userName, string password, int age) =>
            accounts.Exists(u => u.UserName == userName && u.Password == password && u.Age == age);

        public bool AddFavorite(string userName, string book)
        {
            var user = GetUser(userName);
            if (user == null || user.Favorites.Contains(book)) return false;

            user.Favorites.Add(book);
            SaveToFile();
            return true;
        }

        public bool RemoveFavorite(string userName, string book)
        {
            var user = GetUser(userName);
            if (user == null || !user.Favorites.Contains(book)) return false;

            user.Favorites.Remove(book);
            SaveToFile();
            return true;
        }

        public List<string> GetFavorites(string userName)
        {
            var user = GetUser(userName);
            return user?.Favorites ?? new List<string>();
        }
    }
}
