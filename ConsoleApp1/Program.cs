using System;
using ELibraryBusinessDataLogic;

namespace CheckerSystem
{

    internal class Program
    {
        public static void Main()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Welcome to E-Library System");
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
            Console.WriteLine("[1] Register\n[2] Find more Books\n[3] Exit");
            Console.Write("Choose an option (1/2/3): ");
            
        }

        static void RegisterMember()
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("-----Register Member-----");
            Console.Write("Enter Full Name or Username: ");
            string UserName = Console.ReadLine();
            Console.WriteLine("Enter Your Age: ");
            string AgeInput = Console.ReadLine();

            bool result = E_LibraryProcedure.UserAction(UserName, AgeInput);
            if (result == false)
            {
                Console.WriteLine("You are stil Underage. Thank you for Visiting!");
            }
        }
                static void BookGenre()
                {
                    do
                    {
                        string[] RecommendGenre = { "Fantasy", "Romance", "Drama", "Science-Fiction", "Action", "Historical" };
                        Console.WriteLine("Available Genres:");
                        Console.WriteLine("--------------------");
                        Array.Sort(RecommendGenre);
                        foreach (string AvailableGenre in RecommendGenre)
                        {
                            Console.WriteLine(AvailableGenre);
                        }
                        Console.Write("Enter the Genre You Want: ");
                        string UserInput = Console.ReadLine();

                        if (E_LibraryProcedure.BookGenre(UserInput))
                        {
                            switch (UserInput)
                            {
                                case "Fantasy":
                                    Console.WriteLine("Titan Academy\nCharm Academy\nTantei High\nOlympus Academy");
                                    break;
                                case "Romance":
                                    Console.WriteLine("Hell University\nUniversity Series\nBuenaventura Series\nThe Girl He Never Noticed");
                                    break;
                                case "Drama":
                                    Console.WriteLine("The Tempest\nA Wife's Cry\nSalamasim\nA Taste of Sky");
                                    break;
                                case "Science-Fiction":
                                    Console.WriteLine("Ender's Game\nProject: Yngrid\nThe Peculiars Tale\nMnemosyne's Tale");
                                    break;
                                case "Action":
                                    Console.WriteLine("The Tempest\nA Wife's Cry\nSalamasim\nA Taste of Sky");
                                    break;
                                case "Historical":
                                    Console.WriteLine("I LOve You Since 1892\nReincarnated as Binibini\nOur Asymptotic Love");
                                    break;
                                default:
                                    Console.WriteLine("Genre Not Available or Genre is not on the List!");
                                    break;
                            }

                        }
                        Console.Write("Do you want to select another genre? (Yes/No): ");
                    } while (Console.ReadLine().Trim().ToLower() == "yes");
                    Console.WriteLine("Thank you for visiting our E-Library!");
                }
            }
        }
    



