namespace DotnetShop.WeatherForecast.Configurations
{
    public class ExternalServicesConfig
    {
        public const string WeatherApi = "WeatherApi";
        public const string SportsNewsApi = "SportsNewsApi";

        public int MinsToCache { get; set; }
    }
}
