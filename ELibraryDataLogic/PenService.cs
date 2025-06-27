using PFinderCommon;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ELibraryDataLogic
{
    public class LibraryDataService
    {
        private readonly IFinderDataService dataService;
        public LibraryDataService(IFinderDataService service)
        {
            this.dataService = service;
        }
        public LibraryDataService()
        {
            dataService = new PenFinderDB();
        }
        public List<UserAccount> GetAllAccounts()
        {
            return dataService.GetAccounts();
        }
        public void AddAccount(UserAccount account)
        {
            dataService.CreateAccount(account);
        }
        public void UpdateAccount(UserAccount account)
        {
            dataService.UpdateAccount(account);
        }
        public bool DeleteAccountWithUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return false;
            return dataService.DeleteAccount(username);
        }
        public bool RegisterAccount(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) return false;

            if (dataService.IsUserAlreadyRegistered(username)) return false;

            var newUser = new UserAccount
            {
                UserName = username,
                Password = password,
                Favorites = new List<string>()
            };

            dataService.CreateAccount(newUser);
            return true;
        }
        public bool Login(string username, string password)
        {
            return dataService.ValidateAccount(username, password);
        }
        public bool IsRegistered(string username)
        {
            return dataService.IsUserAlreadyRegistered(username);
        }
        public void UpdatePassword(string username, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(newPassword)) return;

            var user = dataService.GetAccountByUsername(username);
            if (user != null)
            {
                user.Password = newPassword;
                dataService.UpdateAccount(user);
            }
        }
        public List<string> GetGenres()
        {
            return dataService.GetGenres();
        }
        public List<string> GetBooksByGenre(string genre)
        {
            return string.IsNullOrWhiteSpace(genre)
                ? new List<string>()
                : dataService.GetBooksByGenre(genre);
        }
        public List<string> SearchBooks(string keyword)
        {
            return string.IsNullOrWhiteSpace(keyword)
                ? new List<string>()
                : dataService.SearchBooksTitle(keyword);
        }
        public List<string> GetFavorites(string username)
        {
            return string.IsNullOrWhiteSpace(username)
                ? new List<string>()
                : dataService.GetFavorites(username);
        }
        public bool AddFavorite(string username, string book)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(book))
                return false;

            var bookExists = dataService.GetGenres()
                                        .SelectMany(g => dataService.GetBooksByGenre(g))
                                        .Contains(book);

            if (!bookExists) 
                return false;

            var favorites = dataService.GetFavorites(username);
            if (favorites.Contains(book)) return false;

            return dataService.AddFavorite(username, book);
        }
        public bool RemoveFavorite(string username, string book)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(book))
                return false;

            return dataService.RemoveFavorite(username, book);
        }
    }
}
