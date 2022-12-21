using DotnetShop.Data.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DotnetShop.WebApp.ModelBinding
{
    public class CSVModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(ApplicationUser))
            {
                return new CSVModelBinder();
            }

            return null;
        }
    }
}
