using DotnetShop.Data;
using DotnetShop.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotnetShop.WebApp.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public Product Product { get; set; }

        public DetailsModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult OnGet(int id)
        {
            var result = _productRepository.GetProductById(id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                Product = result;
            }

            return Page();
        }
    }
}
