using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Web.Utilities
{
    public class CacheService : ICacheService
    {
        public void Set<T>(string cacheKey, DateTimeOffset absoluteExpiration, Func<T> getItemCallback) where T : class
        {
            T item = getItemCallback();
            if (item != null)
            {
                if (MemoryCache.Default.Contains(cacheKey))
                {
                    MemoryCache.Default.Set(cacheKey, item, absoluteExpiration);
                }
                else
                {
                    MemoryCache.Default.Add(cacheKey, item, absoluteExpiration);
                }
            }
        }
        public T Get<T>(string cacheKey) where T : class
        {
            T item = MemoryCache.Default.Get(cacheKey) as T;
            return item;
        }
        public bool Remove(string cacheKey)
        {
            try
            {
                MemoryCache.Default.Remove(cacheKey);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    interface ICacheService
    {
        void Set<T>(string cacheKey, DateTimeOffset absoluteExpiration, Func<T> getItemCallback) where T : class;
        T Get<T>(string cacheKey) where T : class;
    }
}
