using System;
using System.Collections.Generic;
using PFinderCommon;
using ELibraryDataLogic;

public static class E_LibraryServices
{
    private static IFinderDataService dataService = new PenFinderDB();
    private static UserAccount CurrentUser = null;

    public static bool RegisterAccount(string UserName, string Password)
    {
        if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            return false;

        if (dataService.GetAccountByUsername(UserName) != null)
            return false;

        var NewUser = new UserAccount
        {
            UserName = UserName,
            Password = Password,
            Favorites = new List<string>()
        };

        dataService.CreateAccount(NewUser);
        return true;
    }

    public static bool Login(string UserName, string Password)
    {
        if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            return false;

        var user = dataService.GetAccountByUsername(UserName);
        if (user != null && user.Password == Password)
        {
            CurrentUser = user;
            return true;
        }
        return false;
    }

    public static void Logout()
    {
        CurrentUser = null;
    }

    public static List<string> MyFavorites()
    {
        if (CurrentUser == null) return new List<string>();
        CurrentUser.Favorites = dataService.GetFavorites(CurrentUser.UserName);
        return CurrentUser.Favorites;
    }

    public static bool AddFavorite(string book)
    {
        return AddBookToUser(book);
    }

    public static bool RemoveFavorites(string book)
    {
        return RemoveBookFromUser(book);
    }

    public static void UpdatePassword(string NewPassword)
    {
        if (CurrentUser != null && !string.IsNullOrWhiteSpace(NewPassword))
        {
            CurrentUser.Password = NewPassword;
            dataService.UpdateAccount(CurrentUser);
        }
    }

    public static List<string> GetGenres()
    {
        return dataService.GetGenres();
    }

    public static List<string> GetBooksByGenre(string genre)
    {
        return dataService.GetBooksByGenre(genre);
    }

    public static List<string> SearchBooksByTitle(string KeyWord)
    {
        if (string.IsNullOrWhiteSpace(KeyWord)) return new List<string>();
        return dataService.SearchBooksTitle(KeyWord);
    }
    public static bool DeleteAccount()
    {
        if (CurrentUser == null)
            return false;

        string UserName = CurrentUser.UserName;
        bool DeletedAccount = dataService.DeleteAccount(UserName);
        if (DeletedAccount)
            CurrentUser = null;
        return DeletedAccount;
    }

    private static bool AddBookToUser(string book)
    {
        if (CurrentUser == null || string.IsNullOrWhiteSpace(book)) return false;

        bool BookExist = false;
        foreach (var genre in dataService.GetGenres())
        {
            var books = dataService.GetBooksByGenre(genre);
            if (books.Contains(book))
            {
                BookExist = true;
                break;
            }
        }
        if (!BookExist) return false;

        bool AddedBook = dataService.AddFavorite(CurrentUser.UserName, book);
        if (AddedBook && !CurrentUser.Favorites.Contains(book))
            CurrentUser.Favorites.Add(book);

        return AddedBook;
    }

    private static bool RemoveBookFromUser(string book)
    {
        if (CurrentUser == null || string.IsNullOrWhiteSpace(book)) return false;

        bool RemovedBook = dataService.RemoveFavorite(CurrentUser.UserName, book);
        if (RemovedBook)
            CurrentUser.Favorites.Remove(book);

        return RemovedBook;
    }
}
