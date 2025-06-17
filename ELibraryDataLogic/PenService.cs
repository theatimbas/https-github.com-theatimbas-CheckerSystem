using PFinderCommon;
using System.Collections.Generic;
using System.Linq;

namespace ELibraryDataLogic
{
    public class LibraryDataService
    {
        private readonly IFinderDataService dataService;
        private UserAccount? CurrentUser;

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
            // dataService = new InMemoryDataService();
            // dataService = new TextFileDataService("accounts.txt");
            // dataService = new JsonFileDataService("accounts.json");
            dataService = new PenFinderDB(); 
        }

        public List<UserAccount> GetAllAccounts() => dataService.GetAccounts();

        public void AddAccount(UserAccount UserAccount) => dataService.CreateAccount(UserAccount);

        public void UpdateAccount(UserAccount UserAccount) => dataService.UpdateAccount(UserAccount);

        public void DeleteAccount(string UserName) => dataService.DeleteAccount(UserName);

        public bool RegisterAccount(string UserName, string Password)
        {
            if (dataService.IsUserAlreadyRegistered(UserName))
                return false;

            var user = new UserAccount
            {
                UserName = UserName,
                Password = Password,
                Favorites = new List<string>()
            };

            dataService.CreateAccount(user);
            return true;
        }

        public bool Login(string UserName, string Password)
        {
            if (dataService.ValidateAccount(UserName, Password))
            {
                CurrentUser = dataService.GetAccountByUsername(UserName);
                return true;
            }
            return false;
        }

        public void Logout() => CurrentUser = null;

        public bool IsRegistered(string UserName) => dataService.IsUserAlreadyRegistered(UserName);

        public bool IsLoggedIn() => CurrentUser != null;

        public UserAccount? GetCurrentUser() => CurrentUser;

        public string? GetCurrentUsername() => CurrentUser?.UserName;

        public List<string> GetGenres() => genres.Keys.ToList();

        public List<string> GetBooksByGenre(string genre)
        {
            return genres.TryGetValue(genre, out var books) ? books : new List<string>();
        }

        public bool AddFavorite(string book)
        {
            if (CurrentUser == null)
                return false;

            bool BookExist = genres.Values.Any(BookList => BookList.Contains(book));
            if (!BookExist)
                return false;

            var ExistingFavorites = dataService.GetFavorites(CurrentUser.UserName);
            if (ExistingFavorites.Contains(book))
                return false;

            return dataService.AddFavorite(CurrentUser.UserName, book);
        }

        public bool RemoveFavorite(string book)
        {
            if (CurrentUser == null) return false;
            return dataService.RemoveFavorite(CurrentUser.UserName, book);
        }
        public bool RemoveFavorites(string book) => RemoveFavorite(book);

        public List<string> MyFavorites()
        {
            if (CurrentUser == null) return new List<string>();
            return dataService.GetFavorites(CurrentUser.UserName);
        }

        public void UpdatePassword(string NewPassword)
        {
            if (CurrentUser != null && !string.IsNullOrWhiteSpace(NewPassword))
            {
                CurrentUser.Password = NewPassword;
                dataService.UpdateAccount(CurrentUser);
            }
        }

        public List<string> SearchBooks(string KeyWord)
        {
            if (string.IsNullOrWhiteSpace(KeyWord)) return new List<string>();
            return dataService.SearchBooksTitle(KeyWord);
        }

        public bool DeleteCurrentAccount()
        {
            if (CurrentUser == null)
                return false;

            string UserName = CurrentUser.UserName;
            dataService.DeleteAccount(UserName);
            CurrentUser = null;
            return true;
        }
    }
}
