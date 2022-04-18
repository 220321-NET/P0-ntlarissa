using Microsoft.AspNetCore.Mvc;
using BL;
using Models;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class HistoryByStoreController : ControllerBase
{
    private readonly IStoreBL _bl;
    private IMemoryCache _cache;
    public HistoryByStoreController(IStoreBL bl, IMemoryCache cache)
    {
        _bl = bl;
        _cache = cache;
    }

    // GET api/<HistoryByStoreController>/
    [HttpGet]
    public async Task<ActionResult<List<HistoryByStore>>> GetAsync()
    {
        List<HistoryByStore>? getHistoryByStore = await _bl.getHistoryByStores();
        if (getHistoryByStore != null)
        {
            return Ok(getHistoryByStore);
        }
        return NoContent();
    }

   

}