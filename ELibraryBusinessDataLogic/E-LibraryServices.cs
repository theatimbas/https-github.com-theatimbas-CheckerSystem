using System;
using System.Collections.Generic;
using PFinderCommon;
using ELibraryDataLogic;

public static class E_LibraryServices
{
    private static IFinderDataService dataService = new PenFinderDB();

    private static UserAccount? CurrentUser = null;

    public static bool RegisterAccount(string userName, string password)
    {
        if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            return false;

        if (dataService.GetAccountByUsername(userName) != null)
            return false;

        var newUser = new UserAccount
        {
            UserName = userName,
            Password = password,
            Favorites = new List<string>()
        };

        dataService.CreateAccount(newUser);
        return true;
    }
    public static bool Login(string userName, string password)
    {
        if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            return false;

        var user = dataService.GetAccountByUsername(userName);
        if (user != null && user.Password == password)
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
        if (!IsLoggedIn()) return new List<string>();

        CurrentUser!.Favorites = dataService.GetFavorites(CurrentUser.UserName);
        return CurrentUser.Favorites;
    }
    public static bool AddFavorite(string bookTitle)
    {
        return AddBookToUser(bookTitle);
    }
    public static bool RemoveFavorite(string bookTitle)
    {
        return RemoveBookFromUser(bookTitle);
    }
    public static void UpdatePassword(string newPassword)
    {
        if (!IsLoggedIn() || string.IsNullOrWhiteSpace(newPassword)) return;

        CurrentUser!.Password = newPassword;
        dataService.UpdateAccount(CurrentUser);
    }
    public static List<string> GetGenres()
    {
        return dataService.GetGenres();
    }
    public static List<string> GetBooksByGenre(string genre)
    {
        return string.IsNullOrWhiteSpace(genre)
            ? new List<string>()
            : dataService.GetBooksByGenre(genre);
    }
    public static List<string> SearchBooksByTitle(string keyword)
    {
        return string.IsNullOrWhiteSpace(keyword)
            ? new List<string>()
            : dataService.SearchBooksTitle(keyword);
    }
    public static bool DeleteAccount()
    {
        if (!IsLoggedIn()) return false;

        string userName = CurrentUser!.UserName;
        bool deleted = dataService.DeleteAccount(userName);

        if (deleted) Logout();

        return deleted;
    }
    private static bool AddBookToUser(string bookTitle)
    {
        if (!IsLoggedIn() || string.IsNullOrWhiteSpace(bookTitle)) return false;

        bool bookExists = false;
        foreach (var genre in dataService.GetGenres())
        {
            if (dataService.GetBooksByGenre(genre).Contains(bookTitle))
            {
                bookExists = true;
                break;
            }
        }

        if (!bookExists) return false;

        bool added = dataService.AddFavorite(CurrentUser!.UserName, bookTitle);
        if (added && !CurrentUser.Favorites.Contains(bookTitle))
            CurrentUser.Favorites.Add(bookTitle);

        return added;
    }
    private static bool RemoveBookFromUser(string bookTitle)
    {
        if (!IsLoggedIn() || string.IsNullOrWhiteSpace(bookTitle)) return false;

        bool removed = dataService.RemoveFavorite(CurrentUser!.UserName, bookTitle);
        if (removed)
            CurrentUser.Favorites.Remove(bookTitle);

        return removed;
    }
    private static bool IsLoggedIn()
    {
        return CurrentUser?.UserName != null;
    }
}
