using DotnetShop.WeatherForecast.Models;

namespace DotnetShop.WeatherForecast.Services
{
    public class DisabledWeatherForecaster : IWeatherForecaster
    {
        public bool ForecastEnabled => false;

        public Task<CurrentWeatherResult> GetCurrentWeatherAsync()
        {
            var result = new CurrentWeatherResult
            {
                WeatherType = "The weather forecast feature is not currently available."
            };

            return Task.FromResult(result);
        }
    }
}
