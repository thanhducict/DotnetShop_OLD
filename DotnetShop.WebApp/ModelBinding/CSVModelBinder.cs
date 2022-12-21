using DotnetShop.Data;
using DotnetShop.Data.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DotnetShop.WebApp.ModelBinding
{
    public class CSVModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var username = bindingContext.ValueProvider.GetValue("Username").ToString();
            var email = bindingContext.ValueProvider.GetValue("Email").ToString();
            var level = bindingContext.ValueProvider.GetValue("Level").ToString();

            var newUser = new ApplicationUser()
            {
                UserName = $"{username}{new Random().Next(100)}",
                Email = !string.IsNullOrEmpty(email) ? email : "anonymous@email.com",
                Level = !string.IsNullOrEmpty(level) ? level : Constants.Levels.NotEmployee
            };

            bindingContext.Result = ModelBindingResult.Success(newUser);
            return Task.CompletedTask;
        }
    }
}
