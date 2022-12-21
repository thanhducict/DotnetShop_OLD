using DotnetShop.Data.Domain;
using DotnetShop.Data.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotnetShop.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public string Slogan { get; set; }

        public IEnumerable<Product> ProductsOfTheWeek { get; set; }

        public IndexModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void OnGet()
        {
            ProductsOfTheWeek = _productRepository.GetProducts().Where(p => p.IsProductOfTheWeek);
        }

        public override async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context,
                                                  PageHandlerExecutionDelegate next)
        {
            Slogan = "Our weekly selection - just for you!";
            await next.Invoke();
        }
    }
}
