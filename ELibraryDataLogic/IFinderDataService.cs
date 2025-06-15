using System.Collections.Generic;
using PFinderCommon;

public interface IFinderDataService
{
    List<UserAccount> GetAccounts();
    UserAccount GetAccountByUsername(string userName);
    void CreateAccount(UserAccount userAccount);
    void UpdateAccount(UserAccount userAccount);
    void RemoveAccount(string userName);
    bool ValidateAccount(string userName, string password);
    bool IsUserAlreadyRegistered(string userName);
    List<string> GetFavorites(string userName);
    bool AddFavorite(string userName, string bookTitle);
    bool RemoveFavorite(string userName, string bookTitle);
    List<string> GetGenres();
    List<string> GetBooksByGenre(string genre);
    List<string> SearchBooksTitle(string keyword);
}
