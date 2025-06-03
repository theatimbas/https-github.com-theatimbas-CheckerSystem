using PFinderCommon;
using System.Collections.Generic;

namespace ELibraryDataLogic
{
    public class PenDataService
    {
        private readonly IFinderDataService penDataService;

        public PenDataService()
        {
            // Choose which data service implementation to use here:
             penDataService = new TextFileDataService();
           // penDataService = new JsonFileDataService();
            // penDataService = new InMemoryDataService();
        }

        public List<UserAccount> GetAccounts() =>
            penDataService.GetAccounts();

        public void CreateAccount(UserAccount userAccount) =>
            penDataService.CreateAccount(userAccount);

        public void UpdateAccount(UserAccount userAccount) =>
            penDataService.UpdateAccount(userAccount);

        public void RemoveAccount(string userName) =>
            penDataService.RemoveAccount(userName);

        public bool RegisterAccount(string userName, string password, int age) =>
            penDataService.RegisterAccount(userName, password, age);

        public bool IsUserAlreadyRegistered(string userName) =>
            penDataService.IsUserAlreadyRegistered(userName);

        public bool ValidateAccount(string userName, string password, int age) =>
            penDataService.ValidateAccount(userName, password, age);

        public bool AddFavorite(string userName, string book) =>
            penDataService.AddFavorite(userName, book);

        public bool RemoveFavorite(string userName, string book) =>
            penDataService.RemoveFavorite(userName, book);

        public List<string> GetFavorites(string userName) =>
            penDataService.GetFavorites(userName);
    }
}
