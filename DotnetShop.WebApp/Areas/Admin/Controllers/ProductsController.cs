using DotnetShop.Data;
using DotnetShop.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotnetShop.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var model = _productRepository.GetProducts();

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        public IActionResult Create()
        {
            PopulateCategoriesDropDownList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _productRepository.CreateProduct(product);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Product updatedProduct)
        {
            _productRepository.UpdateProduct(updatedProduct);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            _productRepository.DeleteProduct(productId);

            return RedirectToAction("Index");
        }

        private void PopulateCategoriesDropDownList()
        {
            var categoriesQuery = from c in _categoryRepository.GetCategories()
                                   orderby c.CategoryName
                                   select new SelectListItem { Text = c.CategoryName, Value = c.CategoryId.ToString() };
            
            ViewBag.Categories = categoriesQuery;
        }
    }
}
