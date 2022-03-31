using DotnetShop.WebApp.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DotnetShop.WebApp.ViewComponents
{
    public class CartSummary : ViewComponent
    {
        private readonly Cart _cart;
        private readonly IHttpContextAccessor _accessor;

        public CartSummary(Cart cart, IHttpContextAccessor accessor)
        {
            _cart = cart;
            _accessor = accessor;
        }

        public IViewComponentResult Invoke()
        {
            var items = _cart.GetCartItems();
            _cart.CartItems = items;
            var cartViewModel = new CartSummaryViewModel
            {
                Cart = _cart,
                CartTotal = _cart.GetCartTotal()

            };

            return View(cartViewModel);
        }
    }
}
