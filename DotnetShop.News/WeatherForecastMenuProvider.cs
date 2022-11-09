using DotnetShop.Infrastructure.MenuProvider;

namespace DotnetShop.WeatherForecast
{
    internal class WeatherForecastMenuProvider : IMenuProvider<MainMenuItem>
    {
        public IEnumerable<MainMenuItem> GetMenuItems()
        {
            return new List<MainMenuItem>
            {
                new MainMenuItem { AspPage = "/News", MenuItemText = "News", Order = 9 },
            };
        }
    }
}
