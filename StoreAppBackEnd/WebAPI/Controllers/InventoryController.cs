using Microsoft.AspNetCore.Mvc;
using BL;
using Models;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class InventoryController : ControllerBase
{
    private readonly IStoreBL _bl;
    private IMemoryCache _cache;
    public InventoryController(IStoreBL bl, IMemoryCache cache)
    {
        _bl = bl;
        _cache = cache;
    }

    // GET api/<InventoryController>/
    [HttpGet("{id}")]
    public async Task<ActionResult<List<Product>>> GetAsync(int id)
    {
        List<Product>? allProducts = await _bl.GetAllProductByStoreASync(id);
        if (allProducts != null)
        {
            return Ok(allProducts);
        }
        return NoContent();
    }

   

}