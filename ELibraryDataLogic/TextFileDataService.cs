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

        private readonly Dictionary<string, List<string>> genres = new()
        {
            { "Fantasy", new() { "Titan Academy", "Charm Academy", "Tantei High", "Olympus Academy" } },
            { "Romance", new() { "Hell University", "University Series", "Buenaventura Series", "The Girl He Never Noticed" } },
            { "Drama", new() { "The Tempest", "A Wife's Cry", "Salamasim", "Taste of Sky" } },
            { "Science-Fiction", new() { "Ender's Game", "Project: Yngrid", "The Peculiars Tale", "Mnemosyne's Tale" } },
            { "Action", new() { "The Maze Runner", "The Hunger Games", "Divergent", "The Fifth Wave" } },
            { "Historical", new() { "I Love You Since 1892", "Reincarnated as Binibini", "Our Asymptotic Love" } }
        };

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
                        UserName = parts[0],
                        Password = parts[1],
                        Favorites = parts.Length > 2 ? parts[2].Split(',').Where(f => !string.IsNullOrWhiteSpace(f)).ToList() : new List<string>()
                    };
                    users.Add(user);
                }
            }
        }

        private void SaveToFile()
        {
            var lines = users.Select(u =>
                $"{u.UserName}|{u.Password}|{string.Join(",", u.Favorites ?? new List<string>())}");
            File.WriteAllLines(filePath, lines);
        }

        public List<UserAccount> GetAccounts() => new(users);

        public UserAccount GetAccountByUsername(string UserName) =>
            users.FirstOrDefault(u => u.UserName.Equals(UserName, StringComparison.OrdinalIgnoreCase));

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

        public bool ValidateAccount(string UserName, string Password) =>
            GetAccountByUsername(UserName)?.Password == Password;

        public bool IsUserAlreadyRegistered(string UserName) =>
            users.Any(u => u.UserName.Equals(UserName, StringComparison.OrdinalIgnoreCase));

        public List<string> GetFavorites(string UserName) =>
            GetAccountByUsername(UserName)?.Favorites ?? new();

        public bool AddFavorite(string UserName, string book)
        {
            var user = GetAccountByUsername(UserName);
            if (user == null || string.IsNullOrWhiteSpace(book)) return false;

            user.Favorites ??= new List<string>();
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

        public List<string> GetGenres() => new(genres.Keys);

        public List<string> GetBooksByGenre(string genre) =>
            genres.TryGetValue(genre, out var books) ? new List<string>(books) : new List<string>();

        public List<string> SearchBooksTitle(string KeyWord)
        {
            if (string.IsNullOrWhiteSpace(KeyWord)) return new();

            return genres.Values
                .SelectMany(b => b)
                .Where(b => b.Contains(KeyWord, StringComparison.OrdinalIgnoreCase))
                .Distinct()
                .ToList();
        }
    }
}
