using DotnetShop.WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetShop.WebApp.Views.Shared.Components.CartSummary
{
    public class CartSummary : ViewComponent
    {
        private readonly ICartService _cartService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartSummary(ICartService cartService, IHttpContextAccessor httpContextAccessor)
        {
            _cartService = cartService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated &&
                !_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
            {
                int quantity = _cartService.GetCartLineItemsQuantity();
                return View(quantity);
            }
            else
            {
                return View("NoCart");
            }
        }
    }
}
