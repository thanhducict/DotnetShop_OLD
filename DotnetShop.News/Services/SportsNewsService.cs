using DotnetShop.News.Services;
using DotnetShop.WeatherForecast.Configurations;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace DotnetShop.WeatherForecast.Services
{
    public class SportsNewsService : ISportsNewsService
    {
        private readonly IMemoryCache _cache;
        private readonly IOptionsMonitor<ExternalServicesConfig> _sportsNewsApiConfig;
        private const string CacheKey = "SportsNews";

        public SportsNewsService(IOptionsMonitor<ExternalServicesConfig> options, IMemoryCache cache)
        {
            _sportsNewsApiConfig = options;
            _cache = cache;
        }

        public ExternalServicesConfig ExternalServicesConfig { get; }

        public string GetSportsNews()
        {
            if (!_cache.TryGetValue<string>(CacheKey, out var result))
            {
                string[] results = {
                    "Manchester United 8 - 2 Arsenal",
                    "Manchester United 2 - 0 Chelsea",
                    "Manchester United 3 - 0 Liverpool",
                    "Manchester United 4 - 3 Manchester City"
                };
                result = results[new Random().Next(0, 4)];

                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_sportsNewsApiConfig.Get(ExternalServicesConfig.SportsNewsApi).MinsToCache)
                };
                _cache.Set(CacheKey, result, cacheOptions);
            }

            return result;
        }
    }
}
