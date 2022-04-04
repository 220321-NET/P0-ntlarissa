using System.ComponentModel.DataAnnotations;
using Models;
using BL;

namespace UI;

public class CustomerMenu
{
    private readonly IStoreBL _bl;

    //Dependency injection
    public CustomerMenu(IStoreBL bl)
    {
        _bl = bl;
    }

    public void Start()
    {
        HomePageCustomer();


    }


    //home page customer
    private void HomePageCustomer()
    {
        bool exit = false;
        do
        {

            Console.WriteLine("What would you like to do today?");
            Console.WriteLine("[1] Log IN");
            Console.WriteLine("[2] Sign UP");
            Console.WriteLine("[x] Exit");
            string input = InputValidation.validString();

            switch (input.ToLower())
            {
                case "1":
                    //Login to an app
                    LogToUser();
                    break;

                case "2":
                    //create a new customer
                    CreateNewUser();
                    break;
                case "x":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid input, try again");
                    break;
            }

        } while (!exit);
    }

    // log to an account user or customer
    private void LogToUser()
    {

        Console.WriteLine("Logging  Customer");

        Console.WriteLine("Enter Your UserName: ");
        string username = InputValidation.validString();
        Console.WriteLine("Enter Your Password: ");
        string? password = InputValidation.validString();

        //connect to the database if the user exit return all the information to build user objet

        User userToGet = new User();
        userToGet.UserName = username;
        userToGet.Password = password;
        userToGet.IsAdmin = false;
        User? gotUser = _bl.getUser(userToGet);
        if (gotUser != null)
        {
            OutputMessage.SucessConnexion(gotUser.FirstName);
            PortalCustomer(gotUser);

        }
        else
        {
            OutputMessage.ErrorConnexion();
        }
    }

    private void PortalCustomer(User customer)
    {
        bool exit = false;
        do
        {

            Console.WriteLine("What would you like to do today?");
            Console.WriteLine("[1] Make an order!");
            Console.WriteLine("[2] Add products to an order!");
            Console.WriteLine("[3] View details of an order!");
            Console.WriteLine("[4] place an order!");
            Console.WriteLine("[5] View order history!");
            Console.WriteLine("[x] Exit");
            string input = InputValidation.validString();

            switch (input.ToLower())
            {
                case "1":
                    //Make an order!
                    break;

                case "2":
                    //Add products to an order!
                    break;
                case "3":
                    //View details of an order!
                    break;

                case "4":
                    //place an order!
                    break;
                case "5":
                    //View order history!
                    break;

                case "x":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid input, try again");
                    break;
            }

        } while (!exit);
    }

    // create user or customer
    private void CreateNewUser()
    {

        Console.WriteLine("Creating New Customer");

        Console.WriteLine("Enter Your Last Name: ");
        string lastname = InputValidation.validString();

        Console.WriteLine("Enter Your First Name: ");
        string firstname = InputValidation.validString();

        Console.WriteLine("Enter Your UserName: ");
        string username = InputValidation.validString();

        bool same = false;
        Console.WriteLine("Enter Your Password: ");
        string password = InputValidation.validString();
        do
        {

            Console.WriteLine("Confirm Your Password: ");
            string passwordconf = InputValidation.validString();
            same = password == passwordconf ? true : false;
        } while (!same);

        User userToCreate = new User();
        userToCreate.FirstName = firstname;
        userToCreate.LastName = lastname;
        userToCreate.UserName = username;
        userToCreate.Password = password;
        userToCreate.IsAdmin = false;


        User? createdUser = _bl.createNewUser(userToCreate);
        if (createdUser != null)
        {
            OutputMessage.SucessCreation(createdUser.FirstName);
            PortalCustomer(createdUser);
        }
        else
        {
            OutputMessage.ErrorCreation();
        }

    }

    private void LogOut()
    {
        Console.WriteLine("Goodbye!");
        new MainMenu(_bl).Start();
    }
}
