using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Commune.Caching
{
    public class CommuneCache: ICommuneCache
    {
        private readonly IMemoryCache _cache;
        private readonly string TOKENKEY = "tokenKey";
        private readonly string EMAILKEY = "emailKey";

        public CommuneCache()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        public void SetToken(string value)
        {
            _cache.Set(TOKENKEY, value, DateTime.Now.AddDays(1));
        }

        public string GetToken()
        {
            if (_cache.TryGetValue(TOKENKEY, out string value))
                return value;
            else
                return default(string);
        }

        public void Set<T>(string key, T value)
        {
            _cache.Set(key, value, DateTime.Now.AddDays(1));
        }

        public T Get<T>(string key)
        {
            if (_cache.TryGetValue(key, out T value))
                return value;
            else
                return default(T);
        }

        public string GetEmail()
        {
            if (_cache.TryGetValue(EMAILKEY, out string value))
                return value;
            else
            {
                var token = GetToken();

                var email = "";
                Set(EMAILKEY, email);
                return email;
            }
        }
    }
}
