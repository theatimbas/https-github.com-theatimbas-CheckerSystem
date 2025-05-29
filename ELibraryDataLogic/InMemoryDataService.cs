using System.Collections.Generic;

namespace ELibraryDataLogic
{
    public class InMemoryDataService : IFinderDataService
    {
        private readonly List<UserAccount> accounts = new();

        public InMemoryDataService()
        {
            CreateDefaultAccounts();
        }

        private void CreateDefaultAccounts()
        {
            accounts.Add(new UserAccount
            {
                UserName = "lachimolala",
                Password = "nabiee",
                Age = 18,
                Favorites = new List<string> { "Salamasim" }
            });

            accounts.Add(new UserAccount
            {
                UserName = "@spring_sensation",
                Password = "password",
                Age = 21,
                Favorites = new List<string> { "Taste of Sky" }
            });
        }

        public void CreateAccount(UserAccount newUser)
        {
            if (GetUser(newUser.UserName) == null)
            {
                newUser.Favorites ??= new List<string>();
                accounts.Add(newUser);
            }
        }

        public List<UserAccount> GetAccounts() => accounts;

        public UserAccount GetUser(string userName) =>
            accounts.Find(u => u.UserName == userName);

        public bool ValidateAccount(string userName, string password, int age) =>
            accounts.Exists(u => u.UserName == userName && u.Password == password && u.Age == age);

        public void RemoveAccount(string userName)
        {
            var user = GetUser(userName);
            if (user != null)
                accounts.Remove(user);
        }

        public void UpdateAccount(UserAccount updatedUser)
        {
            var user = GetUser(updatedUser.UserName);
            if (user != null)
            {
                user.Password = updatedUser.Password;
                user.Age = updatedUser.Age;
                user.Favorites = updatedUser.Favorites;
            }
        }

        public bool IsUserAlreadyRegistered(string userName) =>
            GetUser(userName) != null;

        public bool RegisterAccount(string userName, string password, int age)
        {
            if (IsUserAlreadyRegistered(userName)) return false;

            accounts.Add(new UserAccount
            {
                UserName = userName,
                Password = password,
                Age = age,
                Favorites = new List<string>()
            });

            return true;
        }

        public List<string> GetFavorites(string userName) =>
            GetUser(userName)?.Favorites ?? new List<string>();

        public bool AddFavorite(string userName, string book)
        {
            var user = GetUser(userName);
            if (user == null || string.IsNullOrWhiteSpace(book) || user.Favorites.Contains(book))
                return false;

            user.Favorites.Add(book);
            return true;
        }

        public bool RemoveFavorite(string userName, string book)
        {
            var user = GetUser(userName);
            if (user == null || string.IsNullOrWhiteSpace(book)) return false;

            return user.Favorites.Remove(book);
        }
    }
}
