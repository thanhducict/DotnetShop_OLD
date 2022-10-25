using DotnetShop.Infrastructure.Initialization;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DotnetShop.Admin.Warehouses
{
    public class WarehouseInitialization : IInitialization
    {
        public void ConfigureContainer(IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();
            // This creates an AssemblyPart, but does not create any related parts for items such as views.
            var part = new AssemblyPart(assembly);
            services.AddControllersWithViews()
                .ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(part));

            services.AddTransient<ITagHelperComponent, WarehouseTagHelperComponent>();
        }
    }
}
