using DotnetShop.Infrastructure.MenuProvider;

namespace DotnetShop.WebApp.Pages
{
    public class MainMenuProvider : IMainMenuProvider
    {
        public IEnumerable<MainMenuItem> GetMenuItems()
        {
            return new List<MainMenuItem>
            {
                new MainMenuItem { AspPage = "/Contact", MenuItemText = "Contact", Order = 10 },
            };
        }
    }
}
