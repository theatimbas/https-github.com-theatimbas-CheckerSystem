using System.Collections.Generic;
using System.Linq;
using PFinderCommon;

namespace ELibraryDataLogic
{
    public class InMemoryDataService : IFinderDataService
    {
        private readonly List<UserAccount> users = new List<UserAccount>();
        private readonly Dictionary<string, List<string>> genreBooks = new Dictionary<string, List<string>>();

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
            if (user != null)
            {
                user.Favorites ??= new List<string>();
                if (!user.Favorites.Contains(book))
                {
                    user.Favorites.Add(book);
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
                return true;
            }
            return false;
        }
    }
}
