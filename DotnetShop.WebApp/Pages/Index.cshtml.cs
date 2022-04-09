using DotnetShop.Data;
using DotnetShop.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotnetShop.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public IEnumerable<Product> ProductsOfTheWeek { get; set; }

        public IndexModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void OnGet()
        {
            ProductsOfTheWeek = _productRepository.GetProducts().Where(p => p.IsProductOfTheWeek);
        }
    }
}