using DotnetShop.Infrastructure.MenuProvider;

namespace DotnetShop.Admin.Warehouses
{
    internal class BookssMenuProvider : IMenuProvider<MainMenuItem>
    {
        public IEnumerable<MainMenuItem> GetMenuItems()
        {
            return new List<MainMenuItem>
            {
                new MainMenuItem { AspPage = "/Books", MenuItemText = "Books", Order = 2 },
            };
        }
    }
}
