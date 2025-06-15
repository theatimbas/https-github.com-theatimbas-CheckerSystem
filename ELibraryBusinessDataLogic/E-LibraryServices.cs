using System;
using System.Collections.Generic;
using PFinderCommon;
using ELibraryDataLogic;

public static class E_LibraryServices
{
    private static IFinderDataService dataService = new PenFinderDB(); // You can replace with JsonDataFileService, etc.
    private static UserAccount currentUser = null;

    public static bool RegisterAccount(string username, string password)
    {
        if (dataService.GetAccountByUsername(username) != null)
            return false;

        var newUser = new UserAccount
        {
            UserName = username,
            Password = password,
            Favorites = new List<string>(),
        };

        dataService.CreateAccount(newUser);
        return true;
    }

    public static bool Login(string username, string password)
    {
        var user = dataService.GetAccountByUsername(username);
        if (user != null && user.Password == password)
        {
            currentUser = user;
            return true;
        }
        return false;
    }

    public static void Logout()
    {
        currentUser = null;
    }

    public static List<string> MyFavorites()
    {
        if (currentUser == null) return new List<string>();
        currentUser.Favorites = dataService.GetFavorites(currentUser.UserName);
        return currentUser.Favorites;
    }

    public static bool AddFavorite(string book)
    {
        return AddBookToUser(book);
    }

    public static bool RemoveFavorites(string book)
    {
        return RemoveBookFromUser(book);
    }

    public static void UpdatePassword(string newPassword)
    {
        if (currentUser != null)
        {
            currentUser.Password = newPassword;
            dataService.UpdateAccount(currentUser);
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

    // Internal helpers
    private static bool AddBookToUser(string book)
    {
        if (currentUser == null) return false;

        // Check if book exists in all genres before adding
        bool exists = false;
        var genres = dataService.GetGenres();
        foreach (var genre in genres)
        {
            var books = dataService.GetBooksByGenre(genre);
            if (books.Contains(book))
            {
                exists = true;
                break;
            }
        }

        if (!exists) return false;

        bool added = dataService.AddFavorite(currentUser.UserName, book);
        if (added && !currentUser.Favorites.Contains(book))
            currentUser.Favorites.Add(book);
        return added;
    }

    private static bool RemoveBookFromUser(string book)
    {
        if (currentUser == null) return false;
        bool removed = dataService.RemoveFavorite(currentUser.UserName, book);
        if (removed)
            currentUser.Favorites.Remove(book);
        return removed;
    }
    public static List<string> SearchBooksByTitle(string keyword)
    {
        return dataService.SearchBooksTitle(keyword);
    }

}
