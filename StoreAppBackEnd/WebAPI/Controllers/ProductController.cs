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

    // POST api/<ProductController>
    [HttpPost]
    public ActionResult<Product> Post([FromBody] Product productToAdd)
    {
        Product? addedProduct = _bl.addProduct(productToAdd);
        if (addedProduct != null)
        {


            //check the cache, is there a cached all users?
            //If so, update the cache
            List<Product> products = new List<Product>();
            if (_cache.TryGetValue<List<Product>>("AllProducts", out products))
            {
                products.Add(addedProduct);
                _cache.Set("AllProducts", products, new TimeSpan(0, 1, 0));
            }

            return Created("api/Product", addedProduct);
        }

        else
        {
            return NoContent();
        }
    }

    [HttpPut("UpdateProduct")]
        public Product Put(Product productToUpdate)
        {
            return _bl.updateProduct(productToUpdate);
        }

}