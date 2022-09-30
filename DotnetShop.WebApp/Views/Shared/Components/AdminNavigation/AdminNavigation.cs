using DotnetShop.Infrastructure.MenuProvider;
using Microsoft.AspNetCore.Mvc;

namespace DotnetShop.WebApp.Views.Shared.Components.AdminNavigation
{
    public class AdminNavigation : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var navigationItems = new List<NavigationItemModel>();

            var allMenuProviders = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes()).Where(p => typeof(IMenuProvider).IsAssignableFrom(p) && p.IsClass);

            foreach (var item in allMenuProviders)
            {
                var menuProvider = Activator.CreateInstance(item) as IMenuProvider;
                if (menuProvider != null && menuProvider.GetMenuItems() != null && menuProvider.GetMenuItems().Any())
                {
                    var menuItems = menuProvider.GetMenuItems();

                    navigationItems.AddRange(menuItems.Select(x => new NavigationItemModel
                    {
                        AspArea = x.AspArea,
                        AspController = x.AspController,
                        AspAction = x.AspAction,
                        AspPage = x.AspPage,
                        MenuText = x.MenuText,
                    }));
                }
            }

            var model = new AdminNavigationViewModel
            {
                NavigationItems = navigationItems
            };

            return View(model);
        }
    }
}
