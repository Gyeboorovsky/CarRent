using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Common.CacheProvider
{
    public class CacheProvider : ICacheProvider
    {
        private const int CacheSeconds = 5;

        private readonly IMemoryCache _cache;

        public CacheProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// Clears cache
        /// </summary>
        /// <param name="key"></param>
        public void ClearCache(string key)
        {
            _cache.Remove(key);
        }

        /// <summary>
        /// Get cached data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetFromCache<T>(string key) where T : class
        {
            var cachedResponse = _cache.Get(key);
            return cachedResponse as T;
        }

        /// <summary>
        /// Set cache data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetCache<T>(string key, T value) where T : class
        {
            SetCache(key, value, DateTimeOffset.Now.AddSeconds(CacheSeconds));
        }

        /// <summary>
        /// Set cache data with time offset
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="duration"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void SetCache<T>(string key, T value, DateTimeOffset duration) where T : class
        {
            _cache.Set(key, value, duration);
        }

        /// <summary>
        /// Get from cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="getDataFunc"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<T> TryGetCached<T>(string key, Func<Task<T>> getDataFunc) where T : class
        {
            var result = GetFromCache<T>(key);
            if (result != null)
            {
                return result;
            }
            result = await getDataFunc();
            SetCache<T>(key, result);

            return result;
        }
    }
}
