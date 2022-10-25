using DotnetShop.WeatherForecast.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace DotnetShop.WeatherForecast.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IWeatherForecaster _weatherForecaster;
        private readonly IGreetingService _greetingService;
        private readonly IConfiguration _configuration;

        public IndexModel(IWeatherForecaster weatherForecaster,
            IGreetingService greetingService,
            IConfiguration configuration)
        {
            _weatherForecaster = weatherForecaster;
            _greetingService = greetingService;
            _configuration = configuration;
        }

        public string Greeting { get; private set; }
        public bool ShowGreeting => !string.IsNullOrEmpty(Greeting);
        public string ForecastSectionTitle { get; private set; }
        public string WeatherDescription { get; private set; }
        public bool ShowWeatherForecast { get; private set; }

        public async void OnGet()
        {
            var homePageFeatures = _configuration.GetSection("Features:HomePage");
            
            if (homePageFeatures != null && homePageFeatures.GetValue<bool>("EnableGreeting"))
            {
                Greeting = _greetingService.GetRandomGreeting();
            }

            ShowWeatherForecast = homePageFeatures.GetValue<bool>("EnableWeatherForecast");

            if (ShowWeatherForecast)
            {
                var title = homePageFeatures["ForecastSectionTitle"];
                ForecastSectionTitle = string.IsNullOrEmpty(title) ? "How's the weather?" : title;

                var currentWeather = await _weatherForecaster.GetCurrentWeatherAsync();

                if (currentWeather != null)
                {
                    switch (currentWeather.Description)
                    {
                        case "Sun":
                            WeatherDescription = "It's sunny right now. A great day for shopping!";
                            break;

                        case "Cloud":
                            WeatherDescription = "It's cloudy at the moment the fall has begun.";
                            break;

                        case "Rain":
                            WeatherDescription = "It's raining but you can still do shopping online.";
                            break;

                        case "Snow":
                            WeatherDescription = "It's snowing!! The sale season has just begun.";
                            break;
                    }
                }
            }
        }
    }
}
