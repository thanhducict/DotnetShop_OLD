using Microsoft.AspNetCore.Mvc;

namespace DotnetShop.WebApp.Middleware
{
    public class FeatureSwitchMiddleware
    {
        private readonly RequestDelegate _next;

        public FeatureSwitchMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IConfiguration config)
        {
            var endpoint = httpContext.GetEndpoint()?.Metadata.GetMetadata<RouteAttribute>();

            if (endpoint != null)
            {
                var featureSwitch = config.GetSection("FeatureSwitches")
                    .GetChildren().FirstOrDefault(x => x.Key == endpoint.Name);

                if (featureSwitch != null && !bool.Parse(featureSwitch.Value))
                {
                    httpContext.SetEndpoint(new Endpoint((context) =>
                    {
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        return Task.CompletedTask;
                    },
                    EndpointMetadataCollection.Empty, "Feature Not Found"));
                }
            }

            await _next(httpContext);
        }
    }

    public static class FeatureSwitchMiddlewareExtensions
    {
        public static IApplicationBuilder UseFeatureSwitch(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FeatureSwitchMiddleware>();
        }
    }
}
