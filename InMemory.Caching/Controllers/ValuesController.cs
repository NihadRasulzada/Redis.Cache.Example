using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController(IMemoryCache memoryCache) : ControllerBase
    {
        //[HttpGet("set/{name}")]
        //public void SetName(string name)
        //{
        //    memoryCache.Set("name", name);
        //}

        //[HttpGet("get")]
        //public string GetName()
        //{
        //    return memoryCache.Get<string>("name") ?? "Value not found";
        //}

        [HttpGet("setDate")]
        public void SetDate()
        {
            memoryCache.Set("date", DateTime.Now, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
        }

        [HttpGet("getDate")]
        public DateTime GetDate()
        {
            return memoryCache.Get<DateTime>("date");
        }
    }
}
