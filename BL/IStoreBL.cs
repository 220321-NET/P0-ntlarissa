namespace BL;

public interface IStoreBL
{
    User createNewUser(User userToCreate);
    User getUser(User userToGet);

    StoreFront addStoreFront(StoreFront storeToAdd);
    Product addProduct(Product productToAdd);
    List<Product> GetAllProductByStore(int store);
    List<Product> GetAllProduct();
    Product updateProduct(Product productToUpdate);
    Order placeOrder(Order orderToPlace);

    List<Order> getHistoryOrder(int id);

    List<User> GetAllCustomer();
}