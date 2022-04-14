using Microsoft.AspNetCore.Mvc;
using BL;
using Models;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class OrderController : ControllerBase
{
    private readonly IStoreBL _bl;
    private IMemoryCache _cache;
    public OrderController(IStoreBL bl, IMemoryCache cache)
    {
        _bl = bl;
        _cache = cache;
    }

    // GET api/<OrderController>/
    [HttpGet("{id}")]
    public async Task<ActionResult<List<Order>>> GetAsync(int id)
    {
        List<Order>? gethistory = await _bl.getHistoryOrder(id);
        if (gethistory != null)
        {
            return Ok(gethistory);
        }
        return NoContent();
    }

    // POST api/<OrderController>
    [HttpPost]
    public ActionResult<Order> Post([FromBody] Order orderToPlace)
    {
        Order? placedOrder = _bl.placeOrder(orderToPlace);
        if (placedOrder != null)
        {


            //check the cache, is there a cached all users?
            //If so, update the cache
            List<Order> orders = new List<Order>();
            if (_cache.TryGetValue<List<Order>>("AllOrders", out orders))
            {
                orders.Add(placedOrder);
                _cache.Set("AllOrders", orders, new TimeSpan(0, 1, 0));
            }

            return Created("api/Order", placedOrder);
        }

        else
        {
            return NoContent();
        }
    }

}