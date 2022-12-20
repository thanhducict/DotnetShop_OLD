using DotnetShop.News.Services;
using DotnetShop.WeatherForecast.Configurations;
using DotnetShop.WeatherForecast.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace DotnetShop.WeatherForecast.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IGreetingService _greetingService;
        private readonly IWeatherForecaster _weatherForecaster;
        private readonly ISportsNewsService _sportsNewsService;
        private readonly HomePageConfiguration _homePageConfiguration;

        public IndexModel(IGreetingService greetingService,
            IWeatherForecaster weatherForecaster,
            ISportsNewsService sportsNewsService,
            IOptionsSnapshot<HomePageConfiguration> options)
        {
            _greetingService = greetingService;
            _weatherForecaster = weatherForecaster;
            _sportsNewsService = sportsNewsService;
            _homePageConfiguration = options.Value;

            GreetingColour = _greetingService.GreetingColour ?? "black";
        }

        public string Greeting { get; private set; }
        public bool ShowGreeting => !string.IsNullOrEmpty(Greeting);
        public string GreetingColour { get; private set; }
        public string ForecastSectionTitle { get; private set; }
        public string WeatherDescription { get; private set; }
        public bool ShowWeatherForecast { get; private set; }
        public string SportsNews { get; set; }

        public async void OnGet()
        {
            if (_homePageConfiguration.EnableGreeting)
            {
                Greeting = _greetingService.GetRandomGreeting();
            }

            ShowWeatherForecast = _homePageConfiguration.EnableWeatherForecast && _weatherForecaster.ForecastEnabled;

            if (ShowWeatherForecast)
            {
                var title = _homePageConfiguration.ForecastSectionTitle;
                ForecastSectionTitle = string.IsNullOrEmpty(title) ? "How's the weather?" : title;

                var currentWeather = await _weatherForecaster.GetCurrentWeatherAsync();

                if (currentWeather != null)
                {
                    switch (currentWeather.WeatherType)
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

            var sportsNewsResult = _sportsNewsService.GetSportsNews();

            SportsNews = sportsNewsResult;
        }
    }
}
