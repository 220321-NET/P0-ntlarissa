using Microsoft.AspNetCore.Mvc;
using BL;
using Models;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[UserController]")]
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
    [HttpGet]
    public async Task<ActionResult<User>> GetAsync([FromQuery] User userToGet)
    {
        User? gotUser = await _bl.getUserAsync(userToGet);
        if (gotUser != null)
        {
            return Ok(gotUser);
        }
        return NoContent();
    }

}