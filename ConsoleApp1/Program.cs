using System;
using System.Collections.Generic;
using PFinderCommon;
using ELibraryDataLogic;

namespace ELibrarySystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Pen Finder!");

            Registration();
            if (Login())
            {
                ShowMenu();
            }
        }

        static void Registration()
        {
            string UserAccount = Prompt("Do you already have an account? (yes/no)").ToLower();
            while (UserAccount != "yes" && UserAccount != "no")
            {
                UserAccount = Prompt("Please enter 'yes' or 'no':").ToLower();
            }

            if (UserAccount == "no")
            {
                bool registered = false;
                while (!registered)
                {
                    Console.WriteLine("Register a New User.");
                    string username = Prompt("Enter Username:");
                    string password = Prompt("Enter Password:");

                    registered = E_LibraryServices.RegisterAccount(username, password);
                    Console.WriteLine(registered ? "Registration Successful! You can now login." : "Username already exists or registration failed. Try again.");
                }
            }
        }

        static bool Login()
        {
            Console.WriteLine("Please login to continue.");
            string loginUsername = Prompt("Username:");
            string loginPassword = Prompt("Password:");

            if (E_LibraryServices.Login(loginUsername, loginPassword))
            {
                Console.WriteLine($"Welcome, {loginUsername}!");
                return true;
            }

            Console.WriteLine("Invalid credentials. Exiting.");
            return false;
        }

        static void ShowMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. View My Favorites");
                Console.WriteLine("2. Add Favorite");
                Console.WriteLine("3. Remove Favorite");
                Console.WriteLine("4. Rename Book");
                Console.WriteLine("0. Logout");

                string choice = Console.ReadLine()?.Trim();
                switch (choice)
                {
                    case "1": ViewFavorites(); break;
                    case "2": AddFavorite(); break;
                    case "3": RemoveFavorite(); break;
                    case "4": UpdateBook(); break;
                    case "0":
                        E_LibraryServices.Logout();
                        Console.WriteLine("Logged out. Thank you!");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        static void ViewFavorites()
        {
            var favorites = E_LibraryServices.MyFavorites();
            Console.WriteLine("Your Favorites:");
            if (favorites.Count == 0)
            {
                Console.WriteLine("No Favorites yet.");
            }
            else
            {
                foreach (var fav in favorites)
                {
                    Console.WriteLine("- " + fav);
                }
            }
        }

        static void AddFavorite()
        {
            string book = Prompt("Enter book name to add to Favorites:");
            if (E_LibraryServices.AddFavorite(book))
            {
                Console.WriteLine("Favorite added.");
            }
            else
            {
                Console.WriteLine("Book already in favorites or Invalid.");
            }
        }

        static void RemoveFavorite()
        {
            string book = Prompt("Enter book name to remove from favorites:");
            if (E_LibraryServices.RemoveFromFavorites(book))
            {
                Console.WriteLine("Removed from favorites.");
            }
            else
            {
                Console.WriteLine("Book not found in favorites.");
            }
        }

        static void UpdateBook()
        {
            string oldName = Prompt("Enter old book name:");
            string newName = Prompt("Enter new book name:");

            if (E_LibraryServices.RenameBookInUser(oldName, newName))
            {
                Console.WriteLine("Book renamed successfully.");
            }
            else
            {
                Console.WriteLine("Rename failed.");
            }
        }

        static string Prompt(string message)
        {
            Console.Write(message + " ");
            string input = Console.ReadLine()?.Trim();
            while (string.IsNullOrEmpty(input))
            {
                Console.Write("Input cannot be empty. " + message + " ");
                input = Console.ReadLine()?.Trim();
            }
            return input;
        }
    }
}
