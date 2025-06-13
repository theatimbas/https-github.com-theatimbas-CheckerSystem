using PFinderCommon;
using System.Collections.Generic;

namespace ELibraryDataLogic
{
    public class LibraryDataService
    {
        private readonly IFinderDataService dataService;
        private UserAccount? currentUser;

        public LibraryDataService()
        {
            // dataService = new InMemoryDataService();
            dataService = new PenFinderDB();
            // dataService = new TextFileDataService("accounts.txt");
            // dataService = new JsonFileDataService("accounts.json");
        }

        // ========== Account Operations ==========

        public List<UserAccount> GetAllAccounts()
        {
            return dataService.GetAccounts();
        }

        public void AddAccount(UserAccount userAccount)
        {
            dataService.CreateAccount(userAccount);
        }

        public void UpdateAccount(UserAccount userAccount)
        {
            dataService.UpdateAccount(userAccount);
        }

        public void RemoveAccount(string userName)
        {
            dataService.RemoveAccount(userName);
        }

        public bool Register(string userName, string password)
        {
            if (dataService.IsUserAlreadyRegistered(userName))
                return false;

            var user = new UserAccount
            {
                UserName = userName,
                Password = password,
                Favorites = new List<string>()
            };

            dataService.CreateAccount(user);
            return true;
        }

        public bool Login(string userName, string password)
        {
            if (dataService.ValidateAccount(userName, password))
            {
                currentUser = dataService.GetAccountByUsername(userName);
                return true;
            }
            return false;
        }

        public void Logout()
        {
            currentUser = null;
        }

        public bool IsRegistered(string userName)
        {
            return dataService.IsUserAlreadyRegistered(userName);
        }

        public UserAccount? GetCurrentUser()
        {
            return currentUser;
        }

        public string? GetCurrentUsername()
        {
            return currentUser?.UserName;
        }

        // ========== Favorites ==========

        public bool AddFavorite(string book)
        {
            if (currentUser == null) return false;
            return dataService.AddFavorite(currentUser.UserName, book);
        }

        public bool RemoveFavorite(string book)
        {
            if (currentUser == null) return false;
            return dataService.RemoveFavorite(currentUser.UserName, book);
        }

        public List<string> GetCurrentUserFavorites()
        {
            if (currentUser == null) return new List<string>();
            return dataService.GetFavorites(currentUser.UserName);
        }
    }
}
