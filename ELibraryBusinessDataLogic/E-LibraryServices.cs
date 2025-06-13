using System;
using System.Collections.Generic;
using PFinderCommon;
using ELibraryDataLogic;

public static class E_LibraryServices
{
    private static IFinderDataService dataService = new PenFinderDB();

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
        return currentUser?.Favorites ?? new List<string>();
    }

    public static bool RenameBookInUser(string oldName, string newName)
    {
        if (currentUser?.Favorites == null) return false;

        int index = currentUser.Favorites.IndexOf(oldName);
        if (index >= 0 && !currentUser.Favorites.Contains(newName))
        {
            currentUser.Favorites[index] = newName;
            dataService.UpdateAccount(currentUser);
            return true;
        }
        return false;
    }

    public static bool RemoveBookFromUser(string book)
    {
        if (currentUser?.Favorites != null && currentUser.Favorites.Contains(book))
        {
            currentUser.Favorites.Remove(book);
            dataService.UpdateAccount(currentUser);
            return true;
        }
        return false;
    }

    public static bool AddBookToUser(string book)
    {
        if (currentUser == null) return false;

        currentUser.Favorites ??= new List<string>();
        if (!currentUser.Favorites.Contains(book))
        {
            currentUser.Favorites.Add(book);
            dataService.UpdateAccount(currentUser);
            return true;
        }
        return false;
    }

    public static bool AddFavorite(string book)
    {
        return AddBookToUser(book);
    }

    public static bool RemoveFromFavorites(string book)
    {
        return RemoveBookFromUser(book);
    }
}
