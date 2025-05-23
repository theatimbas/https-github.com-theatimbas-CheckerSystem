using ELibraryDataLogic;
using System.Collections.Generic;
using System.Linq;

namespace ELibraryBusinessDataLogic
{
    public static class E_LibraryServices
    {
        private static readonly InMemoryDataService dataService = new();
        private static string currentUser;

        private static readonly Dictionary<string, List<string>> booksByGenre = new()
        {
            { "Fantasy", new List<string> { "Harry Potter", "The Hobbit", "Percy Jackson" } },
            { "Science Fiction", new List<string> { "Dune", "Ender's Game", "The Martian" } },
            { "Mystery", new List<string> { "Gone Girl", "The Girl with the Dragon Tattoo" } },
            { "Romance", new List<string> { "Pride and Prejudice", "Me Before You" } },
            { "Non-Fiction", new List<string> { "Sapiens", "Educated" } }
        };

        public static bool ValidateAccount(string username, string password, int age)
        {
            bool valid = dataService.ValidateAccount(username, password, age);
            if (valid)
                currentUser = username;

            return valid;
        }

        public static bool RegisterAccount(string username, string password, int age)
        {
            return dataService.RegisterAccount(username, password, age);
        }

        public static void Logout()
        {
            currentUser = null;
        }

        public static List<string> MyFavorites()
        {
            return currentUser != null
                ? dataService.GetFavorites(currentUser)
                : new List<string>();
        }

        public static bool UpdateFavorite(string oldBook, string newBook)
        {
            if (currentUser == null) return false;

            var favorites = dataService.GetFavorites(currentUser);
            if (!favorites.Contains(oldBook) || string.IsNullOrWhiteSpace(newBook))
                return false;

            dataService.RemoveFavorite(currentUser, oldBook);
            return dataService.AddFavorite(currentUser, newBook);
        }

        public static bool RemoveFromFavorites(string book)
        {
            return currentUser != null && dataService.RemoveFavorite(currentUser, book);
        }

        public static bool AddToFavorites(string book)
        {
            return currentUser != null && dataService.AddFavorite(currentUser, book);
        }

        public static List<string> GetGenres()
        {
            return booksByGenre.Keys.ToList();
        }

        public static List<string> GetBooksByGenre(string genre)
        {
            return booksByGenre.TryGetValue(genre, out var books) ? books : new List<string>();
        }
    }
}
