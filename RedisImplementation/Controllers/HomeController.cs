using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RedisImplementation.Models;
using StackExchange.Redis;
using System.Diagnostics;

namespace RedisImplementation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConnectionMultiplexer _redis;
        private readonly IDistributedCache distributedCache;

        public HomeController(ILogger<HomeController> logger, IConnectionMultiplexer redis, IDistributedCache distributedCache)
        {
            _logger = logger;
            this._redis = redis;
            this.distributedCache = distributedCache;
        }

        public async Task<IActionResult> Index()
        {
            var db = _redis.GetDatabase();
            await db.StringSetAsync("myKey", "Dummy data to test Redis");
            var value = await db.StringGetAsync("myKey");
            TempData["data"] = value;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
