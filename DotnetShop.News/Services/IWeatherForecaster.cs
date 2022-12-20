using DotnetShop.WeatherForecast.Models;

namespace DotnetShop.WeatherForecast.Services
{
    public interface IWeatherForecaster
    {
        Task<CurrentWeatherResult> GetCurrentWeatherAsync();
        bool ForecastEnabled { get; }
    }
}
