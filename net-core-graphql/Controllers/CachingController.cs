using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;

namespace net_core_graphql.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class CachingController : ControllerBase
    {
        private IMemoryCache _memoryCache;
        private ILogger<CachingController> _logger;
        private string _dataKey = "Date-Created";
        private string _message;

        public CachingController(IMemoryCache cache, ILogger<CachingController> logger)
        {
            _memoryCache = cache;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!_memoryCache.TryGetValue(_dataKey, out _message))
            {
                _message = $"New value created at {DateTime.Now}.";
                _memoryCache.Set(_dataKey, _message, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(5)));
                _logger.LogInformation($"> {_dataKey} generated and set in cache.");
            }
            else
            {
                _logger.LogInformation($"> {_dataKey} was available (and pulled) from cache.");
            }
            return Ok(_message);
        }
    }
}