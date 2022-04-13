using Microsoft.AspNetCore.Mvc;
using BL;
using Models;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class UserController : ControllerBase
{
    private readonly IStoreBL _bl;
    private IMemoryCache _cache;
    public UserController(IStoreBL bl, IMemoryCache cache)
    {
        _bl = bl;
        _cache = cache;
    }

    // GET api/<UserController>/
    [HttpGet("{username}")]
    public async Task<ActionResult<User>> GetAsync(string username)
    {
        User? gotUser = await _bl.getUserAsync(username);
        if (gotUser != null)
        {
            return Ok(gotUser);
        }
        return NoContent();
    }

}