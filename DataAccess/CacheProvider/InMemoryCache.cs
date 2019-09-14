using Microsoft.Extensions.Caching.Memory;
using System;

namespace DataAccess.CacheProvider
{
    public class InMemoryCache : ICacheProvider
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T Fetch<T>(string key)
        {
            T entry;

            if (!_memoryCache.TryGetValue(key, out entry))
            {
                _memoryCache.Set(key, entry, new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                    .SetPriority(CacheItemPriority.Low)
                    );
            }

            return entry;
        }

        public bool Save(string key, object value)
        {
            try
            {
                _memoryCache.Set(key, value, new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                            .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                            .SetPriority(CacheItemPriority.Low)
                            );

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}