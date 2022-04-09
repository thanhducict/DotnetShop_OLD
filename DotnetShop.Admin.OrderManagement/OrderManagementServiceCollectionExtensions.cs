using Microsoft.Extensions.DependencyInjection;

namespace DotnetShop.Admin.OrderManagement
{
    public static class OrderManagementServiceCollectionExtensions
    {
        public static void AddOrderManagementAssets(this IServiceCollection services)
        {
            services.ConfigureOptions(typeof(OrderManagementConfigureOptions));
        }
    }
}
