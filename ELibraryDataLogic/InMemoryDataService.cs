using System.Collections.Generic;

namespace ELibraryDataLogic
{
    public class InMemoryDataService : IFinderDataService
    {
        private List<UserAccount> accounts = new List<UserAccount>();

        public InMemoryDataService()
        {
            CreateDefaultAccounts();
        }

        private void CreateDefaultAccounts()
        {
            UserAccount user1 = new UserAccount();
            user1.UserName = "lachimolala";
            user1.Password = "nabiee";
            user1.Age = 18;
            user1.Favorites.Add("Salamasim");

            UserAccount user2 = new UserAccount();
            user2.UserName = "@spring_sensation";
            user2.Password = "password";
            user2.Age = 21;
            user2.Favorites.Add("Taste of Sky");

            accounts.Add(user1);
            accounts.Add(user2);
        }

        public void CreateAccount(UserAccount newUser)
        {
            if (GetUser(newUser.UserName) == null)
            {
                if (newUser.Favorites == null)
                {
                    newUser.Favorites = new List<string>();
                }

                accounts.Add(newUser);
            }
        }

        public List<UserAccount> GetAccounts()
        {
            return accounts;
        }

        public UserAccount GetUser(string userName)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].UserName == userName)
                {
                    return accounts[i];
                }
            }

            return null;
        }

        public bool ValidateAccount(string userName, string password, int age)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].UserName == userName &&
                    accounts[i].Password == password &&
                    accounts[i].Age == age)
                {
                    return true;
                }
            }

            return false;
        }

        public void RemoveAccount(string userName)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].UserName == userName)
                {
                    accounts.RemoveAt(i);
                    break;
                }
            }
        }

        public void UpdateAccount(UserAccount userAccount)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].UserName == userAccount.UserName)
                {
                    accounts[i].Password = userAccount.Password;
                    accounts[i].Age = userAccount.Age;
                    accounts[i].Favorites = userAccount.Favorites;
                    break;
                }
            }
        }

        public bool IsUserAlreadyRegistered(string userName)
        {
            if (GetUser(userName) != null)
            {
                return true;
            }

            return false;
        }

        public bool RegisterAccount(string userName, string password, int age)
        {
            if (IsUserAlreadyRegistered(userName) == true)
            {
                return false;
            }

            UserAccount newUser = new UserAccount();
            newUser.UserName = userName;
            newUser.Password = password;
            newUser.Age = age;
            newUser.Favorites = new List<string>();

            accounts.Add(newUser);
            return true;
        }

        public List<string> GetFavorites(string userName)
        {
            UserAccount user = GetUser(userName);

            if (user != null)
            {
                return user.Favorites;
            }

            return new List<string>();
        }

        public bool AddFavorite(string userName, string book)
        {
            UserAccount user = GetUser(userName);

            if (user == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(book))
            {
                return false;
            }

            for (int i = 0; i < user.Favorites.Count; i++)
            {
                if (user.Favorites[i] == book)
                {
                    return false;
                }
            }

            user.Favorites.Add(book);
            return true;
        }

        public bool RemoveFavorite(string userName, string book)
        {
            UserAccount user = GetUser(userName);

            if (user == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(book))
            {
                return false;
            }

            for (int i = 0; i < user.Favorites.Count; i++)
            {
                if (user.Favorites[i] == book)
                {
                    user.Favorites.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }
    }
}
