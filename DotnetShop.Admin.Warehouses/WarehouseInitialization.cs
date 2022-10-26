using DotnetShop.Infrastructure.Initialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DotnetShop.Admin.Warehouses
{
    public class WarehouseInitialization : IInitialization
    {
        public void ConfigureContainer(WebApplicationBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            // This creates an AssemblyPart, but does not create any related parts for items such as views.
            var part = new AssemblyPart(assembly);
            builder.Services.AddControllersWithViews()
                .ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(part));

            builder.Services.AddTransient<ITagHelperComponent, WarehouseTagHelperComponent>();
        }
    }
}
