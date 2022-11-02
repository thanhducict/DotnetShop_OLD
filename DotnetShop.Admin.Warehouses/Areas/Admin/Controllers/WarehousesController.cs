using DotnetShop.Admin.Warehouses.Areas.Admin.Models;
using DotnetShop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetShop.Admin.Warehouses.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WarehousesController : Controller
    {
        private readonly DotnetShopDbContext _context;

        public WarehousesController(DotnetShopDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = _context.Warehouses.Select(w => new WarehouseModel
            {
                WarehouseId = w.WarehouseId,
                WarehouseName = w.WarehouseName,
                Address = w.Address,
            });

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var categories = _context.Categories.ToList();
            var warehouse = _context.Warehouses.Include(w => w.Products).Where(w => w.WarehouseId == id).FirstOrDefault();
            var productsInWarehouse = _context.ProductWarehouses.Where(w => w.WarehouseId == id);

            if (warehouse == null)
                return NotFound();

            var model = new WarehouseDetailModel
            {
                WarehouseId = warehouse.WarehouseId,
                WarehouseName = warehouse.WarehouseName,
                Address = warehouse.Address,
                Latitude = warehouse.Latitude,
                Longitude = warehouse.Longitude,
                ProductModels = warehouse.Products.Select(w => new ProductModel
                {
                    ProductId = w.ProductId,
                    ProductName = w.Name,
                    CategoryName = categories.Where(c => c.CategoryId == w.CategoryId).FirstOrDefault().CategoryName,
                    Quantity = productsInWarehouse.Where(p => p.ProductId == w.ProductId).FirstOrDefault().ProductQuantity,
                }).ToList()
            };

            return View(model);
        }

        public IActionResult Map()
        {
            PopulateProductDropdown();

            return View();
        }

        public void PopulateProductDropdown()
        {
            var products = _context.Products.OrderBy(p => p.Name).Select(p => new
            {
                Text = p.Name,
                Value = p.ProductId
            });

            ViewBag.Products = products;
        }

        public JsonResult GetWarehouseMapInfo([FromQuery] int productId = 0)
        {
            var product = _context.Products.Include(p => p.Warehouses).Where(p => p.ProductId == productId).SingleOrDefault();
            if (product == null)
            {
                return new JsonResult(new WarehouseLocationModel());
            }

            var warehouseOfProduct = _context.ProductWarehouses.Where(p => p.ProductId == productId).Select(x => x.WarehouseId);

            var model = new WarehouseLocationModel
            {
                ProductId = product.ProductId,
                ProductName = product.Name,
                WarehouseModels = product.Warehouses.Select(w => new WarehouseModel
                {
                    WarehouseId = w.WarehouseId,
                    WarehouseName = w.WarehouseName,
                    Address = w.Address,
                    Latitude = w.Latitude,
                    Longitude = w.Longitude
                }).ToList(),
            };

            return new JsonResult(model);
        }
    }
}
