using System.ComponentModel.Design;

string fname, lname, option;
int age;


Console.WriteLine("Welcome!");



    Console.WriteLine("Enter First Name: ");

    fname = Console.ReadLine();
    Console.WriteLine("Enter Last Name: ");
    lname = Console.ReadLine();

    Console.WriteLine("Do you want to Continue?");
    
    Console.WriteLine("YES/NO");
    option = Console.ReadLine().ToUpper();

    switch (option)
{
    case "YES":
        Console.WriteLine("Enter Age: ");
        if (int.TryParse(Console.ReadLine(), out age))
        {
            if (age > 17)
            {
                Console.WriteLine($"{fname} {lname}, you are Qualified!");
            }
            else
            {
                Console.WriteLine($"{fname} {lname}, you are Not Qualified!");
            }
        }
        else
        {
            Console.WriteLine("Invalid Age");
        }
        break;


    case "NO":
        Console.WriteLine("Thank you!");
        break;

    default:
        Console.WriteLine("Invalid");
        break;
}

