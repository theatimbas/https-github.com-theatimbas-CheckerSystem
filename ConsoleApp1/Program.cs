using ELibraryBusinessDataLogic;
using System;

namespace ELibrarySystem
{
    internal class Program
    {
        static bool isLoggedIn = false;

        static void Main()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("         ---Pen Finder---");
            Console.WriteLine("-------------------------------------");

            while (true)
            {
                ShowMenu();
                string userChoice = Console.ReadLine()?.Trim();

                switch (userChoice)
                {
                    case "0":
                        if (!isLoggedIn)
                            LoginMember();
                        else
                            Console.WriteLine("You're already logged in.");
                        break;
                    case "1":
                        if (isLoggedIn)
                            MyFavorites();
                        else
                            Console.WriteLine("Please log in first.");
                        break;
                    case "2":
                        if (isLoggedIn)
                            UpdateFavorites();
                        else
                            Console.WriteLine("Please log in first.");
                        break;
                    case "3":
                        if (isLoggedIn)
                            RemoveFavorite();
                        else
                            Console.WriteLine("Please log in first.");
                        break;
                    case "4":
                        if (isLoggedIn)
                            Logout();
                        else
                            Console.WriteLine("Please log in first.");
                        break;
                    case "5":
                        Console.WriteLine("Exiting the system. Thank you for visiting!");
                        return;
                    case "6":
                        if (isLoggedIn)
                            BrowseBooksMenu();
                        else
                            Console.WriteLine("Please log in first.");
                        break;
                    case "7":
                        RegisterNewAccount();
                        break;
                    default:
                        Console.WriteLine("Invalid Choice. Please try again.");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\nMENU:");
            if (!isLoggedIn)
            {
                Console.WriteLine("[0] Login");
                Console.WriteLine("[7] Register New Account");
                Console.WriteLine("[5] Exit");
            }
            else
            {
                Console.WriteLine("[1] View My Favorites");
                Console.WriteLine("[2] Update Favorite Book");
                Console.WriteLine("[3] Remove a Favorite Book");
                Console.WriteLine("[4] Log Out");
                Console.WriteLine("[6] Browse Books");
                Console.WriteLine("[5] Exit");
            }
            Console.Write("Choose an option: ");
        }

        static void LoginMember()
        {
            Console.WriteLine("-----Login Member-----");
            Console.Write("Enter Username: ");
            string userName = Console.ReadLine()?.Trim();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine()?.Trim();
            Console.Write("Enter Age: ");
            string ageInput = Console.ReadLine()?.Trim();

            if (int.TryParse(ageInput, out int age) && E_LibraryServices.ValidateAccount(userName, password, age))
            {
                Console.WriteLine($"Login successful! Welcome, {userName}.");
                isLoggedIn = true;
            }
            else
            {
                Console.WriteLine("Login failed. Please check your credentials.");
            }
        }

        static void RegisterNewAccount()
        {
            Console.WriteLine("----- Register New User -----");
            Console.Write("Enter Username: ");
            string userName = Console.ReadLine()?.Trim();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine()?.Trim();
            Console.Write("Enter Age: ");
            string ageInput = Console.ReadLine()?.Trim();

            if (!int.TryParse(ageInput, out int age))
            {
                Console.WriteLine("Invalid age.");
                return;
            }

            if (E_LibraryServices.RegisterAccount(userName, password, age))
                Console.WriteLine("Registration successful. You can now log in.");
            else
                Console.WriteLine("Registration failed. Username might already be taken.");
        }

        static void MyFavorites()
        {
            Console.WriteLine("-----My Favorite Books-----");
            var favorites = E_LibraryServices.MyFavorites();

            if (favorites.Count == 0)
            {
                Console.WriteLine("No favorites added yet.");
                return;
            }

            foreach (var book in favorites)
                Console.WriteLine("- " + book);
        }

        static void UpdateFavorites()
        {
            Console.WriteLine("-----Update Favorite Book-----");
            Console.Write("Enter current book name: ");
            string oldName = Console.ReadLine()?.Trim();
            Console.Write("Enter new book name: ");
            string newName = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(oldName) || string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("Book names cannot be empty.");
                return;
            }

            if (E_LibraryServices.UpdateFavorite(oldName, newName))
                Console.WriteLine("Book updated successfully.");
            else
                Console.WriteLine("Update failed. Book may not exist.");
        }

        static void RemoveFavorite()
        {
            Console.WriteLine("-----Remove a Favorite Book-----");
            Console.Write("Enter the book name to remove: ");
            string bookToRemove = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(bookToRemove))
            {
                Console.WriteLine("Book name cannot be empty.");
                return;
            }

            if (E_LibraryServices.RemoveFromFavorites(bookToRemove))
                Console.WriteLine($"'{bookToRemove}' removed from favorites.");
            else
                Console.WriteLine("Book not found in favorites.");
        }

        static void BrowseBooksMenu()
        {
            Console.WriteLine("----- Browse Books by Genre -----");
            var genres = E_LibraryServices.GetGenres();

            for (int i = 0; i < genres.Count; i++)
            {
                Console.WriteLine($"[{i}] {genres[i]}");
            }
            Console.Write("Select a genre by number: ");

            if (int.TryParse(Console.ReadLine(), out int genreIndex)
                && genreIndex >= 0 && genreIndex < genres.Count)
            {
                string selectedGenre = genres[genreIndex];
                var books = E_LibraryServices.GetBooksByGenre(selectedGenre);

                if (books.Count == 0)
                {
                    Console.WriteLine("No books available in this genre.");
                    return;
                }

                Console.WriteLine($"\nBooks in {selectedGenre}:");
                for (int j = 0; j < books.Count; j++)
                {
                    Console.WriteLine($"[{j}] {books[j]}");
                }

                Console.WriteLine("\nPress any key to return to the main menu...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Invalid selection. Returning to menu.");
            }
        }

        static void Logout()
        {
            Console.WriteLine("Logging out...");
            E_LibraryServices.Logout();
            isLoggedIn = false;
        }
    }
}