namespace DotnetShop.WeatherForecast.Services
{
    public interface IGreetingService
    {
        string GetRandomGreeting();
        string GetRandomLoginGreeting(string name);
    }
}
