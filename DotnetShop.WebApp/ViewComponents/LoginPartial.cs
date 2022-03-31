using DotnetShop.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotnetShop.WebApp.ViewComponents
{
    public class LoginPartial : ViewComponent
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public LoginPartial(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var returnUrl = $"{HttpContext.Request.Path}{HttpContext.Request.QueryString}";
            var viewmodel = new LoginPartialViewModel
            {
                IsSignedIn = _signInManager.IsSignedIn(HttpContext.User),
                ReturnUrl = returnUrl,
                Username = _userManager.GetUserName(HttpContext.User)
            };

            return View(viewmodel);
        }
    }
}
