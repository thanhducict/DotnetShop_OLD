using DotnetShop.Data;
using DotnetShop.Domain;
using DotnetShop.WebApp.Models;
using DotnetShop.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotnetShop.WebApp.Pages
{
    [Authorize]
    public class CartModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartService _cartService;
        private readonly ICheckoutService _checkoutService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartModel(
            IProductRepository productRepository,
            ICartService cartService,
            ICheckoutService checkoutService,
            UserManager<ApplicationUser> userManager)
        {
            _productRepository = productRepository;
            _cartService = cartService;
            _checkoutService = checkoutService;
            _userManager = userManager;
        }

        public CartViewModel CartViewModel { get; set; }

        public void OnGet()
        {
            CartViewModel = GetCartViewModel();
        }

        public IActionResult OnGetAddToCart(int productId)
        {
            _cartService.AddToCart(productId, 1);

            return RedirectToPage();
        }

        public IActionResult OnPostRemoveFromCart(int productId)
        {
            //var selectedProduct = _productRepository.AllProducts.FirstOrDefault(p => p.ProductId == productId);

            //if (selectedProduct != null)
            //{
            //    _cart.RemoveFromCart(selectedProduct);
            //}

            return RedirectToPage();
        }

        public IActionResult OnPostCheckout(){
            var cartViewModel = GetCartViewModel();

            _checkoutService.Checkout(
                _userManager.GetUserAsync(HttpContext.User).Result.CustomerId,
                cartViewModel);
            _cartService.ClearCart();
            
            return LocalRedirect("/CheckoutComplete"); 
        }

        private CartViewModel GetCartViewModel()
        {
            var products = _productRepository.AllProducts;
            var cartItemViewModels = _cartService.GetCart().CartLineItems
                .Select(c =>
                {
                    var model = new CartLineItemViewModel
                    {
                        ProductId = c.ProductId,
                        ProductName = products.Where(p => p.ProductId == c.ProductId).SingleOrDefault().Name,
                        ProductPrice = products.Where(p => p.ProductId == c.ProductId).SingleOrDefault().Price,
                        CartLineItemQuantity = c.Quantity,
                    };
                    model.CartLineItemTotal = model.CartLineItemQuantity * model.ProductPrice;

                    return model;
                })
                .ToList();

            return new CartViewModel
            {
                CartLineItemViewModels = cartItemViewModels,
                CartTotal = _cartService.GetCartTotal()
            };
        }
    }
}
