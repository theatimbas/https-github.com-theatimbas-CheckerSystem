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
            return users;
        }

        public UserAccount GetAccountByUsername(string username)
        {
            return users.FirstOrDefault(u => u.UserName == username);
        }

        public void CreateAccount(UserAccount user)
        {
            if (!IsUserAlreadyRegistered(user.UserName))
            {
                users.Add(user);
            }
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

        public void RemoveAccount(string userName)
        {
            var user = GetAccountByUsername(userName);
            if (user != null)
            {
                users.Remove(user);
            }
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
            return user?.Favorites ?? new List<string>();
        }

        public bool AddFavorite(string userName, string book)
        {
            var user = GetAccountByUsername(userName);
            if (user == null) return false;
            user.Favorites ??= new List<string>();
            bool bookExists = genreBooks.Values.Any(list => list.Contains(book));
            if (!bookExists) return false;

            if (user.Favorites.Contains(book)) return false;

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
            return genreBooks.ContainsKey(genre) ? genreBooks[genre] : new List<string>();
        }
        public List<string> SearchBooksTitle(string keyword)
        {
            var results = new List<string>();

            foreach (var books in genreBooks.Values)
            {
                results.AddRange(books.Where(book =>
                    book.Contains(keyword, System.StringComparison.OrdinalIgnoreCase)));
            }
            return results;
        }
    }
}
