using PFinderCommon;
using System.Collections.Generic;
using System.Linq;

namespace ELibraryDataLogic
{
    public class LibraryDataService
    {
        private readonly IFinderDataService dataService;
        private UserAccount? currentUser;

        private readonly Dictionary<string, List<string>> genres = new Dictionary<string, List<string>>
        {
            { "Fantasy", new List<string>{ "Titan Academy", "Charm Academy", "Tantei High", "Olympus Academy" } },
            { "Romance", new List<string>{ "Hell University", "University Series", "Buenaventura Series", "The Girl He Never Noticed" } },
            { "Drama", new List<string>{ "The Tempest", "A Wife's Cry", "Salamasim", "Taste of Sky" } },
            { "Science-Fiction", new List<string>{ "Ender's Game", "Project: Yngrid", "The Peculiars Tale", "Mnemosyne's Tale" } },
            { "Action", new List<string>{ "The Maze Runner", "The Hunger Games", "Divergent", "The Fifth Wave" } },
            { "Historical", new List<string>{ "I Love You Since 1892", "Reincarnated as Binibini", "Our Asymptotic Love" } }
        };

        public LibraryDataService()
        {
            // Choose your data backend
            // dataService = new InMemoryDataService();
             dataService = new PenFinderDB();
            // dataService = new TextFileDataService("accounts.txt");
             // dataService = new JsonFileDataService("accounts.json");
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

        public bool RegisterAccount(string userName, string password)
        {
            if (dataService.IsUserAlreadyRegistered(userName))
                return false;

            var user = new UserAccount
            {
                UserName = userName,
                Password = password,
                Favorites = new List<string>()
            };

            dataService.CreateAccount(user);
            return true;
        }

        public bool Login(string userName, string password)
        {
            if (dataService.ValidateAccount(userName, password))
            {
                currentUser = dataService.GetAccountByUsername(userName);
                return true;
            }
            return false;
        }

        public void Logout()
        {
            currentUser = null;
        }

        public bool IsRegistered(string userName)
        {
            return dataService.IsUserAlreadyRegistered(userName);
        }

        public UserAccount? GetCurrentUser()
        {
            return currentUser;
        }

        public string? GetCurrentUsername()
        {
            return currentUser?.UserName;
        }

        public List<string> GetGenres()
        {
            return genres.Keys.ToList();
        }

        public List<string> GetBooksByGenre(string genre)
        {
            return genres.TryGetValue(genre, out var books) ? books : new List<string>();
        }

        public bool AddFavorite(string book)
        {
            if (currentUser == null)
                return false;

            bool bookExists = genres.Values.Any(bookList => bookList.Contains(book));
            if (!bookExists)
                return false;

            var existingFavorites = dataService.GetFavorites(currentUser.UserName);
            if (existingFavorites.Contains(book))
                return false;

            return dataService.AddFavorite(currentUser.UserName, book);
        }

        public bool RemoveFavorites(string book)
        {
            if (currentUser == null) return false;
            return dataService.RemoveFavorite(currentUser.UserName, book);
        }

        public List<string> MyFavorites()
        {
            if (currentUser == null) return new List<string>();
            return dataService.GetFavorites(currentUser.UserName);
        }

        public void UpdatePassword(string newPassword)
        {
            if (currentUser != null)
            {
                currentUser.Password = newPassword;
                dataService.UpdateAccount(currentUser);
            }
        }
        public List<string> SearchBooks(string keyword)
        {
            return dataService.SearchBooksTitle(keyword);
        }

    }
}
