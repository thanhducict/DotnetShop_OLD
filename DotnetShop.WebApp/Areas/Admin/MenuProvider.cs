using DotnetShop.Infrastructure.MenuProvider;

namespace DotnetShop.WebApp.Areas.Admin
{
    public class MenuProvider : IMenuProvider
    {
        public IEnumerable<MenuItem> GetMenuItems()
        {
            return new List<MenuItem>
            {
                new MenuItem { AspArea = "Admin", AspController = "Products", AspAction = "Index", MenuText = "Products" },
                new MenuItem { AspArea = "Admin", AspController = "Accounts", AspAction = "Index", MenuText = "Accounts" },
                new MenuItem { AspArea = "Admin", AspController = "Roles", AspAction = "Index", MenuText = "Roles" },
            };
        }
    }
}
