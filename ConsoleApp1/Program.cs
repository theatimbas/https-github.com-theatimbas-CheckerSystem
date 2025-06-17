using ELibraryDataLogic;
using Microsoft.VisualBasic.FileIO;
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
            Console.WriteLine("1. Create Account\n2. Login\n3. Change Password\n5. Exit");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1": Register(); break;
                case "2": if (Login()) ShowMainMenu(); break;
                case "3": UpdatePassword(); break;
                case "4": DeleteAccount(); break;
                case "5": return;
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
        Console.Write("Enter Username: ");
        string? UserName = Console.ReadLine()?.Trim();
        Console.Write("Enter Password: ");
        string? Password = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
        {
            Console.WriteLine("Invalid.");
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
            return;
        }

        bool success = E_LibraryServices.RegisterAccount(UserName, Password);
        Console.WriteLine(success ? "Registration successful." : "Username Already Exists or Invalid Input.");

        if (success)
        {
            E_LibraryServices.Login(UserName, Password);
            Console.WriteLine("Auto-logged in after Registration.");
            Console.WriteLine("Press Any Key to Continue...");
            Console.ReadKey();
            ShowMainMenu();
        }
        else
        {
            Console.WriteLine("Press Any Key to Return to Main Menu...");
            Console.ReadKey();
        }
    }

    static bool Login()
    {
        Console.Clear();
        Console.Write("Enter Username: ");
        string? UserName = Console.ReadLine()?.Trim();
        Console.Write("Enter Password: ");
        string? Password = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
        {
            Console.WriteLine("Username and password cannot be blank.");
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
            return false;
        }

        bool success = E_LibraryServices.Login(UserName, Password);
        Console.WriteLine(success ? "Login Successful." : "Invalid username or Password.");
        Console.WriteLine("Press Any Key to Continue...");
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
            Console.WriteLine("3. Change Password");
            Console.WriteLine("4. Delete Account");
            Console.WriteLine("5. View Genres and Books");
            Console.WriteLine("6. Search Book by Title");
            Console.WriteLine("7. Log out");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1": ViewFavorites(); break;
                case "2": RemoveFavorite(); break;
                case "3": UpdatePassword(); break;
                case "4":
                    Console.Clear();
                    Console.Write("Are you sure you want to delete your account? This cannot be undone. (yes/no): ");
                    string? confirm = Console.ReadLine()?.Trim().ToLower();

                    if (confirm == "yes")
                    {
                        if (E_LibraryServices.DeleteAccount())
                        {
                            Console.WriteLine("Your Account has been Deleted.");
                            Console.WriteLine("Press Any Key to Return to Main Menu...");
                            Console.ReadKey();
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Error: Unable to Delete Account. Are you logged in?");
                            Console.WriteLine("Press Any Key to Return...");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Account Deletion Canceled.");
                        Console.WriteLine("Press Any Key to Return...");
                        Console.ReadKey();
                    }
                    break;
                case "5": DisplayGenresAndBooks(); break;
                case "6": SearchBookTitle(); break;
                case "7":
                    E_LibraryServices.Logout();
                    Console.WriteLine("You have been logged out.");
                    Console.WriteLine("Press Any Key to Return to Main Menu...");
                    Console.ReadKey();
                    return;
                default:
                    Console.WriteLine("Invalid Option. Press Any Key to Try Again...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void ViewFavorites()
    {
        Console.Clear();
        Console.WriteLine("Your Favorites:");
        var FavoriteBook = E_LibraryServices.MyFavorites();

        if (FavoriteBook.Count == 0)
        {
            Console.WriteLine("No Favorites Yet.");
        }
        else
        {
            foreach (var book in FavoriteBook)
                Console.WriteLine("- " + book);
        }

        Console.WriteLine("Press Any Key to Return...");
        Console.ReadKey();
    }

    static void RemoveFavorite()
    {
        Console.Clear();
        Console.Write("Enter Book Title to Remove: ");
        string? BookTitle = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(BookTitle))
        {
            Console.WriteLine("Book Title cannot be blank.");
        }
        else if (E_LibraryServices.RemoveFavorites(BookTitle))
        {
            Console.WriteLine("Book Removed from Favorites.");
        }
        else
        {
            Console.WriteLine("Book not found in your Favorites.");
        }

        Console.WriteLine("Press Any Key to Return...");
        Console.ReadKey();
    }

    static void UpdatePassword()
    {
        Console.Clear();
        Console.Write("Enter New Password: ");
        string? NewPassword = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(NewPassword))
        {
            Console.WriteLine("Password cannot be blank.");
        }
        else
        {
            E_LibraryServices.UpdatePassword(NewPassword);
            Console.WriteLine("Password Updated Successfully.");
        }

        Console.WriteLine("Press Any Key to Return...");
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

        Console.Write("Select a Genre by Number: ");
        if (int.TryParse(Console.ReadLine(), out int GenreIndex) && GenreIndex >= 1 && GenreIndex <= genres.Count)
        {
            string SelectedGenre = genres[GenreIndex - 1];
            var books = E_LibraryServices.GetBooksByGenre(SelectedGenre);

            Console.WriteLine($"\nBooks under '{SelectedGenre}':");
            foreach (var book in books)
                Console.WriteLine("- " + book);

            Console.Write("\nWould you like to add a book to favorites? (yes/no): ");
            var AddedOption = Console.ReadLine()?.Trim().ToLower();

            if (AddedOption == "yes")
            {
                Console.Write("Enter the Exact Book Title To Add: ");
                var BookToAdd = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(BookToAdd))
                {
                    Console.WriteLine("Book Title Cannot be Blank.");
                }
                else if (E_LibraryServices.AddFavorite(BookToAdd))
                {
                    Console.WriteLine("Book Added to Favorites.");
                }
                else
                {
                    Console.WriteLine("Book Could Not be Added (Already in Favorites or Doesn't Exist).");
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
        string? SearchBook = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(SearchBook))
        {
            Console.WriteLine("Search query cannot be empty.");
            Console.WriteLine("Press Any Key to Return...");
            Console.ReadKey();
            return;
        }

        var MatchBooks = E_LibraryServices.SearchBooksByTitle(SearchBook);

        if (MatchBooks.Count == 0)
        {
            Console.WriteLine("No Matching Books Found.");
        }
        else
        {
            Console.WriteLine("Matching Books:");
            foreach (var book in MatchBooks)
            {
                Console.WriteLine("- " + book);
            }

            Console.Write("\nWould you like to add one to favorites? (yes/no): ");
            var choice = Console.ReadLine()?.Trim().ToLower();
            if (choice == "yes")
            {
                Console.Write("Enter the Exact Book Title to Add: ");
                var TitleToAdd = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(TitleToAdd))
                {
                    Console.WriteLine("Book Title Cannot be Blank.");
                }
                else if (E_LibraryServices.AddFavorite(TitleToAdd))
                {
                    Console.WriteLine("Book Added to Favorites.");
                }
                else
                {
                    Console.WriteLine("Could Not Add Book (Already in Favorites or doesn't exist).");
                }
            }
        }

        Console.WriteLine("Press Any Key to Return...");
        Console.ReadKey();
    }

    static void DeleteAccount()
    {
        Console.Clear();
        Console.Write("Enter the Username to Delete: ");
        string? UserName = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(UserName))
        {
            Console.WriteLine("Username cannot be Blank.");
        }
        else
        {
            bool DeletedAccount = E_LibraryServices.DeleteAccount();
            Console.WriteLine(DeletedAccount ? "Account Deleted Successfully." : "Account Could not be Deleted.");
        }

        Console.WriteLine("Press Any Key to Return to Main Menu...");
        Console.ReadKey();
    }
}
