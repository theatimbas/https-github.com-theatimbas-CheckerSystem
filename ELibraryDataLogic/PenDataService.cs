using System.Collections.Generic;

namespace ELibraryDataLogic
{
    public class PenDataService
    {
        private IFinderDataService penDataService;

        public PenDataService()
        {
            
            //penDataService = new TextFileDataService();
            penDataService = new JsonFileDataService();
            //penDataService = new InMemoryDataService();
        }

        public List<UserAccount> GetAccounts() =>
            penDataService.GetAccounts();

        public void CreateAccount(UserAccount userAccount) =>
            penDataService.CreateAccount(userAccount);

        public void UpdateAccount(UserAccount userAccount) =>
            penDataService.UpdateAccount(userAccount);

        public void RemoveAccount(UserAccount userAccount) =>
            penDataService.RemoveAccount(userAccount.UserName);

        public bool RegisterAccount(UserAccount userAccount) =>
            penDataService.RegisterAccount(userAccount.UserName, userAccount.Password, userAccount.Age);

        public bool IsUserAlreadyRegistered(UserAccount userAccount) =>
            penDataService.IsUserAlreadyRegistered(userAccount.UserName);

        public bool ValidateAccount(UserAccount userAccount) =>
            penDataService.ValidateAccount(userAccount.UserName, userAccount.Password, userAccount.Age);
    }
}
