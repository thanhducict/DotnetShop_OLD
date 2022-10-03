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
            var navigationItems = new List<NavigationItemModel>();

            if (!_memoryCache.TryGetValue(CacheKey, out IEnumerable<Type> allMenuProviders))
            {
                allMenuProviders = AppDomain.CurrentDomain.GetAssemblies()
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

                _memoryCache.Set(CacheKey, allMenuProviders);
            }

            var model = new AdminNavigationViewModel
            {
                NavigationItems = navigationItems
            };

            return View(model);
        }
    }
}
