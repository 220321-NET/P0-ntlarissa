using DL;

namespace BL;

public class StoreBL : IStoreBL
{
    private readonly IStoreDL _repo;
    public StoreBL(IStoreDL repo)
    {
        _repo = repo;
    }

    public User createNewUser(User userToCreate)
    {
        return _repo.createNewUser(userToCreate);
    }

    public async Task<User> getManagerAsync(string username)
    {
        return await _repo.getManagerAsync(username);
    }

    public User createNewManager(User userToCreate)
    {
        return _repo.createNewManager(userToCreate);
    }

    public async Task<User> getUserAsync(string username)
    {
        return await _repo.getUserAsync(username);
    }

    public StoreFront addStoreFront(StoreFront storeToAdd)
    {
        return _repo.addStoreFront(storeToAdd);
    }
    public Product addProduct(Product productToAdd)
    {
        return _repo.addProduct(productToAdd);
    }

    // public List<Product> GetAllProductByStore(int store)
    // {
    //     return _repo.GetAllProductByStore(store);
    // }

    public async Task<List<Product>> GetAllProductAsync()
    {
        return await _repo.GetAllProductAsync();
    }

    // public Product updateProduct(Product productToUpdate)
    // {
    //     return _repo.updateProduct(productToUpdate);
    // }

    public Order placeOrder(Order orderToPlace)
    {
        return _repo.placeOrder(orderToPlace);
    }

    public async Task<List<Order>> getHistoryOrder(int id)
    {
        return await _repo.getHistoryOrder(id);
    }
    // public List<User> GetAllCustomer(){
    //     return _repo.GetAllCustomer();
    // }


}
