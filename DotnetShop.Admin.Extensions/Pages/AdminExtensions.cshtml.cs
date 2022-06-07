using DotnetShop.Admin.AdminExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotnetShop.Admin.Extensions.Pages
{
    [Authorize(Roles = "Admin")]
    public class AdminExtensionsModel : PageModel
    {
        private readonly ITagHelperComponentManager _tagHelperComponentManager;

        public AdminExtensionsModel(ITagHelperComponentManager tagHelperComponentManager)
        {
            _tagHelperComponentManager = tagHelperComponentManager;
        }

        public string BaseApiUrl { get; set; }

        public void OnGet()
        {
            _tagHelperComponentManager.Components.Add(new AdminExtensionsTagHelperComponent());

            BaseApiUrl = $"{Request.Scheme}://{Request.Host}";
        }
    }
}
