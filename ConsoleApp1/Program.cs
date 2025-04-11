using ELibraryBusinessDataLogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ELibrarySystem
{
    internal class Program
    {
        public static void Main()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("---Pen Finder---");
            Console.WriteLine("-------------------------------------");

            while (true)
            {
                ShowMenu();
                string UserMenu = Console.ReadLine();

                switch (UserMenu)
                {
                    case "1":
                        RegisterMember();
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
            Console.WriteLine("MENU: ");
            Console.WriteLine("[1] Register\n[2] Find more Books\n[3] My Favorites\n[4] Update Favorites\n[5] Exit");
            Console.Write("Choose an option (1/2/3/4/5): ");
        }

        static void RegisterMember()
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("-----Register Member-----");
            Console.Write("Enter Username: ");
            string UserName = Console.ReadLine();
            Console.Write("Enter Your Password: ");
            string UserPassword = Console.ReadLine();
            Console.Write("Enter Your Age: ");
            string AgeInput = Console.ReadLine();

            bool isRegistered = E_LibraryServices.ValidateUser(UserName, UserPassword, AgeInput);

            if (isRegistered)
            {
                Console.WriteLine("Registration successful! You can now explore books.");
            }
            else if (ELibraryDataLogic.IsUserAlreadyRegistered(UserName))
            {
                Console.WriteLine("Username already exists. Please choose a different one.");
            }
            else
            {
                Console.WriteLine("Registration failed! Check your details.");
            }
        } // <-- Fixed: closing brace for RegisterMember

        static void BookGenre()
        {
            do
            {
                Console.WriteLine("-----Available Genres-----");
                var genres = E_LibraryServices.GetGenreBooks();
                var genreNames = genres.Keys.ToList();
                genreNames.Sort();

                foreach (var genre in genreNames)
                    Console.WriteLine("- " + genre);

                Console.Write("Enter the Genre You Want: ");
                string UserInput = Console.ReadLine();

                if (E_LibraryServices.BookGenre(UserInput))
                {
                    var books = E_LibraryServices.GetBooksByGenre(UserInput);
                    Console.WriteLine("Recommended Books:");
                    foreach (var book in books)
                        Console.WriteLine("- " + book);

                    Console.Write("Would you like to add a book to your Favorites? (Yes/No): ");
                    if (Console.ReadLine().Trim().ToLower() == "yes")
                    {
                        Console.Write("Enter the book you want to add: ");
                        string AddedBook = Console.ReadLine();
                        if (E_LibraryServices.AddToFavorites(AddedBook))
                            Console.WriteLine($"'{AddedBook}' added to Favorites!");
                        else
                            Console.WriteLine("Book name cannot be empty.");
                    }
                }
                else
                {
                    Console.WriteLine("Genre not found.");
                }

                Console.Write("Select another Genre? (Yes/No): ");
            } while (Console.ReadLine().Trim().ToLower() == "yes");

            Console.WriteLine("Thanks for using Pen Finder!");
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

            Console.Write("Remove a book? (Yes/No): ");
            if (Console.ReadLine().Trim().ToLower() == "yes")
            {
                Console.Write("Enter the book to remove: ");
                string bookToRemove = Console.ReadLine();
                if (E_LibraryServices.RemoveFromFavorites(bookToRemove))
                    Console.WriteLine($"'{bookToRemove}' removed.");
                else
                    Console.WriteLine("Book not found.");
            }
        }

        static void UpdateFavorites()
        {
            Console.WriteLine("-----Update Favorite Book-----");
            Console.Write("Enter current book name: ");
            string oldName = Console.ReadLine();
            Console.Write("Enter new book name: ");
            string newName = Console.ReadLine();

            if (E_LibraryServices.UpdateFavorite(oldName, newName))
                Console.WriteLine("Book updated successfully.");
            else
                Console.WriteLine("Update failed. Book may not exist.");
        }
    }
}
