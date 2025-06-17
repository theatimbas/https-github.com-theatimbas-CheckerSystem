using System.Collections.Generic;
using System.Linq;
using PFinderCommon;

namespace ELibraryDataLogic
{
    public class InMemoryDataService : IFinderDataService
    {
        private readonly List<UserAccount> users = new List<UserAccount>();

        private readonly Dictionary<string, List<string>> genreBooks = new Dictionary<string, List<string>>
        {
            { "Fantasy", new List<string>{ "Titan Academy", "Charm Academy", "Tantei High", "Olympus Academy" } },
            { "Romance", new List<string>{ "Hell University", "University Series", "Buenaventura Series", "The Girl He Never Noticed" } },
            { "Drama", new List<string>{ "The Tempest", "A Wife's Cry", "Salamasim", "Taste of Sky" } },
            { "Science-Fiction", new List<string>{ "Ender's Game", "Project: Yngrid", "The Peculiars Tale", "Mnemosyne's Tale" } },
            { "Action", new List<string>{ "The Maze Runner", "The Hunger Games", "Divergent", "The Fifth Wave" } },
            { "Historical", new List<string>{ "I Love You Since 1892", "Reincarnated as Binibini", "Our Asymptotic Love" } }
        };

        public List<UserAccount> GetAccounts()
        {
            return users.Select(u => new UserAccount
            {
                UserName = u.UserName,
                Password = u.Password,
                Favorites = new List<string>(u.Favorites ?? new List<string>())
            }).ToList();
        }

        public UserAccount GetAccountByUsername(string username)
        {
            return users.FirstOrDefault(u => u.UserName == username);
        }

        public void CreateAccount(UserAccount user)
        {
            if (!IsUserAlreadyRegistered(user.UserName))
            {
                users.Add(new UserAccount
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    Favorites = new List<string>()
                });
            }
        }

        public bool RegisterAccount(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                return false;

            if (IsUserAlreadyRegistered(userName))
                return false;

            CreateAccount(new UserAccount
            {
                UserName = userName.Trim(),
                Password = password.Trim(),
                Favorites = new List<string>()
            });

            return true;
        }

        public void UpdateAccount(UserAccount user)
        {
            var existingUser = GetAccountByUsername(user.UserName);
            if (existingUser != null)
            {
                existingUser.Password = user.Password;
                existingUser.Favorites = user.Favorites ?? new List<string>();
            }
        }

        public bool DeleteAccount(string userName)
        {
            var user = GetAccountByUsername(userName);
            if (user != null)
            {
                users.Remove(user);
                return true;
            }
            return false;
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

            if (!genreBooks.Values.Any(g => g.Contains(book)) || user.Favorites.Contains(book))
                return false;

            user.Favorites.Add(book);
            return true;
        }

        public bool RemoveFavorite(string userName, string book)
        {
            var user = GetAccountByUsername(userName);
            if (user?.Favorites == null || !user.Favorites.Contains(book))
                return false;

            user.Favorites.Remove(book);
            return true;
        }

        public List<string> GetGenres()
        {
            return genreBooks.Keys.ToList();
        }

        public List<string> GetBooksByGenre(string genre)
        {
            if (string.IsNullOrWhiteSpace(genre)) return new List<string>();

            return genreBooks.ContainsKey(genre) ? new List<string>(genreBooks[genre]) : new List<string>();
        }

        public List<string> SearchBooksTitle(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<string>();

            var results = new List<string>();

            foreach (var books in genreBooks.Values)
            {
                results.AddRange(books.Where(book =>
                    book.Contains(keyword, System.StringComparison.OrdinalIgnoreCase)));
            }

            return results.Distinct().ToList();
        }
    }
}
