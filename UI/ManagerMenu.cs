using System.ComponentModel.DataAnnotations;
using Models;

namespace UI;

public class ManagerMenu
{
    public void Start()
    {
        Console.WriteLine("Welcome to Store App");
        bool exit = false;
        do
        {

            Console.WriteLine("What would you like to do today?");
            Console.WriteLine("[1] Login IN");
            Console.WriteLine("[2] Sign UP");
            Console.WriteLine("[x] Exit");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    //Login to an app
                    Console.WriteLine("Login!!!");
                    break;

                case "2":
                    //create a new customer
                    Console.WriteLine("Registration!!!");
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

    // create user or customer
    private void CreateNewUser()
    {

    //label, marks a spot in the code base that we can jump to later
    EnterUserData:
        Console.WriteLine("Creating New User");

        Console.WriteLine("Enter Your Last Name: ");
        string? lastname = Console.ReadLine();

        Console.WriteLine("Enter Your First Name: ");
        string? firstname = Console.ReadLine();

        Console.WriteLine("Enter Your UserName: ");
        string? username = Console.ReadLine();

        bool same = false;
        Console.WriteLine("Enter Your Password: ");
        string? password = Console.ReadLine();
        do
        {

            Console.WriteLine("Confirm Your Password: ");
            string? passwordconf = Console.ReadLine();
            same = password == passwordconf? true : false;
        } while (!same);



        User userToCreate = new User();
        try
        {
            userToCreate.FirstName = firstname!;
            userToCreate.LastName = lastname!;
            userToCreate.UserName = username!;
        }
        catch (ValidationException ex)
        {
            Console.WriteLine(ex.Message);
            goto EnterUserData;
        }
        finally
        {
            //do stuff here, such as cleaning up the outside resources
        }

    }
}
