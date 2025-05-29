using ELibraryBusinessDataLogic;
using System;

namespace ELibrarySystem
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("-------- Welcome to Pen Finder --------");

            while (true)
            {
                ShowMenu();

                Console.Write("\nChoose an option: ");
                string choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "0": Login(); break;
                    case "1": Register(); break;
                    case "2": BrowseGenres(); break;
                    case "3": ViewFavorites(); break;
                    case "4": UpdateFavorite(); break;
                    case "5": RemoveFavorite(); break;
                    case "6": AddBookToGenre(); break;
                    case "8": Logout(); break;
                    case "9": Console.WriteLine("Goodbye!"); return;
                    default: Console.WriteLine("Invalid option. Try again."); break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\nMain Menu");
            Console.WriteLine("[0] Login");
            Console.WriteLine("[1] Register");
            Console.WriteLine("[2] Browse Books by Genre");
            Console.WriteLine("[3] View My Favorites");
            Console.WriteLine("[4] Update Favorite Book");
            Console.WriteLine("[5] Remove a Favorite Book");
            Console.WriteLine("[6] Add Book to Genre");
            Console.WriteLine("[8] Logout");
            Console.WriteLine("[9] Exit");
        }

        static void Login()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Console.Write("Age: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine("Invalid age.");
                return;
            }

            if (E_LibraryServices.Login(username, password, age))
                Console.WriteLine("Login successful!");
            else
                Console.WriteLine("Login failed. Check credentials.");
        }

        static void Register()
        {
            Console.Write("New Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Console.Write("Age: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine("Invalid age.");
                return;
            }

            if (E_LibraryServices.RegisterAccount(username, password, age))
                Console.WriteLine("Registration successful!");
            else
                Console.WriteLine("Registration failed. Username may exist.");
        }

        static void BrowseGenres()
        {
            if (!E_LibraryServices.IsLoggedIn())
            {
                Console.WriteLine("Please log in first.");
                return;
            }

            var genres = E_LibraryServices.GetGenres();
            if (genres.Count == 0)
            {
                Console.WriteLine("No genres available.");
                return;
            }

            Console.WriteLine("\nAvailable Genres:");
            for (int i = 0; i < genres.Count; i++)
                Console.WriteLine($"[{i}] {genres[i]}");

            Console.Write("Select a genre: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 0 || index >= genres.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            var books = E_LibraryServices.GetBooksByGenre(genres[index]);
            if (books.Count == 0)
                Console.WriteLine("No books found.");
            else
                books.ForEach(b => Console.WriteLine("- " + b));
        }

        static void ViewFavorites()
        {
            if (!E_LibraryServices.IsLoggedIn())
            {
                Console.WriteLine("Please log in first.");
                return;
            }

            var favs = E_LibraryServices.MyFavorites();
            if (favs.Count == 0)
                Console.WriteLine("No favorites yet.");
            else
                favs.ForEach(f => Console.WriteLine("- " + f));
        }

        static void UpdateFavorite()
        {
            if (!E_LibraryServices.IsLoggedIn())
            {
                Console.WriteLine("Please log in first.");
                return;
            }

            Console.Write("Current book name: ");
            string oldName = Console.ReadLine();
            Console.Write("New book name: ");
            string newName = Console.ReadLine();

            if (E_LibraryServices.UpdateFavorite(oldName, newName))
                Console.WriteLine("Favorite updated.");
            else
                Console.WriteLine("Update failed.");
        }

        static void RemoveFavorite()
        {
            if (!E_LibraryServices.IsLoggedIn())
            {
                Console.WriteLine("Please log in first.");
                return;
            }

            Console.Write("Book to remove: ");
            string name = Console.ReadLine();

            if (E_LibraryServices.RemoveFromFavorites(name))
                Console.WriteLine("Book removed.");
            else
                Console.WriteLine("Book not found in favorites.");
        }

        static void AddBookToGenre()
        {
            if (!E_LibraryServices.IsLoggedIn())
            {
                Console.WriteLine("Please log in first.");
                return;
            }

            var genres = E_LibraryServices.GetGenres();
            Console.WriteLine("\nAvailable Genres:");
            for (int i = 0; i < genres.Count; i++)
                Console.WriteLine($"[{i}] {genres[i]}");

            Console.Write("Choose genre index or enter new genre: ");
            string input = Console.ReadLine()?.Trim();

            string genre = int.TryParse(input, out int idx) && idx >= 0 && idx < genres.Count
                ? genres[idx]
                : input;

            Console.Write("Enter book title to add: ");
            string title = Console.ReadLine();

            if (E_LibraryServices.AddBookToGenre(genre, title))
                Console.WriteLine($"'{title}' added to '{genre}'.");
            else
                Console.WriteLine("Failed to add book.");
        }

        static void Logout()
        {
            E_LibraryServices.Logout();
            Console.WriteLine("You have been logged out.");
        }
    }
}
