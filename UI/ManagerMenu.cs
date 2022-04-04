using System.ComponentModel.DataAnnotations;
using Models;
using BL;

namespace UI;

public class ManagerMenu
{
    private readonly IStoreBL _bl;

    //Dependency injection
    public ManagerMenu(IStoreBL bl)
    {
        _bl = bl;
    }

    public void Start()
    {
        HomePageManager();


    }


    //home page customer
    private void HomePageManager()
    {
        bool exit = false;
        do
        {

            Console.WriteLine("What would you like to do today?");
            Console.WriteLine("[1] Log IN");
            Console.WriteLine("[x] Exit");
            string input = InputValidation.validString();

            switch (input.ToLower())
            {
                case "1":
                    //Login to an app
                    LogToUser();
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

        Console.WriteLine("Logging  Manager");

        Console.WriteLine("Enter Your UserName: ");
        string username = InputValidation.validString();
        Console.WriteLine("Enter Your Password: ");
        string? password = InputValidation.validString();

        //connect to the database if the user exit return all the information to build user objet

        User userToGet = new User();
        userToGet.UserName = username;
        userToGet.Password = password;
        userToGet.IsAdmin = true;
        User? gotUser = _bl.getUser(userToGet);
        if (gotUser != null)
        {
            OutputMessage.SucessConnexion(gotUser.FirstName);
            PortalManager(gotUser);

        }
        else
        {
            OutputMessage.ErrorConnexion();
        }
    }

    private void PortalManager(User manager)
    {
        bool exit = false;
        do
        {

            Console.WriteLine("What would you like to do today?");
            Console.WriteLine("[1] Add a new manager!");
            Console.WriteLine("[2] Add products!");
            Console.WriteLine("[3] View  order history by location!");
            Console.WriteLine("[4] View order history by customer!");
            Console.WriteLine("[5] View inventory!");
            Console.WriteLine("[6] Replenish inventory!");
            Console.WriteLine("[7] Add storeFront!");
            Console.WriteLine("[x] Exit");
            string input = InputValidation.validString();

            switch (input.ToLower())
            {
                case "1":
                    //Add a new manager!
                    CreateNewUser(manager);
                    break;

                case "2":
                    //Add products!
                    AddProduct(manager);
                    break;
                case "3":
                    //View  order history by location!
                    break;

                case "4":
                    //View order history by customer!
                    break;
                case "5":
                    //View inventory!
                    ViewInventory(manager);
                    break;
                case "6":
                    //Replenish inventory!
                    ReplenishInventory(manager);
                    break;
                case "7":
                    //Add storeFront!
                    AddStoreFront(manager);
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
    private void CreateNewUser(User manager)
    {

        Console.WriteLine("Creating New Manager");

        Console.WriteLine("Enter the store ID: ");
        int storeid = InputValidation.validIntPositif();

        Console.WriteLine("Enter his/her Last Name: ");
        string lastname = InputValidation.validString();

        Console.WriteLine("Enter his/her First Name: ");
        string firstname = InputValidation.validString();

        Console.WriteLine("Enter his/her UserName: ");
        string username = InputValidation.validString();

        bool same = false;
        Console.WriteLine("Enter his/her Password: ");
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
        userToCreate.IDStore = storeid;
        userToCreate.IsAdmin = true;


        User? createdUser = _bl.createNewUser(userToCreate);
        if (createdUser != null)
        {
            OutputMessage.SucessCreation(createdUser.FirstName);
            PortalManager(manager);
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

    // add store
    private void AddStoreFront(User manager)
    {

        Console.WriteLine("Creating New Store");

        Console.WriteLine("Enter Name: ");
        string name = InputValidation.validString();

        Console.WriteLine("Enter address line1: ");
        string line1 = InputValidation.validString();

        Console.WriteLine("Enter address line2: ");
        string? line2 = Console.ReadLine();

        Console.WriteLine("Enter address city: ");
        string city = InputValidation.validString();

        Console.WriteLine("Enter address state: ");
        string state = InputValidation.validString();

        Console.WriteLine("Enter address country: ");
        string country = InputValidation.validString();

        Console.WriteLine("Enter address zipCode: ");
        string zipCode = InputValidation.validString();

        StoreFront storeToAdd = new StoreFront();
        storeToAdd.Name = name;
        storeToAdd.Line1 = line1;
        storeToAdd.Line2 = line2!;
        storeToAdd.City = city;
        storeToAdd.State = state;
        storeToAdd.Country = country;
        storeToAdd.ZipCode = zipCode;

        StoreFront? addedStore = _bl.addStoreFront(storeToAdd);
        if (addedStore != null)
        {
            OutputMessage.SucessCreation("New Store");
            PortalManager(manager);
        }
        else
        {
            OutputMessage.ErrorOperation();
        }

    }


    private void AddProduct(User manager)
    {

        Console.WriteLine("Creating New Product");

        Console.WriteLine("Enter Name: ");
        string name = InputValidation.validString();

        Console.WriteLine("Enter reference or description: ");
        string productRef = InputValidation.validString();

        Console.WriteLine("Enter quantity: ");
        float quantity = InputValidation.validFloatPositif();

        Console.WriteLine("Enter price: ");
        float price = InputValidation.validFloatPositif();


        // Console.WriteLine("Enter the store ID: ");
        // int storeid = InputValidation.validIntPositif();

        Product productToAdd = new Product();
        productToAdd.NameProduct = name;
        productToAdd.ProductRef = productRef;
        productToAdd.ProductQuantity = quantity;
        productToAdd.ProductPrice = price;
        productToAdd.IDStore = manager.IDStore;

        Product? addedStore = _bl.addProduct(productToAdd);
        if (addedStore != null)
        {
            OutputMessage.SucessCreation("New Product");
            PortalManager(manager);
        }
        else
        {
            OutputMessage.ErrorOperation();
        }

    }

    private void ViewInventory(User manager)
    {

        // get all product by store

        List<Product>? allProducts = _bl.GetAllProductByStore(manager.IDStore);
        if (allProducts != null)
        {
            Console.WriteLine("Here are all the products in the store");
            foreach (Product productToDisplay in allProducts)
            {
                Console.WriteLine(productToDisplay);
            }
            PortalManager(manager);
        }
        else
        {
            OutputMessage.ErrorOperation();
        }

    }

    private void ReplenishInventory(User manager)
    {

        // get all product by store and update

        List<Product>? allProducts = _bl.GetAllProductByStore(manager.IDStore);
        if (allProducts != null)
        {
            Console.WriteLine("Here are all the products in the store");
            foreach (Product productToDisplay in allProducts)
            {
                Console.WriteLine(productToDisplay);
                System.Console.WriteLine("==============================================================================");
            }
            System.Console.WriteLine("Enter the  Product ID");
        EnterID:
            int id = InputValidation.validIntPositif();
            if (allProducts.Exists(x => x.IDProduct == id))
            {
                Product? productToUpdate = allProducts.Find(x => x.IDProduct == id);
                if (productToUpdate != null)
                {
                    //System.Console.WriteLine(productToUpdate);
                    Console.WriteLine($"Enter the new quantity or {productToUpdate.ProductQuantity} for the old value: ");
                    float quantity = InputValidation.validFloatPositif();

                    Console.WriteLine($"Enter the new price or {productToUpdate.ProductPrice} for the old value: ");
                    float price = InputValidation.validFloatPositif();
                    productToUpdate.ProductPrice = price;
                    productToUpdate.ProductQuantity = quantity;
                    //System.Console.WriteLine(productToUpdate);
                    Product? updatedProduct = _bl.updateProduct(productToUpdate);
                    if (updatedProduct != null)
                    {
                        OutputMessage.SucessUpdate("Inventory ");
                        PortalManager(manager);
                    }
                    else
                    {
                        OutputMessage.ErrorOperation();
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
        else
        {
            OutputMessage.ErrorOperation();
        }

    }
}
