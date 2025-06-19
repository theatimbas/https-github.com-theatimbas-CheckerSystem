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
            Console.WriteLine("E-Library System");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Change Password");
            Console.WriteLine("4. Delete Account");
            Console.WriteLine("5. Exit");

            string? input = Console.ReadLine()?.Trim();

            switch (input)
            {
                case "1":
                    Register();
                    break;
                case "2":
                    if (Login())
                        ShowMainMenu();
                    break;
                case "3":
                    UpdatePassword();
                    break;
                case "4":
                    DeleteAccount();
                    break;
                case "5":
                    return;
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
        string? username = Console.ReadLine()?.Trim();
        Console.Write("Enter Password: ");
        string? password = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Username and password cannot be blank.");
        }
        else if (E_LibraryServices.RegisterAccount(username, password))
        {
            Console.WriteLine("Registration successful. Auto-logging in...");
            E_LibraryServices.Login(username, password);
            Console.ReadKey();
            ShowMainMenu();
            return;
        }
        else
        {
            Console.WriteLine("Username already exists or invalid input.");
        }

        Console.WriteLine("Press any key to return to the main menu...");
        Console.ReadKey();
    }

    static bool Login()
    {
        Console.Clear();
        Console.Write("Enter Username: ");
        string? username = Console.ReadLine()?.Trim();
        Console.Write("Enter Password: ");
        string? password = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Username and password cannot be blank.");
            Console.ReadKey();
            return false;
        }

        if (E_LibraryServices.Login(username, password))
        {
            Console.WriteLine("Login successful.");
            Console.ReadKey();
            return true;
        }

        Console.WriteLine("Invalid username or password.");
        Console.ReadKey();
        return false;
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

            string? choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1": ViewFavorites(); break;
                case "2": RemoveFavorite(); break;
                case "3": UpdatePassword(); break;
                case "4":
                    Console.Clear();
                    Console.Write("Are you sure you want to delete your account? (yes/no): ");
                    string? confirm = Console.ReadLine()?.Trim().ToLower();
                    if (confirm == "yes")
                    {
                        if (E_LibraryServices.DeleteAccount())
                        {
                            Console.WriteLine("Account deleted.");
                            Console.ReadKey();
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Could not delete account.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Account deletion cancelled.");
                    }
                    Console.ReadKey();
                    break;
                case "5": DisplayGenresAndBooks(); break;
                case "6": SearchBookTitle(); break;
                case "7":
                    E_LibraryServices.Logout();
                    Console.WriteLine("Logged out.");
                    Console.ReadKey();
                    return;
                default:
                    Console.WriteLine("Invalid option.");
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
        string? title = Console.ReadLine()?.Trim();

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
            Console.WriteLine("Book not found in favorites.");
        }

        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    static void UpdatePassword()
    {
        Console.Clear();
        Console.Write("Enter new password: ");
        string? newPassword = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(newPassword))
        {
            Console.WriteLine("Password cannot be blank.");
        }
        else
        {
            E_LibraryServices.UpdatePassword(newPassword);
            Console.WriteLine("Password updated successfully.");
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
            Console.WriteLine($"{i + 1}. {genres[i]}");

        Console.Write("Select a genre by number: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= genres.Count)
        {
            string selectedGenre = genres[index - 1];
            var books = E_LibraryServices.GetBooksByGenre(selectedGenre);

            Console.WriteLine($"\nBooks in '{selectedGenre}':");
            foreach (var book in books)
                Console.WriteLine("- " + book);

            Console.Write("\nAdd a book to favorites? (yes/no): ");
            if (Console.ReadLine()?.Trim().ToLower() == "yes")
            {
                Console.Write("Enter exact book title: ");
                var bookToAdd = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(bookToAdd))
                {
                    Console.WriteLine("Title cannot be blank.");
                }
                else if (E_LibraryServices.AddFavorite(bookToAdd))
                {
                    Console.WriteLine("Book added to favorites.");
                }
                else
                {
                    Console.WriteLine("Could not add (already exists or not found).");
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }

        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    static void SearchBookTitle()
    {
        Console.Clear();
        Console.Write("Enter book title or part of it: ");
        string? query = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(query))
        {
            Console.WriteLine("Search query cannot be blank.");
            Console.ReadKey();
            return;
        }

        var matches = E_LibraryServices.SearchBooksByTitle(query);

        if (matches.Count == 0)
        {
            Console.WriteLine("No matches found.");
        }
        else
        {
            Console.WriteLine("Matching books:");
            foreach (var book in matches)
                Console.WriteLine("- " + book);

            Console.Write("\nAdd a book to favorites? (yes/no): ");
            if (Console.ReadLine()?.Trim().ToLower() == "yes")
            {
                Console.Write("Enter exact book title: ");
                var titleToAdd = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(titleToAdd))
                {
                    Console.WriteLine("Title cannot be blank.");
                }
                else if (E_LibraryServices.AddFavorite(titleToAdd))
                {
                    Console.WriteLine("Book added to favorites.");
                }
                else
                {
                    Console.WriteLine("Could not add (already in favorites or doesn't exist).");
                }
            }
        }

        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    static void DeleteAccount()
    {
        Console.Clear();
        Console.Write("Enter your username to delete (confirmation): ");
        string? username = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(username))
        {
            Console.WriteLine("Username canusing ELibraryDataLogic;\r\nusing PFinderCommon;\r\nusing System;\r\nusing System.Collections.Generic;\r\n\r\nclass Program\r\n{\r\n    static void Main()\r\n    {\r\n        while (true)\r\n        {\r\n            Console.Clear();\r\n            Console.WriteLine(\"E-Library System\");\r\n            Console.WriteLine(\"1. Create Account\");\r\n            Console.WriteLine(\"2. Login\");\r\n            Console.WriteLine(\"3. Change Password\");\r\n            Console.WriteLine(\"4. Delete Account\");\r\n            Console.WriteLine(\"5. Exit\");\r\n\r\n            string? input = Console.ReadLine()?.Trim();\r\n\r\n            switch (input)\r\n            {\r\n                case \"1\":\r\n                    Register();\r\n                    break;\r\n                case \"2\":\r\n                    if (Login())\r\n                        ShowMainMenu();\r\n                    break;\r\n                case \"3\":\r\n                    UpdatePassword();\r\n                    break;\r\n                case \"4\":\r\n                    DeleteAccount();\r\n                    break;\r\n                case \"5\":\r\n                    return;\r\n                default:\r\n                    Console.WriteLine(\"Invalid option. Press any key to try again.\");\r\n                    Console.ReadKey();\r\n                    break;\r\n            }\r\n        }\r\n    }\r\n\r\n    static void Register()\r\n    {\r\n        Console.Clear();\r\n        Console.Write(\"Enter Username: \");\r\n        string? username = Console.ReadLine()?.Trim();\r\n        Console.Write(\"Enter Password: \");\r\n        string? password = Console.ReadLine()?.Trim();\r\n\r\n        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))\r\n        {\r\n            Console.WriteLine(\"Username and password cannot be blank.\");\r\n        }\r\n        else if (E_LibraryServices.RegisterAccount(username, password))\r\n        {\r\n            Console.WriteLine(\"Registration successful. Auto-logging in...\");\r\n            E_LibraryServices.Login(username, password);\r\n            Console.ReadKey();\r\n            ShowMainMenu();\r\n            return;\r\n        }\r\n        else\r\n        {\r\n            Console.WriteLine(\"Username already exists or invalid input.\");\r\n        }\r\n\r\n        Console.WriteLine(\"Press any key to return to the main menu...\");\r\n        Console.ReadKey();\r\n    }\r\n\r\n    static bool Login()\r\n    {\r\n        Console.Clear();\r\n        Console.Write(\"Enter Username: \");\r\n        string? username = Console.ReadLine()?.Trim();\r\n        Console.Write(\"Enter Password: \");\r\n        string? password = Console.ReadLine()?.Trim();\r\n\r\n        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))\r\n        {\r\n            Console.WriteLine(\"Username and password cannot be blank.\");\r\n            Console.ReadKey();\r\n            return false;\r\n        }\r\n\r\n        if (E_LibraryServices.Login(username, password))\r\n        {\r\n            Console.WriteLine(\"Login successful.\");\r\n            Console.ReadKey();\r\n            return true;\r\n        }\r\n\r\n        Console.WriteLine(\"Invalid username or password.\");\r\n        Console.ReadKey();\r\n        return false;\r\n    }\r\n\r\n    static void ShowMainMenu()\r\n    {\r\n        while (true)\r\n        {\r\n            Console.Clear();\r\n            Console.WriteLine(\"Main Menu\");\r\n            Console.WriteLine(\"1. View Favorites\");\r\n            Console.WriteLine(\"2. Remove Favorite\");\r\n            Console.WriteLine(\"3. Change Password\");\r\n            Console.WriteLine(\"4. Delete Account\");\r\n            Console.WriteLine(\"5. View Genres and Books\");\r\n            Console.WriteLine(\"6. Search Book by Title\");\r\n            Console.WriteLine(\"7. Log out\");\r\n\r\n            string? choice = Console.ReadLine()?.Trim();\r\n\r\n            switch (choice)\r\n            {\r\n                case \"1\": ViewFavorites(); break;\r\n                case \"2\": RemoveFavorite(); break;\r\n                case \"3\": UpdatePassword(); break;\r\n                case \"4\":\r\n                    Console.Clear();\r\n                    Console.Write(\"Are you sure you want to delete your account? (yes/no): \");\r\n                    string? confirm = Console.ReadLine()?.Trim().ToLower();\r\n                    if (confirm == \"yes\")\r\n                    {\r\n                        if (E_LibraryServices.DeleteAccount())\r\n                        {\r\n                            Console.WriteLine(\"Account deleted.\");\r\n                            Console.ReadKey();\r\n                            return;\r\n                        }\r\n                        else\r\n                        {\r\n                            Console.WriteLine(\"Could not delete account.\");\r\n                        }\r\n                    }\r\n                    else\r\n                    {\r\n                        Console.WriteLine(\"Account deletion cancelled.\");\r\n                    }\r\n                    Console.ReadKey();\r\n                    break;\r\n                case \"5\": DisplayGenresAndBooks(); break;\r\n                case \"6\": SearchBookTitle(); break;\r\n                case \"7\":\r\n                    E_LibraryServices.Logout();\r\n                    Console.WriteLine(\"Logged out.\");\r\n                    Console.ReadKey();\r\n                    return;\r\n                default:\r\n                    Console.WriteLine(\"Invalid option.\");\r\n                    Console.ReadKey();\r\n                    break;\r\n            }\r\n        }\r\n    }\r\n\r\n    static void ViewFavorites()\r\n    {\r\n        Console.Clear();\r\n        Console.WriteLine(\"Your Favorites:\");\r\n\r\n        var favorites = E_LibraryServices.MyFavorites();\r\n\r\n        if (favorites.Count == 0)\r\n        {\r\n            Console.WriteLine(\"No favorites yet.\");\r\n        }\r\n        else\r\n        {\r\n            foreach (var book in favorites)\r\n                Console.WriteLine(\"- \" + book);\r\n        }\r\n\r\n        Console.WriteLine(\"Press any key to return...\");\r\n        Console.ReadKey();\r\n    }\r\n\r\n    static void RemoveFavorite()\r\n    {\r\n        Console.Clear();\r\n        Console.Write(\"Enter book title to remove: \");\r\n        string? title = Console.ReadLine()?.Trim();\r\n\r\n        if (string.IsNullOrWhiteSpace(title))\r\n        {\r\n            Console.WriteLine(\"Book title cannot be blank.\");\r\n        }\r\n        else if (E_LibraryServices.RemoveFavorites(title))\r\n        {\r\n            Console.WriteLine(\"Book removed from favorites.\");\r\n        }\r\n        else\r\n        {\r\n            Console.WriteLine(\"Book not found in favorites.\");\r\n        }\r\n\r\n        Console.WriteLine(\"Press any key to return...\");\r\n        Console.ReadKey();\r\n    }\r\n\r\n    static void UpdatePassword()\r\n    {\r\n        Console.Clear();\r\n        Console.Write(\"Enter new password: \");\r\n        string? newPassword = Console.ReadLine()?.Trim();\r\n\r\n        if (string.IsNullOrWhiteSpace(newPassword))\r\n        {\r\n            Console.WriteLine(\"Password cannot be blank.\");\r\n        }\r\n        else\r\n        {\r\n            E_LibraryServices.UpdatePassword(newPassword);\r\n            Console.WriteLine(\"Password updated successfully.\");\r\n        }\r\n\r\n        Console.WriteLine(\"Press any key to return...\");\r\n        Console.ReadKey();\r\n    }\r\n\r\n    static void DisplayGenresAndBooks()\r\n    {\r\n        Console.Clear();\r\n        Console.WriteLine(\"Available Genres:\");\r\n        var genres = E_LibraryServices.GetGenres();\r\n\r\n        for (int i = 0; i < genres.Count; i++)\r\n            Console.WriteLine($\"{i + 1}. {genres[i]}\");\r\n\r\n        Console.Write(\"Select a genre by number: \");\r\n        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= genres.Count)\r\n        {\r\n            string selectedGenre = genres[index - 1];\r\n            var books = E_LibraryServices.GetBooksByGenre(selectedGenre);\r\n\r\n            Console.WriteLine($\"\\nBooks in '{selectedGenre}':\");\r\n            foreach (var book in books)\r\n                Console.WriteLine(\"- \" + book);\r\n\r\n            Console.Write(\"\\nAdd a book to favorites? (yes/no): \");\r\n            if (Console.ReadLine()?.Trim().ToLower() == \"yes\")\r\n            {\r\n                Console.Write(\"Enter exact book title: \");\r\n                var bookToAdd = Console.ReadLine()?.Trim();\r\n\r\n                if (string.IsNullOrWhiteSpace(bookToAdd))\r\n                {\r\n                    Console.WriteLine(\"Title cannot be blank.\");\r\n                }\r\n                else if (E_LibraryServices.AddFavorite(bookToAdd))\r\n                {\r\n                    Console.WriteLine(\"Book added to favorites.\");\r\n                }\r\n                else\r\n                {\r\n                    Console.WriteLine(\"Could not add (already exists or not found).\");\r\n                }\r\n            }\r\n        }\r\n        else\r\n        {\r\n            Console.WriteLine(\"Invalid selection.\");\r\n        }\r\n\r\n        Console.WriteLine(\"Press any key to return...\");\r\n        Console.ReadKey();\r\n    }\r\n\r\n    static void SearchBookTitle()\r\n    {\r\n        Console.Clear();\r\n        Console.Write(\"Enter book title or part of it: \");\r\n        string? query = Console.ReadLine()?.Trim();\r\n\r\n        if (string.IsNullOrWhiteSpace(query))\r\n        {\r\n            Console.WriteLine(\"Search query cannot be blank.\");\r\n            Console.ReadKey();\r\n            return;\r\n        }\r\n\r\n        var matches = E_LibraryServices.SearchBooksByTitle(query);\r\n\r\n        if (matches.Count == 0)\r\n        {\r\n            Console.WriteLine(\"No matches found.\");\r\n        }\r\n        else\r\n        {\r\n            Console.WriteLine(\"Matching books:\");\r\n            foreach (var book in matches)\r\n                Console.WriteLine(\"- \" + book);\r\n\r\n            Console.Write(\"\\nAdd a book to favorites? (yes/no): \");\r\n            if (Console.ReadLine()?.Trim().ToLower() == \"yes\")\r\n            {\r\n                Console.Write(\"Enter exact book title: \");\r\n                var titleToAdd = Console.ReadLine()?.Trim();\r\n\r\n                if (string.IsNullOrWhiteSpace(titleToAdd))\r\n                {\r\n                    Console.WriteLine(\"Title cannot be blank.\");\r\n                }\r\n                else if (E_LibraryServices.AddFavorite(titleToAdd))\r\n                {\r\n                    Console.WriteLine(\"Book added to favorites.\");\r\n                }\r\n                else\r\n                {\r\n                    Console.WriteLine(\"Could not add (already in favorites or doesn't exist).\");\r\n                }\r\n            }\r\n        }\r\n\r\n        Console.WriteLine(\"Press any key to return...\");\r\n        Console.ReadKey();\r\n    }\r\n\r\n    static void DeleteAccount()\r\n    {\r\n        Console.Clear();\r\n        Console.Write(\"Enter your username to delete (confirmation): \");\r\n        string? username = Console.ReadLine()?.Trim();\r\n\r\n        if (string.IsNullOrWhiteSpace(username))\r\n        {\r\n            Console.WriteLine(\"Username cannot be blank.\");\r\n        }\r\n        else if (E_LibraryServices.DeleteAccount())\r\n        {\r\n            Console.WriteLine(\"Account deleted successfully.\");\r\n        }\r\n        else\r\n        {\r\n            Console.WriteLine(\"Failed to delete account.\");\r\n        }\r\n\r\n        Console.WriteLine(\"Press any key to return...\");\r\n        Console.ReadKey();\r\n    }\r\n}\r\nnot be blank.");
        }
        else if (E_LibraryServices.DeleteAccount())
        {
            Console.WriteLine("Account deleted successfully.");
        }
        else
        {
            Console.WriteLine("Failed to delete account.");
        }

        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }
}
