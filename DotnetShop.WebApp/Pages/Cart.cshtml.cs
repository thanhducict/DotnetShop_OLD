using DotnetShop.Data;
using DotnetShop.Domain;
using DotnetShop.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotnetShop.WebApp.Pages
{
    [Authorize]
    public class CartModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartService _cartService;

        public CartModel(IProductRepository ProductRepository, ICartService cartService)
        {
            _productRepository = ProductRepository;
            _cartService = cartService;
        }

        public Cart Cart { get; set; }
        public decimal CartTotal { get; set; }

        public void OnGet()
        {
            Cart = _cartService.GetCart();
            CartTotal = _cartService.GetCartTotal();
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
    }
}
