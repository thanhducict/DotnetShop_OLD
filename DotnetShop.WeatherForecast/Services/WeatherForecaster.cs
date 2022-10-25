using DotnetShop.WeatherForecast.Models;

namespace DotnetShop.WeatherForecast.Services
{
    public class WeatherForecaster : IWeatherForecaster
    {
        public bool ForecastEnabled => true;

        public async Task<CurrentWeatherResult> GetCurrentWeatherAsync()
        {
            string[] weatherTypes = { "Sun", "Cloud", "Rain", "Snow" };
            var currentWeather = weatherTypes[new Random().Next(0, 4)];
            var result = new CurrentWeatherResult
            {
                Description = currentWeather
            };

            return result;
        }
    }
}
