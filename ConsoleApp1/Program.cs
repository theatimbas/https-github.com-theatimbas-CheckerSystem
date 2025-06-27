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
            Console.WriteLine("Welcome to Pen Finder!");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("1. Create an Account");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            Console.Write("\nChoose an option: ");

            string? choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    Register();
                    break;
                case "2":
                    if (Login()) ShowMainMenu();
                    break;
                case "3":
                    Console.WriteLine("Thank you for using Pen Finder. Goodbye!");
                    return;
                default:
                    ShowMessage("Invalid option. Try again.");
                    break;
            }
        }
    }

    static void Register()
    {
        Console.Clear();
        Console.WriteLine("Create an Account");
        Console.WriteLine("---------------------");

        string? UserName = ReadInput("Enter Username: ");
        string? Password = ReadInput("Enter Password: ");

        if (E_LibraryServices.RegisterAccount(UserName, Password))
        {
            ShowMessage("Registration successful! Logging you in...");
            E_LibraryServices.Login(UserName, Password);
            ShowMainMenu();
        }
        else
        {
            ShowMessage("That username is already taken or invalid.");
        }
    }

    static bool Login()
    {
        Console.Clear();
        Console.WriteLine("Login");
        Console.WriteLine("--------");

        string? username = ReadInput("Enter Username: ");
        string? password = ReadInput("Enter Password: ");

        if (E_LibraryServices.Login(username, password))
        {
            ShowMessage("Login successful!");
            return true;
        }

        ShowMessage("Incorrect username or password.");
        return false;
    }

    static void ShowMainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine("------------");
            Console.WriteLine("1. View Favorite Books");
            Console.WriteLine("2. Remove a Favorite");
            Console.WriteLine("3. Change Password");
            Console.WriteLine("4. Browse by Genre");
            Console.WriteLine("5. Search Book by Title");
            Console.WriteLine("6. Delete Account");
            Console.WriteLine("7. Log Out");
            Console.Write("\nChoose an option: ");

            string? input = Console.ReadLine()?.Trim();

            switch (input)
            {
                case "1": ViewFavorites(); 
                    break;
                case "2": RemoveFavorite(); 
                    break;
                case "3": UpdatePassword(); 
                    break;
                case "4": DisplayGenresAndBooks(); 
                    break;
                case "5": SearchBookTitle(); 
                    break;
                case "6": DeleteAccount(); 
                    break;
                case "7":
                    E_LibraryServices.Logout();
                    ShowMessage("You have been logged out.");
                    return;
                default:
                    ShowMessage("Invalid option. Try again.");
                    break;
            }
        }
    }

    static void ViewFavorites()
    {
        Console.Clear();
        Console.WriteLine("Your Favorite Books:");
        Console.WriteLine("------------------------");

        var favorites = E_LibraryServices.MyFavorites();
        if (favorites.Count == 0)
        {
            Console.WriteLine("You don’t have any favorites yet.");
        }
        else
        {
            foreach (var book in favorites)
                Console.WriteLine("• " + book);
        }

        Pause();
    }

    static void RemoveFavorite()
    {
        Console.Clear();
        Console.WriteLine("Remove a Favorite Book");
        Console.WriteLine("--------------------------");

        string? title = ReadInput("Enter the title of the book to remove: ");
        if (E_LibraryServices.RemoveFavorite(title))
        {
            ShowMessage("Book removed from favorites.");
        }
        else
        {
            ShowMessage("Book not found in your favorites.");
        }
    }

    static void UpdatePassword()
    {
        Console.Clear();
        Console.WriteLine("Change Your Password");
        Console.WriteLine("------------------------");

        string? newPass = ReadInput("Enter your New Password: ");
        E_LibraryServices.UpdatePassword(newPass);
        ShowMessage("Password Updated Successfully!");
    }

    static void DisplayGenresAndBooks()
    {
        Console.Clear();
        Console.WriteLine("Browse Genres");
        Console.WriteLine("----------------");

        var genres = E_LibraryServices.GetGenres();
        for (int i = 0; i < genres.Count; i++)
            Console.WriteLine($"{i + 1}. {genres[i]}");

        int index;
        Console.Write("\nChoose a genre number: ");
        bool isValid = int.TryParse(Console.ReadLine(), out index);

        if (!isValid || index < 1 || index > genres.Count)
        {
            ShowMessage("Invalid genre selection.");
            return;
        }

        string selectedGenre = genres[index - 1];
        var books = E_LibraryServices.GetBooksByGenre(selectedGenre);

        Console.WriteLine($"\nBooks in {selectedGenre}:");
        foreach (var book in books)
            Console.WriteLine("• " + book);

        if (Confirm("Do you want to add a book to your favorites? (yes/no): "))
        {
            string? bookTitle = ReadInput("Enter the exact book title: ");
            if (E_LibraryServices.AddFavorite(bookTitle))
                ShowMessage("Book added to favorites!");
            else
                ShowMessage("Couldn't add book (check if it's already in favorites or misspelled).");
        }
    }

    static void SearchBookTitle()
    {
        Console.Clear();
        Console.WriteLine("Search Book by Title");
        Console.WriteLine("------------------------");

        string? query = ReadInput("Enter full or partial book title: ");
        var results = E_LibraryServices.SearchBooksByTitle(query);

        if (results.Count == 0)
        {
            ShowMessage("No matching books found.");
            return;
        }

        Console.WriteLine("\nMatching Books:");
        foreach (var book in results)
            Console.WriteLine("• " + book);

        if (Confirm("Would you like to add one to your favorites? (yes/no): "))
        {
            string? bookTitle = ReadInput("Enter the exact title: ");
            if (E_LibraryServices.AddFavorite(bookTitle))
                ShowMessage("Book added to favorites!");
            else
                ShowMessage("Couldn’t add book.");
        }

        Pause();
    }

    static void DeleteAccount()
    {
        Console.Clear();
        Console.WriteLine("Delete Your Account");
        Console.WriteLine("------------------------");

        string? confirm = ReadInput("Type your username to confirm: ");
        if (E_LibraryServices.DeleteAccount())
        {
            ShowMessage("Account deleted successfully.");
        }
        else
        {
            ShowMessage("Failed to delete account. Make sure you're logged in.");
        }
    }
    static string ReadInput(string prompt)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine()?.Trim();
        while (string.IsNullOrWhiteSpace(input))
        {
            Console.Write("Input cannot be blank. Try again: ");
            input = Console.ReadLine()?.Trim();
        }
        return input!;
    }

    static void Pause()
    {
        Console.WriteLine("\nPress any key to return...");
        Console.ReadKey();
    }

    static void ShowMessage(string message)
    {
        Console.WriteLine($"\n{message}");
        Pause();
    }

    static bool Confirm(string message)
    {
        Console.Write(message);
        return Console.ReadLine()?.Trim().ToLower() == "yes";
    }
}
