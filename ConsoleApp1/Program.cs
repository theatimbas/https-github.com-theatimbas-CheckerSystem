using ELibraryDataLogic;
using PFinderCommon;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Create Account\n2. Login\n3. Update Password\n4. Exit");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1": Register(); break;
                case "2": if (Login()) ShowMainMenu(); break;
                case "3": UpdatePassword(); break;
                case "4": return;
                default:
                    Console.WriteLine("Invalid option. Press any key to try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void Register()
    {
        Console.Clear();
        Console.Write("Enter username: ");
        string username = Console.ReadLine()?.Trim();
        Console.Write("Enter password: ");
        string password = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Username and password cannot be blank.");
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
            return;
        }

        bool success = E_LibraryServices.RegisterAccount(username, password);
        Console.WriteLine(success ? "Registration successful." : "Username already exists or invalid input.");

        if (success)
        {
            E_LibraryServices.Login(username, password);
            Console.WriteLine("Auto-logged in after registration.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            ShowMainMenu();
        }
        else
        {
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }
    }

    static bool Login()
    {
        Console.Clear();
        Console.Write("Enter username: ");
        string username = Console.ReadLine()?.Trim();
        Console.Write("Enter password: ");
        string password = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Username and password cannot be blank.");
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
            return false;
        }

        bool success = E_LibraryServices.Login(username, password);
        Console.WriteLine(success ? "Login successful." : "Invalid username or password.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        return success;
    }

    static void ShowMainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine("1. View Favorites");
            Console.WriteLine("2. Remove Favorite");
            Console.WriteLine("3. Update Password");
            Console.WriteLine("4. View Genres and Books");
            Console.WriteLine("5. Search Book by Title");
            Console.WriteLine("6. Log out");


            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1": ViewFavorites(); break;
                case "2": RemoveFavorite(); break;
                case "3": UpdatePassword(); break;
                case "4": SearchBookTitle(); break;

                case "5": DisplayGenresAndBooks(); break;
                case "6":
                    E_LibraryServices.Logout();
                    Console.WriteLine("You have been logged out.");
                    Console.WriteLine("Press any key to return to main menu...");
                    Console.ReadKey();
                    return;
                default:
                    Console.WriteLine("Invalid option. Press any key to try again...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void ViewFavorites()
    {
        Console.Clear();
        Console.WriteLine("Your Favorites:");
        var favorites = E_LibraryServices.MyFavorites();

        if (favorites.Count == 0)
        {
            Console.WriteLine("No favorites yet.");
        }
        else
        {
            foreach (var book in favorites)
                Console.WriteLine("- " + book);
        }

        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    static void RemoveFavorite()
    {
        Console.Clear();
        Console.Write("Enter book title to remove: ");
        string title = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Book title cannot be blank.");
        }
        else if (E_LibraryServices.RemoveFavorites(title))
        {
            Console.WriteLine("Book removed from favorites.");
        }
        else
        {
            Console.WriteLine("Book not found in your favorites.");
        }

        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    static void UpdatePassword()
    {
        Console.Clear();
        Console.Write("Enter new password: ");
        string newPassword = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(newPassword))
        {
            Console.WriteLine("Password cannot be blank.");
        }
        else
        {
            E_LibraryServices.UpdatePassword(newPassword);
            Console.WriteLine("Password Updated Successfully.");
        }

        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    static void DisplayGenresAndBooks()
    {
        Console.Clear();
        Console.WriteLine("Available Genres:");

        var genres = E_LibraryServices.GetGenres();

        for (int i = 0; i < genres.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {genres[i]}");
        }

        Console.Write("Select a genre by number: ");
        if (int.TryParse(Console.ReadLine(), out int genreIndex) && genreIndex >= 1 && genreIndex <= genres.Count)
        {
            string selectedGenre = genres[genreIndex - 1];
            var books = E_LibraryServices.GetBooksByGenre(selectedGenre);

            Console.WriteLine($"\nBooks under '{selectedGenre}':");
            foreach (var book in books)
                Console.WriteLine("- " + book);

            Console.Write("\nWould you like to add a book to favorites? (yes/no): ");
            var addChoice = Console.ReadLine()?.Trim().ToLower();

            if (addChoice == "yes")
            {
                Console.Write("Enter the Exact Book Title To Add: ");
                var bookToAdd = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(bookToAdd))
                {
                    Console.WriteLine("Book Title Cannot be Blank.");
                }
                else if (E_LibraryServices.AddFavorite(bookToAdd))
                {
                    Console.WriteLine("Book Added to Favorites.");
                }
                else
                {
                    Console.WriteLine("Book Could Not be Added (aAready in Favorites or Doesn't Exist).");
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid Genre Selection.");
        }

        Console.WriteLine("Press Any Key to Return...");
        Console.ReadKey();
    }

    static void SearchBookTitle()
{
    Console.Clear();
    Console.Write("Enter Book Title or Part of it to Search: ");
    string query = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(query))
    {
        Console.WriteLine("Search query cannot be empty.");
        Console.WriteLine("Press Any Key to Return...");
        Console.ReadKey();
        return;
    }

    var matchingBooks = E_LibraryServices.SearchBooksByTitle(query);

    if (matchingBooks.Count == 0)
    {
        Console.WriteLine("No Matching Books Found.");
    }
    else
    {
        Console.WriteLine("Matching Books:");
        foreach (var book in matchingBooks)
        {
            Console.WriteLine("- " + book);
        }

        Console.Write("\nWould you like to add one to favorites? (yes/no): ");
        var choice = Console.ReadLine()?.Trim().ToLower();
        if (choice == "yes")
        {
            Console.Write("Enter the Exact Book Title to Add: ");
            var titleToAdd = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(titleToAdd))
            {
                Console.WriteLine("Book Title Cannot be Blank.");
            }
            else if (E_LibraryServices.AddFavorite(titleToAdd))
            {
                Console.WriteLine("Book Added to Favorites.");
            }
            else
            {
                Console.WriteLine("Could Not Add Book (Already in Favorites or doesn't exist).");
            }
        }
    }

    Console.WriteLine("Press any key to return...");
    Console.ReadKey();
}

}
