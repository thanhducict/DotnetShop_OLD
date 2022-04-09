using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotnetShop.WebApp.Pages
{
    [Authorize(Roles = "Customer")]
    public class CheckoutCompleteModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
