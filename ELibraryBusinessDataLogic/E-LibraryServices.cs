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

        public static bool ValidateUser(string userName, string userPassword, string ageInput)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(userPassword))
                return false;

            if (!int.TryParse(ageInput, out int userAge) || userAge <= 17)
                return false;

            bool registered = ELibraryDataLogic.RegisterAccount(userName, userPassword, userAge);
            if (registered)
            {
                currentUser = userName;
                if (!UserFavorites.ContainsKey(userName))
                    UserFavorites[userName] = new List<string>();
            }

            return registered;
        }

        public static bool IsUserAlreadyRegistered(string userName)
        {
            return ELibraryDataLogic.IsUserAlreadyRegistered(userName);
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
            if (currentUser != null && UserFavorites.ContainsKey(currentUser))
                return new List<string>(UserFavorites[currentUser]); // Return a copy
            return new List<string>();
        }

        public static bool AddToFavorites(string bookName)
        {
            if (string.IsNullOrWhiteSpace(bookName) || currentUser == null)
                return false;

            if (!UserFavorites.ContainsKey(currentUser))
                UserFavorites[currentUser] = new List<string>();

            UserFavorites[currentUser].Add(bookName);
            return true;
        }

        public static bool RemoveFromFavorites(string bookName)
        {
            if (currentUser == null || !UserFavorites.ContainsKey(currentUser))
                return false;

            return UserFavorites[currentUser].Remove(bookName);
        }

        public static bool UpdateFavorite(string oldBookName, string newBookName)
        {
            if (string.IsNullOrWhiteSpace(newBookName) || currentUser == null || !UserFavorites.ContainsKey(currentUser))
                return false;

            List<string> favorites = UserFavorites[currentUser];
            int index = favorites.IndexOf(oldBookName);
            if (index >= 0)
            {
                favorites[index] = newBookName;
                return true;
            }

            return false;
        }
    }
}
