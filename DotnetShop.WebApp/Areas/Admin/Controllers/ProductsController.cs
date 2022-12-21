using DotnetShop.Data.Domain;
using DotnetShop.Data.Repositories;
using DotnetShop.WebApp.Areas.Admin.Helpers;
using DotnetShop.WebApp.Areas.Admin.Models;
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
        private readonly IHelpers _helpers;

        public ProductsController(IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IHelpers helpers)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _helpers = helpers;
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

            return View(new ProductModel());
        }

        [HttpPost]
        public IActionResult Create(ProductModel model)
        {
            var imageUrl = _helpers.UploadFile(model.Image, Helpers.Helpers.UploadFileType.ProductImage, $"{model.Name} HD");
            var imageThumbnailUrl = _helpers.UploadFile(model.Image, Helpers.Helpers.UploadFileType.ProductImage, $"{model.Name}");

            var product = new Product
            {
                ProductId = model.ProductId,
                Name = model.Name,
                ShortDescription = model.ShortDescription,
                LongDescription = model.LongDescription,
                Price = model.Price,
                ImageUrl = imageUrl,
                ImageThumbnailUrl = imageThumbnailUrl,
                IsProductOfTheWeek = model.IsProductOfTheWeek,
                InStock = model.InStock,
                CategoryId = model.CategoryId
            };

            _productRepository.CreateProduct(product);

            return RedirectToAction("Index", "Products");
        }

        public IActionResult Edit(int id)
        {
            PopulateCategoriesDropDownList();

            var product = _productRepository.GetProductById(id);
            if (product == null)
                return NotFound();

            var model = new ProductModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                ShortDescription = product.ShortDescription,
                LongDescription = product.LongDescription,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                ImageThumbnailUrl = product.ImageThumbnailUrl,
                IsProductOfTheWeek = product.IsProductOfTheWeek,
                InStock = product.InStock,
                CategoryId = product.CategoryId
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ProductModel model)
        {
            var imageUrl = _helpers.UploadFile(model.Image, Helpers.Helpers.UploadFileType.ProductImage, $"{model.Name} HD");
            var imageThumbnailUrl = _helpers.UploadFile(model.ImageThumbnail, Helpers.Helpers.UploadFileType.ProductImage, $"{model.Name}");

            var updatedProduct = new Product
            {
                ProductId = model.ProductId,
                Name = model.Name,
                ShortDescription = model.ShortDescription,
                LongDescription = model.LongDescription,
                Price = model.Price,
                IsProductOfTheWeek = model.IsProductOfTheWeek,
                InStock = model.InStock,
                CategoryId = model.CategoryId
            };

            if (!string.IsNullOrEmpty(imageUrl))
            {
                updatedProduct.ImageUrl = imageUrl;
            }
            else if (!string.IsNullOrEmpty(model.ImageUrl))
            {
                updatedProduct.ImageUrl = model.ImageUrl;
            }
            else
            {
                updatedProduct.ImageUrl = string.Empty;
            }

            if (!string.IsNullOrEmpty(imageThumbnailUrl))
            {
                updatedProduct.ImageThumbnailUrl = imageThumbnailUrl;
            }
            else if (!string.IsNullOrEmpty(model.ImageThumbnailUrl))
            {
                updatedProduct.ImageThumbnailUrl = model.ImageThumbnailUrl;
            }
            else
            {
                updatedProduct.ImageThumbnailUrl = string.Empty;
            }

            _productRepository.UpdateProduct(updatedProduct);

            return RedirectToAction("Index", "Product");
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
