using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;

namespace net_core_graphql.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class CachingController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CachingController> _logger;
        private const string DATA_KEY = "Date-Created";
        private string _message;

        public CachingController(IMemoryCache cache, ILogger<CachingController> logger)
        {
            _memoryCache = cache;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!_memoryCache.TryGetValue(DATA_KEY, out _message))
            {
                _message = $"New value created at {DateTime.Now}.";

                _memoryCache.Set(DATA_KEY, _message, new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                    .SetPriority(CacheItemPriority.Low)
                    );

                _logger.LogInformation($"> {DATA_KEY} generated and set in cache.");
            }
            else
            {
                _logger.LogInformation($"> {DATA_KEY} was available (and pulled) from cache.");
            }
            return Ok(_message);
        }
    }
}