using ELibraryDataLogic;
using System.Collections.Generic;
using PFinderCommon;

namespace ELibraryBusinessDataLogic
{
    public static class E_LibraryServices
    {
        private static IFinderDataService dataService = new TextFileDataService();

        private static UserAccount currentUser = null;

        private static readonly Dictionary<string, List<string>> genres = new()
        {
            { "Fantasy", new List<string>{ "Titan Academy", "Charm Academy", "Tantei High", "Olympus Academy" } },
            { "Romance", new List<string>{ "Hell University", "University Series", "Buenaventura Series", "The Girl He Never Noticed" } },
            { "Drama", new List<string>{ "The Tempest", "A Wife's Cry", "Salamasim", "Taste of Sky" } },
            { "Science-Fiction", new List<string>{ "Ender's Game", "Project: Yngrid", "The Peculiars Tale", "Mnemosyne's Tale" } },
            { "Action", new List<string>{ "The Maze Runner", "The Hunger Games", "Divergent", "The Fifth Wave" } },
            { "Historical", new List<string>{ "I Love You Since 1892", "Reincarnated as Binibini", "Our Asymptotic Love" } }
        };

        public static bool Login(string username, string password, int age)
        {
            if (dataService.ValidateAccount(username, password, age))
            {
                currentUser = dataService.GetUser(username);
                return true;
            }
            return false;
        }

        public static void Logout() => currentUser = null;

        public static bool IsLoggedIn() => currentUser != null;

        public static bool RegisterAccount(string username, string password, int age) =>
            dataService.RegisterAccount(username, password, age);

        public static List<string> GetGenres() => new(genres.Keys);

        public static List<string> GetBooksByGenre(string genre) =>
            genres.ContainsKey(genre) ? genres[genre] : new List<string>();

        public static bool AddBookToGenre(string genre, string book)
        {
            if (!genres.ContainsKey(genre))
                genres[genre] = new List<string>();

            if (!genres[genre].Contains(book))
            {
                genres[genre].Add(book);
                return true;
            }
            return false;
        }

        public static List<string> MyFavorites() =>
            currentUser?.Favorites ?? new List<string>();

        public static bool UpdateFavorite(string oldName, string newName)
        {
            if (currentUser != null &&
                currentUser.Favorites.Contains(oldName) &&
                !currentUser.Favorites.Contains(newName))
            {
                int index = currentUser.Favorites.IndexOf(oldName);
                currentUser.Favorites[index] = newName;
                dataService.UpdateAccount(currentUser);
                return true;
            }
            return false;
        }

        public static bool RemoveFromFavorites(string book)
        {
            if (currentUser != null && currentUser.Favorites.Remove(book))
            {
                dataService.UpdateAccount(currentUser);
                return true;
            }
            return false;
        }
    }
}
