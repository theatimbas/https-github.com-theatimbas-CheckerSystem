using PFinderCommon;
using System.Collections.Generic;

namespace ELibraryDataLogic
{
    public interface IFinderDataService
    {
        List<UserAccount> GetAccounts();
        UserAccount GetUser(string userName);
        void CreateAccount(UserAccount userAccount);
        void UpdateAccount(UserAccount userAccount);
        void RemoveAccount(string userName);
        bool RegisterAccount(string userName, string password, int age);
        bool IsUserAlreadyRegistered(string userName);
        bool ValidateAccount(string userName, string password, int age);
        bool AddFavorite(string userName, string book);
        bool RemoveFavorite(string userName, string book);
        List<string> GetFavorites(string userName);
    }
}
