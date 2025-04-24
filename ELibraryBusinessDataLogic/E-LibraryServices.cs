using ELibraryDataLogic;
using System;
using System.Collections.Generic;

namespace ELibraryBusinessDataLogic
{
    public class E_LibraryServices
    {
        private static readonly Dictionary<string, List<string>> GenreBooks = new()
        {
            { "Fantasy", new List<string>{ "Titan Academy", "Charm Academy", "Tantei High", "Olympus Academy" } },
            { "Romance", new List<string>{ "Hell University", "University Series", "Buenaventura Series", "The Girl He Never Noticed" } },
            { "Drama", new List<string>{ "The Tempest", "A Wife's Cry", "Salamasim", "Taste of Sky" } },
            { "Science-Fiction", new List<string>{ "Ender's Game", "Project: Yngrid", "The Peculiars Tale", "Mnemosyne's Tale" } },
            { "Action", new List<string>{ "The Maze Runner", "The Hunger Games", "Divergent", "The Fifth Wave" } },
            { "Historical", new List<string>{ "I Love You Since 1892", "Reincarnated as Binibini", "Our Asymptotic Love" } }
        };

        // Per-user favorites dictionary
        private static readonly Dictionary<string, List<string>> UserFavorites = new();

        private static string currentUser;

        public static bool ValidateUser(string UserName, string UserPassword, string AgeInput)
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(UserPassword))
                return false;

            if (!int.TryParse(AgeInput, out int UserAge) || UserAge <= 17)
                return false;

            bool registered = DataFinder.RegisterAccount(UserName, UserPassword, UserAge);
            if (registered)
            {
                currentUser = UserName;
                if (!UserFavorites.ContainsKey(UserName))
                    UserFavorites[UserName] = new List<string>();
            }

            return registered;
        }

        public static bool IsUserAlreadyRegistered(string UserName)
        {
            return DataFinder.IsUserAlreadyRegistered(UserName);
        }

        public static bool BookGenre(string genre)
        {
            return GenreBooks.ContainsKey(genre);
        }

        public static Dictionary<string, List<string>> GetGenreBooks()
        {
            return GenreBooks;
        }

        public static List<string> GetBooksByGenre(string genre)
        {
            return GenreBooks.TryGetValue(genre, out var books) ? books : new List<string>();
        }

        public static List<string> MyFavorites()
        {
            if (CurrentUser != null && UserFavorites.ContainsKey(CurrentUser))
                return new List<string>(UserFavorites[CurrentUser]); // Return a copy
            return new List<string>();
        }

        public static bool AddToFavorites(string BookName)
        {
            if (string.IsNullOrWhiteSpace(BookName) || CurrentUser == null)
                return false;

            if (!UserFavorites.ContainsKey(CurrentUser))
                UserFavorites[CurrentUser] = new List<string>();

            UserFavorites[CurrentUser].Add(BookName);
            return true;
        }

        public static bool RemoveFromFavorites(string BookName)
        {
            if (CurrentUser == null || !UserFavorites.ContainsKey(CurrentUser))
                return false;

            return UserFavorites[CurrentUser].Remove(BookName);
        }

        public static bool UpdateFavorite(string OldBookName, string NewBookName)
        {
            if (string.IsNullOrWhiteSpace(NewBookName) || CurrentUser == null || !UserFavorites.ContainsKey(CurrentUser))
                return false;

            List<string> favorites = UserFavorites[CurrentUser];
            int index = favorites.IndexOf(OldBookName);
            if (index >= 0)
            {
                favorites[index] = NewBookName;
                return true;
            }

            return false;
        }
        public static void SetCurrentUser(string UserName)
        {
            currentUser = UserName;
            if (!UserFavorites.ContainsKey(UserName))
                UserFavorites[UserName] = new List<string>();
        }
        public static List<string> SearchBooks(string keyword)
        {
            List<string> results = new();

            foreach (var bookList in GenreBooks.Values)
            {
                results.AddRange(
                    bookList.FindAll(book => book.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                );
            }

            return results;
        }
        public static string CurrentUser => currentUser;

        public static bool LoginUser(string UserName, string Password, int Age)
        {
            bool valid = DataFinder.ValidateAccount(UserName, Password, Age);
            if (valid)
                currentUser = UserName;

            return valid;
        }
    }
}
