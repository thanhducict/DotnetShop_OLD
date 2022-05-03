namespace DotnetShop.WebApp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUIMappedFileProviders(this IServiceCollection serviceCollection,
            IWebHostEnvironment enviroinment)
        {
            //var uiSolutionFolder = Path.Combine(enviroinment.ContentRootPath, @"..\");
            //var orderManagementFileProvider = new PhysicalFileProvider(Path.Combine(uiSolutionFolder,
            //        "DotnetShop.Admin.OrderManagement/ClientApp/dist"));

            //serviceCollection.Configure<StaticFileOptions>(options =>
            //{
            //    options.FileProvider = new CompositeFileProvider(enviroinment.ContentRootFileProvider, orderManagementFileProvider);
            //    options.RequestPath = "/OrderManagement";
            //});

            //serviceCollection.AddSingleton<IFileProvider>(new CompositeFileProvider(enviroinment.ContentRootFileProvider, orderManagementFileProvider));

            return serviceCollection;
        }
    }
}
