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
                if (parts.Length >= 3)
                {
                    users.Add(new UserAccount
                    {
                        UserName = parts[0],
                        Password = parts[1],
                        Favorites = parts[2]
                            .Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(f => f.Trim()).ToList()
                    });
                }
            }
        }

        private void SaveToFile()
        {
            var lines = users.Select(u =>
                $"{u.UserName}|{u.Password}|{string.Join(",", u.Favorites ?? new List<string>())}");
            File.WriteAllLines(filePath, lines);
        }

        private bool IsValidInput(string? input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        public UserAccount? GetAccountByUsername(string username)
        {
            if (!IsValidInput(username)) return null;
            return users.FirstOrDefault(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public void CreateAccount(UserAccount user)
        {
            if (!IsValidInput(user.UserName) || !IsValidInput(user.Password)) return;

            if (!IsUserAlreadyRegistered(user.UserName))
            {
                user.Favorites ??= new List<string>();
                users.Add(user);
                SaveToFile();
            }
        }

        public void UpdateAccount(UserAccount user)
        {
            if (!IsValidInput(user.UserName)) return;

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
            if (!IsValidInput(userName)) return;

            var user = GetAccountByUsername(userName);
            if (user != null)
            {
                users.Remove(user);
                SaveToFile();
            }
        }

        public bool RegisterAccount(string userName, string password)
        {
            if (!IsValidInput(userName) || !IsValidInput(password))
                return false;

            if (IsUserAlreadyRegistered(userName)) return false;

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
            if (!IsValidInput(userName) || !IsValidInput(password))
                return false;

            return GetAccountByUsername(userName)?.Password == password;
        }

        public bool IsUserAlreadyRegistered(string userName)
        {
            if (!IsValidInput(userName)) return false;
            return users.Any(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }

        public bool AddFavorite(string userName, string book)
        {
            if (!IsValidInput(userName) || !IsValidInput(book)) return false;

            var user = GetAccountByUsername(userName);
            if (user == null) return false;

            user.Favorites ??= new List<string>();
            if (user.Favorites.Contains(book)) return false;

            user.Favorites.Add(book);
            SaveToFile();
            return true;
        }

        public bool RemoveFavorite(string userName, string book)
        {
            if (!IsValidInput(userName) || !IsValidInput(book)) return false;

            var user = GetAccountByUsername(userName);
            if (user?.Favorites == null) return false;

            if (user.Favorites.Remove(book))
            {
                SaveToFile();
                return true;
            }

            return false;
        }

        public List<string> GetFavorites(string userName)
        {
            if (!IsValidInput(userName)) return new List<string>();
            return GetAccountByUsername(userName)?.Favorites ?? new List<string>();
        }

        public List<UserAccount> GetAccounts() => new(users);

        public List<string> GetGenres() => new(genres.Keys);

        public List<string> GetBooksByGenre(string genre)
        {
            return genres.TryGetValue(genre, out var books) ? new List<string>(books) : new List<string>();
        }
        public List<string> SearchBooksTitle(string keyword)
        {
            var results = new List<string>();

            foreach (var genre in genres)
            {
                foreach (var book in genre.Value)
                {
                    if (book.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        results.Add(book);
                    }
                }
            }

            return results;
        }

    }
}
