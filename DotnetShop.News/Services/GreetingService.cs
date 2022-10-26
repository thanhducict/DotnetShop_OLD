using DotnetShop.WeatherForecast.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DotnetShop.WeatherForecast.Services
{
    public class GreetingService: IGreetingService
    {
        private static readonly ThreadLocal<Random> Random = new ThreadLocal<Random>(() => new Random());
        private GreetingConfiguration _greetingConfiguration;

        public GreetingService(IWebHostEnvironment webHostEnvironment, IOptionsMonitor<GreetingConfiguration> options)
        {
            var greetingsJson = File.ReadAllText(webHostEnvironment.WebRootPath + "/greetings.json");
            var greetingsData = JsonConvert.DeserializeObject<GreetingData>(greetingsJson);

            Greetings = greetingsData.Greetings;
            LoginGreetings = greetingsData.LoginGreetings;

            _greetingConfiguration = options.CurrentValue;

            options.OnChange(config =>
            {
                _greetingConfiguration = config;
            });
        }

        public string[] Greetings { get; }

        public string[] LoginGreetings { get; }

        public string GreetingColour => _greetingConfiguration.GreetingColour;

        public string GetRandomGreeting()
        {
            return GetRandomValue(Greetings);
        }

        public string GetRandomLoginGreeting(string name)
        {
            var loginGreeting = GetRandomValue(LoginGreetings);

            return loginGreeting.Replace("{name}", name);
        }

        private string GetRandomValue(IReadOnlyList<string> greetings)
        {
            if (greetings.Count == 0)
            {
                return string.Empty;
            }

            var greetingToUse = Random.Value.Next(greetings.Count);

            return greetingToUse >= 0 ? greetings[greetingToUse] : string.Empty;
        }

        private class GreetingData
        {
            public string[] Greetings { get; set; }

            public string[] LoginGreetings { get; set; }
        }
    }
}
