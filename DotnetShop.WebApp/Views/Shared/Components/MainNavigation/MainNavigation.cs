using DotnetShop.Infrastructure.MenuProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace DotnetShop.WebApp.Views.Shared.Components.MainNavigation
{
    public class MainNavigation : ViewComponent
    {
        private readonly IMemoryCache _memoryCache;
        private const string CacheKey = "MainNavigation";

        public MainNavigation(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IViewComponentResult Invoke()
        {
            if (!_memoryCache.TryGetValue(CacheKey, out List<MainNavigationItemModel> mainNavigationItems))
            {
                mainNavigationItems = new List<MainNavigationItemModel>();

                var allMenuProviders = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(s => s.GetTypes()).Where(p => typeof(IMenuProvider<MainMenuItem>).IsAssignableFrom(p) && p.IsClass);

                foreach (var item in allMenuProviders)
                {
                    var menuProvider = Activator.CreateInstance(item) as IMenuProvider<MainMenuItem>;
                    if (menuProvider != null && menuProvider.GetMenuItems() != null && menuProvider.GetMenuItems().Any())
                    {
                        var menuItems = menuProvider.GetMenuItems();

                        mainNavigationItems.AddRange(menuItems.Select(x => new MainNavigationItemModel
                        {
                            AspPage = x.AspPage,
                            MenuItemText = x.MenuItemText,
                            Order = x.Order
                        }));
                    }
                }

                mainNavigationItems = mainNavigationItems.OrderBy(x => x.Order).ToList();
                _memoryCache.Set(CacheKey, mainNavigationItems);
            }

            var model = new MainNavigationViewModel
            {
                MainNavigationItems = mainNavigationItems
            };

            return View(model);
        }
    }
}
