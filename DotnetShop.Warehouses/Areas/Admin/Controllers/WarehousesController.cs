using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetShop.Warehouses.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WarehousesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
