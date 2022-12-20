using DotnetShop.Infrastructure.MenuProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace DotnetShop.WebApp.Views.Shared.Components.AdminNavigation
{
    public class AdminNavigation : ViewComponent
    {
        private readonly IMemoryCache _memoryCache;
        private const string CacheKey = "AdminNavigation";

        public AdminNavigation(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IViewComponentResult Invoke()
        {
            if (!_memoryCache.TryGetValue(CacheKey, out List<AdminNavigationItemModel> adminNavigationItems))
            {
                adminNavigationItems = new List<AdminNavigationItemModel>();

                var allMenuProviders = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(s => s.GetTypes()).Where(p => typeof(IMenuProvider<AdminMenuItem>).IsAssignableFrom(p) && p.IsClass);

                foreach (var item in allMenuProviders)
                {
                    var menuProvider = Activator.CreateInstance(item) as IMenuProvider<AdminMenuItem>;
                    if (menuProvider != null && menuProvider.GetMenuItems() != null && menuProvider.GetMenuItems().Any())
                    {
                        var menuItems = menuProvider.GetMenuItems();

                        adminNavigationItems.AddRange(menuItems.Select(x => new AdminNavigationItemModel
                        {
                            AspArea = x.AspArea,
                            AspController = x.AspController,
                            AspAction = x.AspAction,
                            MenuItemText = x.MenuItemText,
                            Order = x.Order,
                        }));
                    }
                }

                adminNavigationItems = adminNavigationItems.OrderBy(x => x.Order).ToList();
                _memoryCache.Set(CacheKey, adminNavigationItems);
            }

            var model = new AdminNavigationViewModel
            {
                AdminNavigationItems = adminNavigationItems
            };

            return View(model);
        }
    }
}
