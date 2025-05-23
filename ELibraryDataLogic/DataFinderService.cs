using System.Collections.Generic;
using System.Linq;

namespace ELibraryDataLogic
{
    public class DataFinder
    {
        private readonly IFinderDataService dataService;
        private readonly Dictionary<string, List<string>> genreBooks = new()
        {
            { "Fantasy", new List<string>{ "Titan Academy", "Charm Academy", "Tantei High", "Olympus Academy" } },
            { "Romance", new List<string>{ "Hell University", "University Series", "Buenaventura Series", "The Girl He Never Noticed" } },
            { "Drama", new List<string>{ "The Tempest", "A Wife's Cry", "Salamasim", "Taste of Sky" } },
            { "Science-Fiction", new List<string>{ "Ender's Game", "Project: Yngrid", "The Peculiars Tale", "Mnemosyne's Tale" } },
            { "Action", new List<string>{ "The Maze Runner", "The Hunger Games", "Divergent", "The Fifth Wave" } },
            { "Historical", new List<string>{ "I Love You Since 1892", "Reincarnated as Binibini", "Our Asymptotic Love" } }
        };

        public DataFinder()
        {
            dataService = new InMemoryDataService();
        }
        public List<UserAccount> GetAllAccounts()
        {
            return dataService.GetAccounts();
        }
        public void AddAccount(UserAccount userAccount)
        {
            dataService.CreateAccount(userAccount);
        }
        public void UpdateAccount(UserAccount userAccount)
        {
            dataService.UpdateAccount(userAccount);
        }
        public void RemoveAccount(string userName)
        {
            dataService.RemoveAccount(userName);
        }
        public bool RegisterAccount(string userName, string password, int age)
        {
            return dataService.RegisterAccount(userName, password, age);
        }
        public bool IsUserAlreadyRegistered(string userName)
        {
            return dataService.IsUserAlreadyRegistered(userName);
        }
        public bool ValidateAccount(string userName, string password, int age)
        {
            return dataService.ValidateAccount(userName, password, age);
        }
        public List<string> GetGenres()
        {
            return genreBooks.Keys.ToList();
        }
        public List<string> GetBooksByGenre(string genre)
        {
            if (genreBooks.ContainsKey(genre))
                return new List<string>(genreBooks[genre]);
            return new List<string>();
        }
        public bool AddBookToGenre(string genre, string bookTitle)
        {
            if (string.IsNullOrWhiteSpace(genre) || string.IsNullOrWhiteSpace(bookTitle))
                return false;

            if (!genreBooks.ContainsKey(genre))
            {
                genreBooks[genre] = new List<string>();
            }

            if (!genreBooks[genre].Contains(bookTitle))
            {
                genreBooks[genre].Add(bookTitle);
                return true;
            }

            return false;
        }
    }
}
