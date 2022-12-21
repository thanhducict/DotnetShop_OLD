using DotnetShop.Data;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountsController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.Include(u => u.Customer);

            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser user)
        {
            if (user.UserName != null && await _userManager.FindByNameAsync(user.UserName) == null)
            {
                var result = await _userManager.CreateAsync(user, "Cvbnm123@");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Constants.Roles.AdminRole);
                }
            }

            return RedirectToAction("Index", "Accounts");
        }
    }
}
