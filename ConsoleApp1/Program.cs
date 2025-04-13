using ELibraryBusinessDataLogic;
using ELibraryDataLogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ELibrarySystem
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("         ---Pen Finder---");
            Console.WriteLine("-------------------------------------");

            while (true)
            {
                ShowMenu();
                string UserMenu = Console.ReadLine()?.Trim();

                switch (UserMenu)
                {
                    case "0":
                        LoginMember();
                        break;
                    case "1":
                        RegisterOrLogIn();
                        break;
                    case "2":
                        BookGenre();
                        break;
                    case "3":
                        MyFavorites();
                        break;
                    case "4":
                        UpdateFavorites();
                        break;
                    case "5":
                        SearchBooks();
                        break;
                    case "6":
                        Console.WriteLine("Exiting the system. Thank you for visiting!");
                        return;
                    default:
                        Console.WriteLine("Invalid Choice. Please try again.");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\nMENU: ");
            Console.WriteLine("[0] Login");
            Console.WriteLine("[1] Register");
            Console.WriteLine("[2] Find more Books");
            Console.WriteLine("[3] My Favorites");
            Console.WriteLine("[4] Update Favorites");
            Console.WriteLine("[5] Search Books");
            Console.WriteLine("[7] Exit");
            Console.Write("Choose an option (0-6): ");
        }

        static void RegisterOrLogIn()
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("-----Register Member-----");
            Console.Write("Enter Username: ");
            string UserName = Console.ReadLine();
            Console.Write("Enter Your Password: ");
            string UserPassword = Console.ReadLine();
            Console.Write("Enter Your Age: ");
            string AgeInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(UserPassword) || string.IsNullOrWhiteSpace(AgeInput))
            {
                Console.WriteLine("Invalid input. Please provide all details.");
                return;
            }

            if (DataFinder.IsUserAlreadyRegistered(UserName))
            {
                Console.WriteLine("Account exists. Proceeding to login...");

                if (int.TryParse(AgeInput, out int Age) && DataFinder.ValidateAccount(UserName, UserPassword, Age))
                {
                    Console.WriteLine($"Login successful! Welcome back, {UserName}.");
                    E_LibraryServices.SetCurrentUser(UserName);
                }
                else
                {
                    Console.WriteLine("Login failed. Incorrect credentials.");
                }
            }
            else
            {
                bool registered = E_LibraryServices.ValidateUser(UserName, UserPassword, AgeInput);
                if (registered)
                {
                    Console.WriteLine("Registration successful! You can now explore books.");
                    E_LibraryServices.SetCurrentUser(UserName);
                }
                else
                {
                    Console.WriteLine("Registration failed. Please check your input.");
                }
            }
        }

        static void LoginMember()
        {
            Console.WriteLine("-----Login Member-----");
            Console.Write("Enter Username: ");
            string UserName = Console.ReadLine();
            Console.Write("Enter Password: ");
            string UserPassword = Console.ReadLine();
            Console.Write("Enter Age: ");
            string AgeInput = Console.ReadLine();

            if (int.TryParse(AgeInput, out int Age) && DataFinder.ValidateAccount(UserName, UserPassword, Age))
            {
                Console.WriteLine($"Login successful! Welcome, {UserName}.");
                E_LibraryServices.SetCurrentUser(UserName);
            }
            else
            {
                Console.WriteLine("Login failed. Please check your credentials.");
            }
        }

        static void BookGenre()
        {
            do
            {
                Console.WriteLine("-----Available Genres-----");
                var genres = E_LibraryServices.GetGenreBooks();
                var GenreNames = genres.Keys.ToList();
                GenreNames.Sort();

                foreach (var genre in GenreNames)
                    Console.WriteLine("- " + genre);

                Console.Write("Enter the Genre You Want: ");
                string UserInput = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(UserInput))
                {
                    Console.WriteLine("Genre input cannot be empty.");
                    continue;
                }

                if (E_LibraryServices.BookGenre(UserInput))
                {
                    var books = E_LibraryServices.GetBooksByGenre(UserInput);
                    Console.WriteLine("Recommended Books:");
                    for (int i = 0; i < books.Count; i++)
                        Console.WriteLine($"[{i + 1}] {books[i]}");

                    Console.Write("Would you like to add a book to your Favorites? (Yes/No): ");
                    if (Console.ReadLine()?.Trim().ToLower() == "yes")
                    {
                        Console.Write("Enter the number of the book: ");
                        string input = Console.ReadLine();

                        if (int.TryParse(input, out int BookIndex) && BookIndex >= 1 && BookIndex <= books.Count)
                        {
                            string SelectedBook = books[BookIndex - 1];
                            if (E_LibraryServices.AddToFavorites(SelectedBook))
                                Console.WriteLine($"'{SelectedBook}' added to Favorites!");
                            else
                                Console.WriteLine("Failed to add to favorites.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid selection.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Genre not found.");
                }

                Console.Write("Select another Genre? (Yes/No): ");
            } while (Console.ReadLine()?.Trim().ToLower() == "yes");

            Console.WriteLine("Thanks for using Pen Finder!");
        }

        static void MyFavorites()
        {
            Console.WriteLine("-----My Favorite Books-----");
            var Favorites = E_LibraryServices.MyFavorites();

            if (Favorites.Count == 0)
            {
                Console.WriteLine("No favorites added yet.");
                return;
            }

            foreach (var book in Favorites)
                Console.WriteLine("- " + book);

            Console.Write("Remove a book? (Yes/No): ");
            if (Console.ReadLine()?.Trim().ToLower() == "yes")
            {
                Console.Write("Enter the book to remove: ");
                string BookToRemove = Console.ReadLine();
                if (E_LibraryServices.RemoveFromFavorites(BookToRemove))
                    Console.WriteLine($"'{BookToRemove}' removed.");
                else
                    Console.WriteLine("Book not found.");
            }
        }

        static void UpdateFavorites()
        {
            Console.WriteLine("-----Update Favorite Book-----");
            Console.Write("Enter current book name: ");
            string OldName = Console.ReadLine();
            Console.Write("Enter new book name: ");
            string NewName = Console.ReadLine();

            if (E_LibraryServices.UpdateFavorite(OldName, NewName))
                Console.WriteLine("Book updated successfully.");
            else
                Console.WriteLine("Update failed. Book may not exist.");
        }

        static void SearchBooks()
        {
            Console.WriteLine("-----Search for a Book-----");
            Console.Write("Enter keyword: ");
            string Keyword = Console.ReadLine();

            var results = E_LibraryServices.SearchBooks(Keyword);

            if (results.Count == 0)
            {
                Console.WriteLine("No books found.");
            }
            else
            {
                Console.WriteLine("Search Results:");
                foreach (var book in results)
                    Console.WriteLine("- " + book);
            }
        }
    }
}
