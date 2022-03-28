using DotnetShop.Data;
using DotnetShop.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProductSupplier.WebApp.Pages.Products
{
    public class ListModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public IEnumerable<Product> Products { get; set; }
        public string CurrentCategory { get; set; }

        public ListModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void OnGet()
        {
            Products = _productRepository.AllProducts;
            CurrentCategory = "Beverage";
        }
    }
}
