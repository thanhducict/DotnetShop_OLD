using DotnetShop.Infrastructure.MenuProvider;

namespace DotnetShop.WeatherForecast
{
    internal class WarehouseMenuProvider : IMainMenuProvider
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
