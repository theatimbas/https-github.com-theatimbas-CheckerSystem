using System;

namespace ELibrarySystem
{
    internal class Program
    {
        private static List<string> AddedFavorites = new List<string>();

        public static void Main()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Pen Finder");
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
            Console.WriteLine("[1] Register\n[2] Find more Books\n[3] My Favorites\n[4] Exit");
            Console.Write("Choose an option (1/2/3/4): ");
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
        }

        static void BookGenre()
        {
            do
            {
                string[] RecommendGenre = { "Fantasy", "Romance", "Drama", "Science-Fiction", "Action", "Historical" };
                Console.WriteLine("-----Available Genres-----");
                Array.Sort(RecommendGenre);
                foreach (string AvailableGenre in RecommendGenre)
                {
                    Console.WriteLine(AvailableGenre);
                }

                Console.Write("Enter the Genre You Want: ");
                string UserInput = Console.ReadLine();

                switch (UserInput)
                {
                    case "Fantasy":
                        Console.WriteLine("Titan Academy\nCharm Academy\nTantei High\nOlympus Academy");
                        break;
                    case "Romance":
                        Console.WriteLine("Hell University\nUniversity Series\nBuenaventura Series\nThe Girl He Never Noticed");
                        break;
                    case "Drama":
                        Console.WriteLine("The Tempest\nA Wife's Cry\nSalamasim\nTaste of Sky");
                        break;
                    case "Science-Fiction":
                        Console.WriteLine("Ender's Game\nProject: Yngrid\nThe Peculiars Tale\nMnemosyne's Tale");
                        break;
                    case "Action":
                        Console.WriteLine("The Maze Runner\nThe Hunger Games\nDivergent\nThe Fifth Wave");
                        break;
                    case "Historical":
                        Console.WriteLine("I Love You Since 1892\nReincarnated as Binibini\nOur Asymptotic Love");
                        break;
                    default:
                        Console.WriteLine("Genre Not Available or Genre is not on the List!");
                        continue;
                }

                Console.Write("Would you like to add a book to your Favorites? (Yes/No): ");
                if (Console.ReadLine().Trim().ToLower() == "yes")
                {
                    Console.Write("Enter the book you want to add: ");
                    string AddedBook = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(AddedBook))
                    {
                        AddedFavorites.Add(AddedBook);
                        Console.WriteLine($"'{AddedBook}' has been added to your Favorites!");
                    }
                    else
                    {
                        Console.WriteLine("Book name cannot be empty!");
                    }
                }

                Console.Write("Do you want to select another Genre? (Yes/No): ");
            } while (Console.ReadLine().Trim().ToLower() == "yes");

            Console.WriteLine("Thank you for visiting Pen Finder!");
        }

        static void MyFavorites()
        {
            Console.WriteLine("-----Added Books-----");

            if (AddedFavorites.Count == 0)
            {
                Console.WriteLine("No Added Books yet.");
                return;
            }

            foreach (var book in AddedFavorites)
            {
                Console.WriteLine("- " + book);
            }

            Console.Write("Would you like to remove a book from your Favorites? (Yes/No): ");
            if (Console.ReadLine().Trim().ToLower() == "yes")
            {
                RemoveFromFavorites();
            }
        }

        static void RemoveFromFavorites()
        {
            Console.Write("Enter the name of the book to remove: ");
            string BookToRemove = Console.ReadLine();

            if (AddedFavorites.Remove(BookToRemove))
            {
                Console.WriteLine($"'{BookToRemove}' has been removed from your Favorites!");
            }
            else
            {
                Console.WriteLine("Book not found in your Favorites!");
            }
        }
    }
}
