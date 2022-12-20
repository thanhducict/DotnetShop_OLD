using Microsoft.AspNetCore.Builder;

namespace DotnetShop.Infrastructure.Initialization
{
    public interface IInitialization
    {
        void ConfigureContainer(WebApplicationBuilder builder);
    }
}
