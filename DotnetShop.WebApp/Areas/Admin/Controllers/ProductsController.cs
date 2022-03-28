using DotnetShop.Data;
using ProductSupplier.WebApp.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProductSupplier.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductsController(IProductRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult List()
        {
            var productListViewModel = new ProductsListViewModel();
            productListViewModel.Products = _productRepository.AllProducts;

            productListViewModel.CurrentCategory = "Beverage";
            return View(productListViewModel);
        }

        public IActionResult Details(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}

