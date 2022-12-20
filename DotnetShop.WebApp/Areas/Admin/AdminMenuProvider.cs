using DotnetShop.Infrastructure.MenuProvider;

namespace DotnetShop.WebApp.Areas.Admin
{
    public class AdminMenuProvider : IMenuProvider<AdminMenuItem>
    {
        public IEnumerable<AdminMenuItem> GetMenuItems()
        {
            return new List<AdminMenuItem>
            {
                new AdminMenuItem { AspArea = "Admin", AspController = "Products", AspAction = "Index", MenuItemText = "Products", Order = 0 },
                new AdminMenuItem { AspArea = "Admin", AspController = "Accounts", AspAction = "Index", MenuItemText = "Accounts", Order = 9 },
                new AdminMenuItem { AspArea = "Admin", AspController = "Roles", AspAction = "Index", MenuItemText = "Roles", Order = 10 },
            };
        }
    }
}
