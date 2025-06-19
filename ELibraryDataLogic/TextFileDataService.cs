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
                        UserName = parts[0].Trim(),
                        Password = parts[1].Trim(),
                        Favorites = parts.Length > 2
                            ? parts[2].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(f => f.Trim()).ToList()
                            : new List<string>()
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

        public List<UserAccount> GetAccounts() =>
            users.Select(u => new UserAccount
            {
                UserName = u.UserName,
                Password = u.Password,
                Favorites = new List<string>(u.Favorites ?? new List<string>())
            }).ToList();

        public UserAccount? GetAccountByUsername(string userName) =>
            users.FirstOrDefault(u => u.UserName.Equals(userName.Trim(), StringComparison.OrdinalIgnoreCase));

        public void CreateAccount(UserAccount user)
        {
            if (!IsUserAlreadyRegistered(user.UserName))
            {
                user.Favorites ??= new List<string>();
                users.Add(new UserAccount
                {
                    UserName = user.UserName.Trim(),
                    Password = user.Password.Trim(),
                    Favorites = new List<string>(user.Favorites)
                });
                SaveToFile();
            }
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

        public bool ValidateAccount(string userName, string password) =>
            GetAccountByUsername(userName)?.Password == password.Trim();

        public bool IsUserAlreadyRegistered(string userName) =>
            users.Any(u => u.UserName.Equals(userName.Trim(), StringComparison.OrdinalIgnoreCase));

        public List<string> GetFavorites(string userName)
        {
            var user = GetAccountByUsername(userName);
            return user?.Favorites != null ? new List<string>(user.Favorites) : new List<string>();
        }

        public bool AddFavorite(string userName, string book)
        {
            var user = GetAccountByUsername(userName);
            if (user == null || string.IsNullOrWhiteSpace(book)) return false;

            user.Favorites ??= new List<string>();

            bool bookExists = genres.Values.Any(list => list.Contains(book));
            if (!bookExists || user.Favorites.Contains(book)) return false;

            user.Favorites.Add(book);
            SaveToFile();
            return true;
        }

        public bool RemoveFavorite(string userName, string book)
        {
            var user = GetAccountByUsername(userName);
            if (user?.Favorites == null || !user.Favorites.Contains(book)) return false;

            user.Favorites.Remove(book);
            SaveToFile();
            return true;
        }

        public List<string> GetGenres() => genres.Keys.ToList();

        public List<string> GetBooksByGenre(string genre) =>
            genres.TryGetValue(genre, out var books) ? new List<string>(books) : new List<string>();

        public List<string> SearchBooksTitle(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return new();

            return genres.Values
                .SelectMany(b => b)
                .Where(b => b.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .Distinct()
                .ToList();
        }
    }
}
