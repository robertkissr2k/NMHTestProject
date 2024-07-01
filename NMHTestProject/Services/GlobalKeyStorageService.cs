using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NMHTestProject.Common;

namespace NMHTestProject.Services
{
    public class GlobalKeyStorageService : IGlobalKeyStorageService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IOptions<Configuration> _configurationOptions;

        public GlobalKeyStorageService(IMemoryCache memoryCache, IOptions<Configuration> configurationOptions)
        {
            _memoryCache = memoryCache;
            _configurationOptions = configurationOptions;
        }

        public TValue? Get<TValue>(string key)
        {
            return _memoryCache.Get<TValue>(key);
        }

        public void Set<TValue>(string key, TValue value)
        {
            var config = _configurationOptions.Value;
            _memoryCache.Set(key, value, config.KeyStorageValueExpiration);
        }
    }
}
