using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DotnetShop.WebApp.Filters
{
    public class OutageAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private IConfiguration _config;

        public OutageAuthorizationFilter(IConfiguration config)
        {
            _config = config;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var applicationSwitch = _config.GetSection("FeatureSwitches")
                .GetChildren().FirstOrDefault(x => x.Key == "Outage");

            if (bool.Parse(applicationSwitch.Value))
            {
                context.Result = new ViewResult() { ViewName = "Outage" };
            }
        }
    }
}
