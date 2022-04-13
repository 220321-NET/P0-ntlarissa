namespace DL;

/// <summary>
/// Interface for accessing data
/// </summary>
public interface IStoreDL
{

    // /// <summary>
    // /// check if customer exists in the dataset.
    // /// </summary>
    // /// <param name="userName">Username of customer.</param>
    // /// <param name="password">Password of customer.</param>
    // User? customerConnexion(string userName, string password);


    /// <summary>
    /// adds a new user
    ///</summary>
    ///<param name="userToCreate"> User object to be inserted or added</param>
    //Task<User> createNewUserAsync(User userToCreate);
    User createNewUser(User userToCreate);


    /// <summary>
    /// get an existant user
    ///</summary>
    ///<param name="userToGet"> User object to be got</param>
    Task<User> getUserAsync(string username);

    // StoreFront addStoreFront(StoreFront storeToAdd);

     Product addProduct(Product productToAdd);

    // List<Product> GetAllProductByStore(int store);
     Task<List<Product>> GetAllProductAsync();
    // List<User> GetAllCustomer();

    // Product updateProduct(Product productToUpdate);

    // List<Order> getHistoryOrder(int id);

    // Order placeOrder(Order orderToPlace);

    // // /// <summary>
    // // /// adds a new manager
    // // ///</summary>
    // // ///<param name="managerToCreate"> Manager object to be inserted</param>
    // // User createNewManager(User managerToCreate);

    // // /// <summary>
    // // /// adds a new product
    // // ///</summary>
    // // ///<param name="productToCreate"> Product object to be inserted</param>
    // // Product createNewProduct(Product productToCreate);

    // // /// <summary>
    // // /// places a new order
    // // ///</summary>
    // // ///<param name="orderToPlace"> Order object to be inserted.</param>
    // // void placeNewOrder(Order orderToPlace);

    // // /// <summary>
    // // /// adds product to an order
    // // ///</summary>
    // // ///<param name="orderToUpdate"> Order object to be updated.</param>
    // // ///<param name="productToAdd"> Product object to be added to order.</param>
    // // void addProductToOrder(Order orderToUpdate, Product productToAdd );

    // // /// <summary>
    // // /// Retrieves all orders
    // // /// </summary>
    // // /// <returns>List of orders, if empty, returns an empty list</returns>
    // // List<Order> GetAll();









}