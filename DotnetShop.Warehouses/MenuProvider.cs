
using DotnetShop.Infrastructure.MenuProvider;

namespace DotnetShop.Warehouses
{
    internal class MenuProvider : IMenuProvider
    {
        public IEnumerable<MenuItem> GetMenuItems()
        {
            return new List<MenuItem>
            {
                new MenuItem { AspArea = "Admin", AspController = "Warehouses", AspAction = "Index", MenuText = "Warehouses" },
            };
        }
    }
}
