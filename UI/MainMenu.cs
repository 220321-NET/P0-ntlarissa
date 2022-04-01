using System.ComponentModel.DataAnnotations;
using Models;

namespace UI;

public class MainMenu
{
    public void Start()
    {
        Console.WriteLine("Welcome to Store App");
        bool exit = false;
        do
        {

            Console.WriteLine("Are you a customer or a manager?");
            Console.WriteLine("[1] Customer");
            Console.WriteLine("[2] Manager");
            Console.WriteLine("[x] Exit");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    //customer portal
                    Console.WriteLine("Customer!!!");
                    new CustomerMenu().Start();
                    break;

                case "2":
                    //Manager view
                    Console.WriteLine("Manager!!!");
                    new ManagerMenu().Start();
                    break;
                case "x":
                    Console.WriteLine("Goodbye!");
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid input, try again");
                    break;
            }

        } while (!exit);

    }

}
