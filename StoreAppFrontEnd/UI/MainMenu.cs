using System.ComponentModel.DataAnnotations;
using Models;

namespace UI;

public class MainMenu
{

    private readonly HttpService _httpService;

    //Dependency injection
    public MainMenu(HttpService httpService)
    {
        _httpService = httpService;
    }


    public async Task Start()
    {
        await HomePage();
    }
    public async Task HomePage()
    {
        Console.WriteLine("Welcome to Store App");
        bool exit = false;
        do
        {

            Console.WriteLine("Are you a customer or a manager?");
            Console.WriteLine("[1] Customer");
            Console.WriteLine("[2] Manager");
            Console.WriteLine("[x] Exit");
            string input = InputValidation.validString();

            switch (input.ToLower())
            {
                case "1":
                    //customer portal
                    await new CustomerMenu(_httpService).Start();
                    break;

                case "2":
                    //Manager view
                    //new ManagerMenu(_httpService).Start();
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
