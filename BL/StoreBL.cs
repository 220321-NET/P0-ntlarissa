using DB;
namespace BL;
public class StoreBL : IStoreBL
{
    private readonly IStoreBL _repo;
    public StoreBL(IStoreBL repo)
    {
        _repo = repo;
    }

}
