using DotnetShop.Infrastructure.MenuProvider;

namespace DotnetShop.WebApp.Pages
{
    public class MainMenuProvider : IMenuProvider<MainMenuItem>
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
