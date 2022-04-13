namespace BL;

public interface IStoreBL
{
    User createNewUser(User userToCreate);
    Task<User> getUserAsync(string username);



    // StoreFront addStoreFront(StoreFront storeToAdd);
    Product addProduct(Product productToAdd);
    // List<Product> GetAllProductByStore(int store);
         Task<List<Product>> GetAllProductAsync();

    // Product updateProduct(Product productToUpdate);
    // Order placeOrder(Order orderToPlace);

    // List<Order> getHistoryOrder(int id);

    // List<User> GetAllCustomer();
}