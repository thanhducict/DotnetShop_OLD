using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotnetShop.WebClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ITagHelperComponentManager _tagHelperComponentManager;

        public IndexModel(ITagHelperComponentManager tagHelperComponentManager)
        {
            _tagHelperComponentManager = tagHelperComponentManager;
        }

        public string BaseApiUrl { get; set; } = string.Empty;

        public void OnGet()
        {
            _tagHelperComponentManager.Components.Add(new WebClientTagHelperComponent());

            BaseApiUrl = $"https://localhost:7100";
        }
    }
}