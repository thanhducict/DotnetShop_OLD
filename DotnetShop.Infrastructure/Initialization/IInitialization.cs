using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetShop.Infrastructure.Initialization
{
    public interface IInitialization
    {
        void ConfigureContainer(IServiceCollection services, IConfiguration configuration);
    }
}
