using DotnetShop.Infrastructure.Initialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DotnetShop.Admin.Warehouses
{
    public class WarehousesInitialization : IInitialization
    {
        public void ConfigureContainer(WebApplicationBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var part = new AssemblyPart(assembly);
            builder.Services.AddControllersWithViews()
                .ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(part));

            builder.Services.AddTransient<ITagHelperComponent, WarehousesTagHelperComponent>();
        }
    }
}
