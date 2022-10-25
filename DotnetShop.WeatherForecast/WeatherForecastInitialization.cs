using DotnetShop.Infrastructure.Initialization;
using DotnetShop.WeatherForecast.Services;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace DotnetShop.WeatherForecast
{
    internal class WeatherForecastInitialization : IInitialization
    {
        public void ConfigureContainer(IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();
            // This creates an AssemblyPart, but does not create any related parts for items such as views.
            var part = new AssemblyPart(assembly);
            services.AddControllersWithViews()
                .ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(part));

            services.TryAddSingleton<IGreetingService, GreetingService>();

            if (configuration.GetValue<bool>("Features:WeatherForecasting:EnableWeatherForecast"))
            {
                services.TryAddSingleton<IWeatherForecaster, WeatherForecaster>();
            }
            else
            {
                services.TryAddSingleton<IWeatherForecaster, DisabledWeatherForecaster>();
            }
        }
    }
}
