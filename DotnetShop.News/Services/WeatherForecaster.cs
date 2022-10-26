using DotnetShop.WeatherForecast.Configurations;
using DotnetShop.WeatherForecast.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace DotnetShop.WeatherForecast.Services
{
    public class WeatherForecaster : IWeatherForecaster
    {
        private readonly IMemoryCache _cache;
        private readonly IOptionsMonitor<ExternalServicesConfig> _weatherApiConfig;
        private const string CacheKey = "WeatherResult";

        public WeatherForecaster(IOptionsMonitor<ExternalServicesConfig> options, IMemoryCache cache)
        {
            _weatherApiConfig = options;
            _cache = cache;
        }

        public bool ForecastEnabled => true;
        public ExternalServicesConfig ExternalServicesConfig { get; }

        public Task<CurrentWeatherResult> GetCurrentWeatherAsync()
        {
            if (!_cache.TryGetValue<CurrentWeatherResult>(CacheKey, out var result))
            {
                string[] weatherTypes = { "Sun", "Cloud", "Rain", "Snow" };
                var currentWeather = weatherTypes[new Random().Next(0, 4)];

                result = new CurrentWeatherResult
                {
                    WeatherType = currentWeather
                };


                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_weatherApiConfig.Get(ExternalServicesConfig.WeatherApi).MinsToCache)
                };
                _cache.Set(CacheKey, result, cacheOptions);
            }

            return Task.FromResult(result);
        }
    }
}
