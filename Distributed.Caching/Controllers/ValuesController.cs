using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Distributed.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController(IDistributedCache distributedCache) : ControllerBase
    {
        [HttpGet("set")]
        public async Task<IActionResult> Set(string name, string surname)
        {
            await distributedCache.SetStringAsync("name", name, options: new()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
            await distributedCache.SetAsync("surname", Encoding.UTF8.GetBytes(surname), options: new()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
            return Ok();
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var name = await distributedCache.GetStringAsync("name");
            var surnameBytes = await distributedCache.GetAsync("surname");
            var surname = surnameBytes is not null ? Encoding.UTF8.GetString(surnameBytes) : null;
            return Ok(new { name, surname });
        }

    }
}
