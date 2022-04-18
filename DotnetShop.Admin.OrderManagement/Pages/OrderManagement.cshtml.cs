using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotnetShop.Admin.OrderManagement.Pages
{
    [Authorize(Roles = "Admin")]
    public class OrderManagementModel : PageModel
    {
        private readonly ITagHelperComponentManager _tagHelperComponentManager;

        public OrderManagementModel(ITagHelperComponentManager tagHelperComponentManager)
        {
            _tagHelperComponentManager = tagHelperComponentManager;
        }

        public string BaseApiUrl { get; set; }

        public void OnGet()
        {
            _tagHelperComponentManager.Components.Add(new OrderManagementTagHelperComponent());

            BaseApiUrl  = $"{Request.Scheme}://{Request.Host}";
        }
    }
}
