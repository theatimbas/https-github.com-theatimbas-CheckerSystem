using PFinderCommon;
using System.Collections.Generic;

namespace ELibraryDataLogic
{
    public interface IFinderDataService
    {
        List<UserAccount> GetAccounts();
        UserAccount? GetAccountByUsername(string userName);
        void CreateAccount(UserAccount user);
        void UpdateAccount(UserAccount user);
        bool DeleteAccount(string userName);
        bool ValidateAccount(string userName, string password);
        bool IsUserAlreadyRegistered(string userName);
        List<string> GetFavorites(string userName);
        bool AddFavorite(string userName, string book);
        bool RemoveFavorite(string userName, string book);
        List<string> GetGenres();
        List<string> GetBooksByGenre(string genre);
        List<string> SearchBooksTitle(string keyword);
    }
}
