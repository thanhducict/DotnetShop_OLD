using DotnetShop.WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetShop.WebApp.ViewComponents
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
            var total = 0;
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                total = _cartService.GetCartLineItemsQuantity();
            }

            return View(total);
        }
    }
}
