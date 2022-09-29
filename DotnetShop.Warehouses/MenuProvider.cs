using DotnetShop.Shells.Providers.MenuProvider;

namespace DotnetShop.Warehouses
{
    internal class MenuProvider : IMenuProvider
    {
        public IEnumerable<MenuItem> GetMenuItems()
        {
            return new List<MenuItem>
            {
                new MenuItem { AspPage = "WarehouseList", MenuText = "Warehouses" },
            };
        }
    }
}
