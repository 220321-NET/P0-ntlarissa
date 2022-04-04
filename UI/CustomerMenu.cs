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
            PortalCustomer(gotUser, new Order());

        }
        else
        {
            OutputMessage.ErrorConnexion();
        }
    }

    private void PortalCustomer(User customer, Order orderCustomer)
    {
        bool exit = false;
        do
        {

            Console.WriteLine("What would you like to do today?");
            Console.WriteLine("[1] Add products to an order!");
            Console.WriteLine("[2] Remove products to an order!");
            Console.WriteLine("[3] View details of an order!");
            Console.WriteLine("[4] place an order!");
            Console.WriteLine("[5] View order history!");
            Console.WriteLine("[x] Exit");
            string input = InputValidation.validString();

            switch (input.ToLower())
            {
                case "1":
                    //Add products to an order!
                    AddProductToOrder(customer, orderCustomer);
                    break;

                case "2":
                    //Remove products to an order!
                    RemoveProductToOrder(customer, orderCustomer);
                    break;
                case "3":
                    //View details of an order!
                    ViewDetailsOrder(customer, orderCustomer);
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
            PortalCustomer(createdUser, new Order());
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

    

    private void AddProductToOrder(User customer, Order orderMaking)
    {

        orderMaking.CustomerID = customer.ID;
        // get all product 
        List<Product>? allProducts = _bl.GetAllProduct();
        List<Product>? allProductsDisplay = new List<Product>();
        if (allProducts != null)
        {

            Console.WriteLine("Here are all the products in the store");
            foreach (Product productToDisplay in allProducts)
            {//Display only the products do not exist in order making
                if (!orderMaking.Products.Exists(x => x.IDProduct == productToDisplay.IDProduct))
                {

                    Console.WriteLine(productToDisplay);
                    System.Console.WriteLine("==============================================================================");
                    allProductsDisplay.Add(productToDisplay);
                }
            }
            if (allProductsDisplay.Count < 1)
            {
                System.Console.WriteLine("Your cart contains all the products. Choose remove or update. ");
                PortalCustomer(customer, orderMaking);
            }
            else
            {
                System.Console.WriteLine("Which product do you want to add. Enter the  Product ID");
            EnterID:
                int id = InputValidation.validIntPositif();
                if (allProductsDisplay.Exists(x => x.IDProduct == id))
                {
                    Product? productToAdd = allProductsDisplay.Find(x => x.IDProduct == id);
                    if (productToAdd != null)
                    {
                    EnterQty:
                        Console.WriteLine($"Enter the  quantity less than or equal to {productToAdd.ProductQuantity} :");

                        float quantity = InputValidation.validFloatPositif();
                        if (quantity <= productToAdd.ProductQuantity)
                        {

                            productToAdd.ProductQuantity = quantity;
                            orderMaking.Products.Add(productToAdd);
                            orderMaking.OrderTotal += productToAdd.ProductQuantity * productToAdd.ProductPrice;
                            PortalCustomer(customer, orderMaking);
                        }
                        else
                        {
                            goto EnterQty;
                        }
                    }
                    else
                    {
                        OutputMessage.ErrorOperation();
                    }

                }
                else
                {
                    Console.WriteLine($"This ID Product {id} does not exist, please enter the correct ID");
                    goto EnterID;
                }
            }
        }
        else
        {
            OutputMessage.ErrorOperation();
        }

    }

    private void RemoveProductToOrder(User customer, Order orderMaking)
    {
        if (orderMaking.Products != null)
        {

            Console.WriteLine("Here are all the products in your cart");
            foreach (Product productToDisplay in orderMaking.Products)
            {//Display  the products  exist in order making
                Console.WriteLine(productToDisplay);
                System.Console.WriteLine("==============================================================================");

            }
            System.Console.WriteLine("\nWhich product do you want to remove. Enter the  Product ID");
        EnterID:
            int id = InputValidation.validIntPositif();
            if (orderMaking.Products.Exists(x => x.IDProduct == id))
            {
                Product? productToRemove = orderMaking.Products.Find(x => x.IDProduct == id);
                if (productToRemove != null)
                {

                    orderMaking.Products.Remove(productToRemove);
                    orderMaking.OrderTotal -= productToRemove.ProductQuantity * productToRemove.ProductPrice;
                    PortalCustomer(customer, orderMaking);
                }
                else
                {
                    OutputMessage.ErrorOperation();
                }

            }
            else
            {
                Console.WriteLine($"This ID Product {id} does not exist, please enter the correct ID");
                goto EnterID;
            }
        }
        else
        {
            OutputMessage.CartEmpty();
        }

    }

    private void ViewDetailsOrder(User customer, Order orderMaking)
    {
        System.Console.WriteLine(orderMaking);
        System.Console.WriteLine("==============================================================================\n");
        PortalCustomer(customer, orderMaking);
    }
}
