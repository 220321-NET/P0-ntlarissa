using Microsoft.AspNetCore.Mvc;
using BL;
using Models;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class HistoryByUserController : ControllerBase
{
    private readonly IStoreBL _bl;
    private IMemoryCache _cache;
    public HistoryByUserController(IStoreBL bl, IMemoryCache cache)
    {
        _bl = bl;
        _cache = cache;
    }

    // GET api/<HistoryByUserController>/
    [HttpGet]
    public async Task<ActionResult<List<HistoryByUser>>> GetAsync()
    {
        List<HistoryByUser>? getHistoryByUser = await _bl.getHistoryByUsers();
        if (getHistoryByUser != null)
        {
            return Ok(getHistoryByUser);
        }
        return NoContent();
    }

   

}