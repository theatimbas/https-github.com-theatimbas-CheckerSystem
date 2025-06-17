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

        private readonly Dictionary<string, List<string>> genres = new Dictionary<string, List<string>>
        {
            { "Fantasy", new List<string>{ "Titan Academy", "Charm Academy", "Tantei High", "Olympus Academy" } },
            { "Romance", new List<string>{ "Hell University", "University Series", "Buenaventura Series", "The Girl He Never Noticed" } },
            { "Drama", new List<string>{ "The Tempest", "A Wife's Cry", "Salamasim", "Taste of Sky" } },
            { "Science-Fiction", new List<string>{ "Ender's Game", "Project: Yngrid", "The Peculiars Tale", "Mnemosyne's Tale" } },
            { "Action", new List<string>{ "The Maze Runner", "The Hunger Games", "Divergent", "The Fifth Wave" } },
            { "Historical", new List<string>{ "I Love You Since 1892", "Reincarnated as Binibini", "Our Asymptotic Love" } }
        };

        public JsonFileDataService(string PathOverride = null)
        {
            if (!string.IsNullOrWhiteSpace(PathOverride))
                filePath = PathOverride;

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

        public List<UserAccount> GetAccounts()
        {
            return new List<UserAccount>(users);
        }

        public UserAccount GetAccountByUsername(string Username)
        {
            return users.FirstOrDefault(u => u.UserName == Username);
        }

        public void CreateAccount(UserAccount user)
        {
            if (!IsUserAlreadyRegistered(user.UserName))
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
        public bool DeleteAccount(string UserName)
        {
            var user = GetAccountByUsername(UserName);
            if (user != null)
            {
                users.Remove(user);
                SaveToFile();
                return true;
            }
            return false;
        }

        public bool ValidateAccount(string UserName, string Password)
        {
            var user = GetAccountByUsername(UserName);
            return user != null && user.Password == Password;
        }

        public bool IsUserAlreadyRegistered(string UserName)
        {
            return users.Any(u => u.UserName == UserName);
        }

        public List<string> GetFavorites(string UserName)
        {
            var user = GetAccountByUsername(UserName);
            return user?.Favorites ?? new List<string>();
        }

        public bool AddFavorite(string UserName, string book)
        {
            var user = GetAccountByUsername(UserName);
            if (user == null) return false;

            user.Favorites ??= new List<string>();

            bool BookExists = genres.Values.Any(list => list.Contains(book));
            if (!BookExists) return false;

            if (user.Favorites.Contains(book)) return false;

            user.Favorites.Add(book);
            SaveToFile();
            return true;
        }

        public bool RemoveFavorite(string UserName, string book)
        {
            var user = GetAccountByUsername(UserName);
            if (user?.Favorites == null || !user.Favorites.Contains(book)) return false;

            user.Favorites.Remove(book);
            SaveToFile();
            return true;
        }
        public List<string> SearchBooksTitle(string KeyWord)
        {
            if (string.IsNullOrWhiteSpace(KeyWord))
                return new List<string>();

            KeyWord = KeyWord.Trim().ToLower();
            var MatchBooks = new List<string>();

            foreach (var BookList in genres.Values)
            {
                MatchBooks.AddRange(BookList.Where(book =>
                    book.ToLower().Contains(KeyWord)));
            }

            return MatchBooks.Distinct().ToList();
        }

        public List<string> GetGenres()
        {
            return genres.Keys.ToList();
        }

        public List<string> GetBooksByGenre(string genre)
        {
            return genres.ContainsKey(genre) ? genres[genre] : new List<string>();
        }
    }
}
