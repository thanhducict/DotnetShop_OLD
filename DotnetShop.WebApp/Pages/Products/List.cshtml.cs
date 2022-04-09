using DotnetShop.Data;
using DotnetShop.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotnetShop.WebApp.Pages.Products
{
    public class ListModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public IEnumerable<Product> Products { get; set; }
        public string CurrentCategory { get; set; }

        public ListModel(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public void OnGet(int categoryId)
        {
            if (categoryId > 0)
            {
                Products = _productRepository.GetProducts().Where(p => p.Category.CategoryId == categoryId)
                     .OrderBy(p => p.ProductId);
                CurrentCategory = _categoryRepository.GetCategories()
                    .FirstOrDefault(c => c.CategoryId == categoryId)?.CategoryName;
            }
            else
            {
                Products = _productRepository.GetProducts().OrderBy(p => p.ProductId);
                CurrentCategory = "All products";
            }
        }
    }
}
