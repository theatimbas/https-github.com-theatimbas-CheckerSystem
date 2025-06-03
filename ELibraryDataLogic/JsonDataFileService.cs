using PFinderCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ELibraryDataLogic
{
    public class JsonFileDataService : IFinderDataService
    {
        private static List<UserAccount> accounts = new();
        private static string filePath = "accounts.json";

        public JsonFileDataService()
        {
            ReadJsonDataFromFile();
        }

        private void ReadJsonDataFromFile()
        {
            if (!File.Exists(filePath))
            {
                accounts = new List<UserAccount>();
                return;
            }

            string jsonText = File.ReadAllText(filePath);
            accounts = JsonSerializer.Deserialize<List<UserAccount>>(jsonText, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<UserAccount>();
        }

        private void WriteJsonDataToFile()
        {
            string jsonString = JsonSerializer.Serialize(accounts, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(filePath, jsonString);
        }

        private int FindIndex(UserAccount user)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].UserName == user.UserName)
                    return i;
            }
            return -1;
        }

        public List<UserAccount> GetAccounts() => accounts;

        public UserAccount GetUser(string userName)
        {
            return accounts.FirstOrDefault(u => u.UserName == userName);
        }

        public void CreateAccount(UserAccount newUser)
        {
            if (GetUser(newUser.UserName) != null)
                return;

            newUser.Favorites ??= new List<string>();
            accounts.Add(newUser);
            WriteJsonDataToFile();
        }

        public void UpdateAccount(UserAccount updatedUser)
        {
            int index = FindIndex(updatedUser);
            if (index != -1)
            {
                accounts[index] = updatedUser;
                WriteJsonDataToFile();
            }
        }

        public void RemoveAccount(string userName)
        {
            var index = accounts.FindIndex(u => u.UserName == userName);
            if (index != -1)
            {
                accounts.RemoveAt(index);
                WriteJsonDataToFile();
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

        public bool ValidateAccount(string userName, string password, int age)
        {
            return accounts.Exists(u =>
                u.UserName == userName &&
                u.Password == password &&
                u.Age == age);
        }

        public bool IsUserAlreadyRegistered(string userName)
        {
            return GetUser(userName) != null;
        }

        public bool AddFavorite(string userName, string book)
        {
            var user = GetUser(userName);
            if (user == null || string.IsNullOrWhiteSpace(book) || user.Favorites.Contains(book))
                return false;

            user.Favorites.Add(book);
            WriteJsonDataToFile();
            return true;
        }

        public bool RemoveFavorite(string userName, string book)
        {
            var user = GetUser(userName);
            if (user == null || !user.Favorites.Contains(book))
                return false;

            user.Favorites.Remove(book);
            WriteJsonDataToFile();
            return true;
        }

        public List<string> GetFavorites(string userName)
        {
            var user = GetUser(userName);
            return user?.Favorites ?? new List<string>();
        }
    }
}