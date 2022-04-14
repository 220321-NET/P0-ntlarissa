using System.ComponentModel.DataAnnotations;
using Models;

namespace UI;

public class CustomerMenu
{
   private readonly HttpService _httpService;

    //Dependency injection
    public CustomerMenu(HttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task Start()
    {
       await HomePageCustomer();


    }


    //home page customer
    private async Task HomePageCustomer()
    {
        bool exit = false;
        do
        {

            Console.WriteLine("\nWhat would you like to do today?");
            Console.WriteLine("[1] Log IN");
            Console.WriteLine("[2] Sign UP");
            Console.WriteLine("[x] Exit");
            string input = InputValidation.validString();

            switch (input.ToLower())
            {
                case "1":
                    //Login to an app
                   await LogToUserAsync();
                    break;

                case "2":
                    //create a new customer
                    await CreateNewUser();
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
    private async Task PortalCustomer(User customer, Order orderCustomer)
    {
        bool exit = false;
        do
        {

            Console.WriteLine("\nWhat would you like to do today?");
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
                    await AddProductToOrder(customer, orderCustomer);
                    break;

                case "2":
                    //Remove products to an order!
                    await RemoveProductToOrder(customer, orderCustomer);
                    break;
                case "3":
                    //View details of an order!
                    await ViewDetailsOrder(customer, orderCustomer);
                    break;

                case "4":
                    //place an order!
                    await PlaceOrder(customer, orderCustomer);
                    break;
                case "5":
                    //View order history!
                    await ViewHistory(customer, orderCustomer);
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
    private async Task LogToUserAsync()
    {

        Console.WriteLine("Logging  Customer");

        Console.WriteLine("Enter Your UserName: ");
        string username = InputValidation.validString();
        Console.WriteLine("Enter Your Password: ");
        string? password = InputValidation.validString();

        //connect to the database if the user exit return all the information to build user objet

        User? gotUser = await _httpService.GetUserAsync(username);
        if (gotUser != null && gotUser.Password == password)
        {
            OutputMessage.SucessConnexion(gotUser.FirstName);
            Order order = new Order();
            order.OrderRef = Guid.NewGuid().ToString("N");
            order.CustomerID = gotUser.ID;
            order.OrderDate = DateTime.Now;
            await PortalCustomer(gotUser, order);

        }
        else
        {
            OutputMessage.ErrorConnexion();
        }
    }



    //create user or customer
    private async Task CreateNewUser()
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


        User? createdUser = await _httpService.createNewUser(userToCreate);
        if (createdUser != null)
        {
            OutputMessage.SucessCreation(createdUser.FirstName);
            Order order = new Order();
            order.OrderRef = Guid.NewGuid().ToString("N");
            order.CustomerID = createdUser.ID;
            order.OrderDate = DateTime.Now;
            await new CustomerMenu(_httpService).Start();
        }
        else
        {
            OutputMessage.ErrorCreation();
        }

    }

    // private void LogOut()
    // {
    //     Console.WriteLine("Goodbye!");
    //     new MainMenu(_bl).Start();
    // }



    private async Task AddProductToOrder(User customer, Order orderMaking)
    {

        // get all product 
        List<Product>? allProducts = await _httpService.GetAllProductAsync();
        List<Product>? allProductsDisplay = new List<Product>();
        if (allProducts != null)
        {

            Console.WriteLine("\nHere are all the products in the store\n");
            foreach (Product productToDisplay in allProducts)
            {//Display only the products do not exist in order making
                if (!orderMaking.Products.Exists(x => x.IDProduct == productToDisplay.IDProduct))
                {

                    Console.WriteLine(productToDisplay);
                    System.Console.WriteLine("      ============================================================================");
                    allProductsDisplay.Add(productToDisplay);
                }
            }
            if (allProductsDisplay.Count < 1)
            {
                System.Console.WriteLine("\nYour cart contains all the products. Choose remove or update. \n");
                await PortalCustomer(customer, orderMaking);
            }
            else
            {
                System.Console.WriteLine("\nWhich product do you want to add. Enter the  Product ID\n");
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
                            await PortalCustomer(customer, orderMaking);
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

    private async Task RemoveProductToOrder(User customer, Order orderMaking)
    {
        if (orderMaking.Products != null && orderMaking.Products.Count > 0)
        {

            Console.WriteLine("Here are all the products in your cart\n");
            foreach (Product productToDisplay in orderMaking.Products)
            {//Display  the products  exist in order making
                Console.WriteLine(productToDisplay);
                System.Console.WriteLine("      ========================================================================");

            }
            System.Console.WriteLine("\nWhich product do you want to remove. Enter the  Product ID\n");
        EnterID:
            int id = InputValidation.validIntPositif();
            if (orderMaking.Products.Exists(x => x.IDProduct == id))
            {
                Product? productToRemove = orderMaking.Products.Find(x => x.IDProduct == id);
                if (productToRemove != null)
                {

                    orderMaking.Products.Remove(productToRemove);
                    orderMaking.OrderTotal -= productToRemove.ProductQuantity * productToRemove.ProductPrice;
                    await PortalCustomer(customer, orderMaking);
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

    private async Task ViewDetailsOrder(User customer, Order orderMaking)
    {
        System.Console.WriteLine(orderMaking);
        System.Console.WriteLine("==============================================================================\n");
        await PortalCustomer(customer, orderMaking);
    }

    private async Task PlaceOrder(User customer, Order orderMaking)
    {
        Order? placedOrder = await _httpService.placeOrder(orderMaking);
        if (placedOrder != null)
        {
            Order order = new Order();
            order.OrderRef = Guid.NewGuid().ToString("N");
            OutputMessage.SuccessPlaceOrder();
            await PortalCustomer(customer, order);
        }
        else
        {
            OutputMessage.ErrorOperation();
        }
    }
    private async Task ViewHistory(User customer, Order orderCustomer)
    {
        List<Order>? gethistory = await _httpService.getHistoryOrderAsync(customer.ID);
        if (gethistory != null)
        {
            Console.WriteLine("Here are all  your history\n");
            foreach (Order orderDisplay in gethistory)
            {//Display  the products  exist in order making
                Console.WriteLine(orderDisplay);
                System.Console.WriteLine("          ===========================================================================");

            }
            await PortalCustomer(customer, orderCustomer);
        }
        else
        {
            OutputMessage.ErrorOperation();
        }
    }
}
