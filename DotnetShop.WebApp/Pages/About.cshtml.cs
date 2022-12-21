using DotnetShop.WebApp.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotnetShop.WebApp.Pages
{
    [TypeFilter(typeof(OutageAuthorizationFilter))]
    public class AboutModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
