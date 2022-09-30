using DotnetShop.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotnetShop.WebApp.Views.Shared.Components.LoginPartial
{
    public class LoginPartial : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginPartial(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var returnUrl = $"{HttpContext.Request.Path}";
            var viewmodel = new LoginPartialViewModel
            {
                IsSignedIn = _signInManager.IsSignedIn(HttpContext.User),
                ReturnUrl = returnUrl.StartsWith("/Account") ? "/" : returnUrl,
                Username = _userManager.GetUserName(HttpContext.User)
            };

            return View(viewmodel);
        }
    }
}
