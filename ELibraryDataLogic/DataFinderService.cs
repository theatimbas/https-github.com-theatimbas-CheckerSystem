using System.Collections.Generic;
using System.Linq;
using PFinderCommon;

namespace ELibraryDataLogic
{
    public class DataFinder
    {
        private readonly IFinderDataService dataService;

        private readonly Dictionary<string, List<string>> genreBooks = new()
        {
            { "Fantasy", new List<string>{ "Titan Academy", "Charm Academy" } },
            { "Romance", new List<string>{ "Hell University", "University Series" } },
            { "Drama", new List<string>{ "The Tempest", "A Wife's Cry" } },
            { "Science-Fiction", new List<string>{ "Ender's Game", "Project: Yngrid" } },
            { "Action", new List<string>{ "The Maze Runner", "The Hunger Games" } },
            { "Historical", new List<string>{ "I Love You Since 1892" } }
        };

        public DataFinder() => dataService = new TextFileDataService();

        public bool RegisterAccount(string userName, string password, int age) =>
            dataService.RegisterAccount(userName, password, age);

        public bool ValidateAccount(string userName, string password, int age) =>
            dataService.ValidateAccount(userName, password, age);

        public List<string> GetGenres() =>
            genreBooks.Keys.ToList();

        public List<string> GetBooksByGenre(string genre) =>
            genreBooks.TryGetValue(genre, out var books) ? books : new List<string>();

        public List<string> GetFavorites(string userName) =>
            dataService.GetFavorites(userName);

        public bool AddFavorite(string userName, string book) =>
            dataService.AddFavorite(userName, book);

        public bool RemoveFavorite(string userName, string book) =>
            dataService.RemoveFavorite(userName, book);
    }
}

