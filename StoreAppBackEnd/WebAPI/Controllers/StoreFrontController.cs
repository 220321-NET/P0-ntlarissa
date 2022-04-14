using Microsoft.AspNetCore.Mvc;
using BL;
using Models;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class StoreFrontController : ControllerBase
{
    private readonly IStoreBL _bl;
    private IMemoryCache _cache;
    public StoreFrontController(IStoreBL bl, IMemoryCache cache)
    {
        _bl = bl;
        _cache = cache;
    }

    // // GET api/<StoreFrontController>/
    // [HttpGet]
    // public async Task<ActionResult<List<Product>>> GetAsync()
    // {
    //     List<Product>? allProducts = await _bl.GetAllProductAsync();
    //     if (allProducts != null)
    //     {
    //         return Ok(allProducts);
    //     }
    //     return NoContent();
    // }

    // POST api/<StoreFrontController>
    [HttpPost]
    public ActionResult<StoreFront> Post([FromBody] StoreFront storeToAdd)
    {
        StoreFront? addedStore = _bl.addStoreFront(storeToAdd);
        if (addedStore != null)
        {


            //check the cache, is there a cached all users?
            //If so, update the cache
            List<StoreFront> storeFronts = new List<StoreFront>();
            if (_cache.TryGetValue<List<StoreFront>>("AllStores", out storeFronts))
            {
                storeFronts.Add(addedStore);
                _cache.Set("AllStores", storeFronts, new TimeSpan(0, 1, 0));
            }

            return Created("api/Product", addedStore);
        }

        else
        {
            return NoContent();
        }
    }

}