using DotnetShop.Infrastructure.Initialization;
using DotnetShop.News.Services;
using DotnetShop.WeatherForecast.Configurations;
using DotnetShop.WeatherForecast.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace DotnetShop.WeatherForecast
{
    internal class WeatherForecastInitialization : IInitialization
    {
        public void ConfigureContainer(WebApplicationBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var part = new AssemblyPart(assembly);
            builder.Services.AddControllersWithViews()
                .ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(part));

            if (builder.Environment.IsDevelopment())
            {
                var projectRootFolder = Path.Combine(builder.Environment.ContentRootPath, @"..\");

                builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile(path: Path.Combine(projectRootFolder, @"DotnetShop.News\weathersettings.json"),
                        optional: false,
                        reloadOnChange: true);
                });
            }

            builder.Services.Configure<HomePageConfiguration>(builder.Configuration.GetSection("Features:HomePage"));
            builder.Services.Configure<GreetingConfiguration>(builder.Configuration.GetSection("Features:Greeting"));

            builder.Services.Configure<ExternalServicesConfig>(ExternalServicesConfig.WeatherApi,
                builder.Configuration.GetSection("NewsProvider:WeatherApi"));
            builder.Services.Configure<ExternalServicesConfig>(ExternalServicesConfig.SportsNewsApi,
                builder.Configuration.GetSection("NewsProvider:SportsNewsApi"));

            if (builder.Configuration.GetValue<bool>("Features:WeatherForecasting:EnableWeatherForecast"))
            {
                builder.Services.TryAddSingleton<IWeatherForecaster, WeatherForecaster>();
            }
            else
            {
                builder.Services.TryAddSingleton<IWeatherForecaster, DisabledWeatherForecaster>();
            }

            builder.Services.TryAddSingleton<IGreetingService, GreetingService>();
            builder.Services.TryAddSingleton<ISportsNewsService, SportsNewsService>();
        }
    }
}
