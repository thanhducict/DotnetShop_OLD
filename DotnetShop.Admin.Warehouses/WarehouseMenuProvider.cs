using DotnetShop.Infrastructure.MenuProvider;

namespace DotnetShop.Admin.Warehouses
{
    internal class WarehouseMenuProvider : IAdminMenuProvider
    {
        public IEnumerable<AdminMenuItem> GetMenuItems()
        {
            return new List<AdminMenuItem>
            {
                new AdminMenuItem { AspArea = "Admin", AspController = "Warehouses", AspAction = "Index", MenuItemText = "Warehouses", Order = 5 },
            };
        }
    }
}
