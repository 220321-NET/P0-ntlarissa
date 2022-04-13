using Microsoft.AspNetCore.Mvc;
using BL;
using Models;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class ProductController : ControllerBase
{
    private readonly IStoreBL _bl;
    private IMemoryCache _cache;
    public ProductController(IStoreBL bl, IMemoryCache cache)
    {
        _bl = bl;
        _cache = cache;
    }

    // GET api/<ProductController>/
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAsync()
    {
        List<Product>? allProducts = await _bl.GetAllProductAsync();
        if (allProducts != null)
        {
            return Ok(allProducts);
        }
        return NoContent();
    }

    // // POST api/<IssuesController>
    // [HttpPost]
    // public ActionResult<User> Post([FromBody] User userToCreate)
    // {
    //     User? createdUser = _bl.createNewUser(userToCreate);
    //     if (createdUser != null)
    //     {


    //         //check the cache, is there a cached all users?
    //         //If so, update the cache
    //         List<User> users = new List<User>();
    //         if (_cache.TryGetValue<List<User>>("AllUsers", out users))
    //         {
    //             users.Add(createdUser);
    //             _cache.Set("AllUsers", users, new TimeSpan(0, 1, 0));
    //         }

    //         return Created("api/Users", createdUser);
    //     }

    //     else
    //     {
    //         return NoContent();
    //     }
    // }

}