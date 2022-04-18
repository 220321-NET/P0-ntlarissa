namespace BL;

public interface IStoreBL
{
    User createNewUser(User userToCreate);
    Task<User> getUserAsync(string username);

    User createNewManager(User userToCreate);
    Task<User> getManagerAsync(string username);



    StoreFront addStoreFront(StoreFront storeToAdd);
    Product addProduct(Product productToAdd);
    Task<List<Product>> GetAllProductByStoreASync(int store);
    Task<List<Product>> GetAllProductAsync();

    Product updateProduct(Product productToUpdate);
    Order placeOrder(Order orderToPlace);

    Task<List<Order>> getHistoryOrder(int id);
    Task<List<HistoryByUser>> getHistoryByUsers();
    Task<List<HistoryByStore>> getHistoryByStores();

    // List<User> GetAllCustomer();
}