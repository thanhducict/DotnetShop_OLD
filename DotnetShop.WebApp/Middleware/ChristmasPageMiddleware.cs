namespace DotnetShop.WebApp.Middleware
{
    public class ChristmasPageMiddleware
    {
        private readonly RequestDelegate _next;

        public ChristmasPageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (DateTime.Now.Month == 12 && DateTime.Now.Day <= 25 && DateTime.Now.Day >= 10)
            {
                var christmas = httpContext.Session.GetString("christmas");
                if (string.IsNullOrWhiteSpace(christmas))
                {
                    httpContext.Session.SetString("christmas", "christmas");
                    httpContext.Response.Redirect("/ChristmasPage");
                    return;
                }
            }

            await _next(httpContext);
        }
    }

    public static class ChrismasPageMiddlewareExtensions
    {
        public static IApplicationBuilder UseChristmasPage(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ChristmasPageMiddleware>();
        }
    }
}
