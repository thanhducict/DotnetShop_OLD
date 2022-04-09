using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace DotnetShop.Admin.OrderManagement
{
    public class OrderManagementConfigureOptions : IPostConfigureOptions<StaticFileOptions>
    {
        private readonly IHostingEnvironment _environment;

        public OrderManagementConfigureOptions(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public void PostConfigure(string name, StaticFileOptions options)
        {
            options.ContentTypeProvider = options.ContentTypeProvider ?? new FileExtensionContentTypeProvider();

            if (options.FileProvider == null && _environment.WebRootFileProvider == null)
            {
                throw new InvalidOperationException("Missing FileProvider.");
            }

            options.FileProvider = options.FileProvider ?? _environment.WebRootFileProvider;

            var filesProvider = new ManifestEmbeddedFileProvider(GetType().Assembly);
            options.FileProvider = new CompositeFileProvider(options.FileProvider, filesProvider);
        }
    }
}
