using DotnetShop.Shells.Providers.MenuProvider;
using DotnetShop.WebApp.Areas.Admin.Views.Shared.Components.NavigationPartial;
using Microsoft.AspNetCore.Mvc;

namespace DotnetShop.WebApp.Pages.Shared.Components.LoginPartial
{
    public class NavigationPartial : ViewComponent
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

            var model = new NavigationViewModel
            {
                NavigationItems = navigationItems
            };

            return View(model);
        }
    }
}
