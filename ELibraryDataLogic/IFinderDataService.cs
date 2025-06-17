using PFinderCommon;
using System.Collections.Generic;

namespace ELibraryDataLogic
{
    public interface IFinderDataService
    {
        List<UserAccount> GetAccounts();
        UserAccount GetAccountByUsername(string UserName);
        void CreateAccount(UserAccount user);
        void UpdateAccount(UserAccount user);
        bool DeleteAccount(string UserName);
        bool ValidateAccount(string UserName, string Password);
        bool IsUserAlreadyRegistered(string UserName);
        List<string> GetFavorites(string UserName);
        bool AddFavorite(string UserName, string book);
        bool RemoveFavorite(string UserName, string book);
        List<string> GetGenres();
        List<string> GetBooksByGenre(string genre);
        List<string> SearchBooksTitle(string KeyWord);
    }
}
