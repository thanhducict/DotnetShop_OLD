using DotnetShop.Data.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetShop.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> _appUserManager;

        public AccountsController(UserManager<ApplicationUser> appUserManager)
        {
            _appUserManager = appUserManager;
        }

        public IActionResult Index()
        {
            var users = _appUserManager.Users.Include(u => u.Customer);

            return View(users);
        }
    }
}
