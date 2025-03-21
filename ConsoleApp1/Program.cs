using System;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Welcome to Checker E-Library System");
        Console.WriteLine("-------------------------------------");

        while (true)
        {
            string UserOption = ShowMenu();

            switch (UserOption)
            {
                case "1":
                    RegisterMember();
                    break;
                case "2":
                    BookGenre();
                    break;
                case "3":
                    Console.WriteLine("Exiting the Library. Thank you for visiting!");
                    return;
                default:
                    Console.WriteLine("Please Choose between number 1-3");
                    break;


            }
        }
    }

    static string ShowMenu()
    {
        Console.WriteLine("------------------------");
        Console.WriteLine("MENU: ");
        Console.WriteLine("[1] Register");
        Console.WriteLine("[2] Find more Books");
        Console.WriteLine("[3] Exit");
        Console.Write("Choose an option (1/2/3): ");
        return Console.ReadLine();
    }

    static void RegisterMember()
    {
        Console.WriteLine("--------------------------");
        Console.WriteLine("Register Member");
        Console.Write("Enter Full Name or Username: ");
        Console.ReadLine();
        Console.WriteLine("Enter Your Age: ");
        int UserAge;
        if (int.TryParse(Console.ReadLine(), out UserAge))
        {
            if (UserAge > 17)
            {
                Console.WriteLine("-----You are now registered successfully!-----");
            }
            else if (UserAge <= 17)
            {
                Console.WriteLine("You are still Underage");
                Console.WriteLine("Thank you for trying!");
            }
        }
        else
        {
            Console.WriteLine("Invalid Age! Please enter a valid number.");
        }
    }
    static void BookGenre()
    {
        Console.WriteLine("-----------------");
        Console.WriteLine("List of Recommended Books and Genres");
        Console.WriteLine("Fantasy");
        Console.WriteLine("Romance");
        Console.WriteLine("Mystery");
        Console.WriteLine("Science Fiction");

        Console.WriteLine("-----------------"); 
    }
}

                