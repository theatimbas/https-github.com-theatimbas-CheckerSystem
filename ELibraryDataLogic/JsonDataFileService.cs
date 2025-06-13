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
        private static string filePath = "accounts.json";
        private List<UserAccount> users;

        public JsonFileDataService(string filePath = null)
        {
            if (!string.IsNullOrWhiteSpace(filePath))
                JsonFileDataService.filePath = filePath;

            LoadFromFile();
        }

        private void LoadFromFile()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                users = JsonSerializer.Deserialize<List<UserAccount>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<UserAccount>();
            }
            else
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

        public UserAccount GetAccountByUsername(string username)
        {
            return users.FirstOrDefault(u => u.UserName == username);
        }

        public void CreateAccount(UserAccount user)
        {
            if (!users.Any(u => u.UserName == user.UserName))
            {
                user.Favorites ??= new List<string>();
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
                existing.Favorites = user.Favorites ?? new List<string>();
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
            if (IsUserAlreadyRegistered(userName))
                return false;

            var newUser = new UserAccount
            {
                UserName = userName,
                Password = password,
                Favorites = new List<string>()
            };
            CreateAccount(newUser);
            return true;
        }

        public bool ValidateAccount(string userName, string password)
        {
            var user = GetAccountByUsername(userName);
            return user != null && user.Password == password;
        }

        public bool IsUserAlreadyRegistered(string userName)
        {
            return users.Any(u => u.UserName == userName);
        }

        public bool AddFavorite(string userName, string book)
        {
            var user = GetAccountByUsername(userName);
            if (user != null)
            {
                user.Favorites ??= new List<string>();

                if (!user.Favorites.Contains(book))
                {
                    user.Favorites.Add(book);
                    SaveToFile();
                    return true;
                }
            }
            return false;
        }

        public bool RemoveFavorite(string userName, string book)
        {
            var user = GetAccountByUsername(userName);
            if (user != null && user.Favorites?.Contains(book) == true)
            {
                user.Favorites.Remove(book);
                SaveToFile();
                return true;
            }
            return false;
        }

        public List<string> GetFavorites(string userName)
        {
            var user = GetAccountByUsername(userName);
            return user?.Favorites ?? new List<string>();
        }

        public List<UserAccount> GetAccounts()
        {
            return new List<UserAccount>(users);
        }
    }
}
