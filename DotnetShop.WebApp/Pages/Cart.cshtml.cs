using DotnetShop.Data;
using DotnetShop.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotnetShop.WebApp.Pages
{
    [Authorize]
    public class CartModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly Cart _cart;

        public CartModel(IProductRepository ProductRepository, Cart cart)
        {
            _productRepository = ProductRepository;
            _cart = cart;
        }

        public Cart Cart { get; set; }
        public decimal CartTotal { get; set; }

        public void OnGet()
        {
            var items = _cart.GetCartItems();
            _cart.CartItems = items;

            Cart = _cart;
            CartTotal = _cart.GetCartTotal();
        }

        public IActionResult OnGetAddToCart(int productId)
        {
            var selectedProduct = _productRepository.AllProducts.FirstOrDefault(p => p.ProductId == productId);

            if (selectedProduct != null)
            {
                _cart.AddToCart(selectedProduct, 1);
            }

            return RedirectToPage();
        }

        public IActionResult OnPostRemoveFromCart(int productId)
        {
            var selectedProduct = _productRepository.AllProducts.FirstOrDefault(p => p.ProductId == productId);

            if (selectedProduct != null)
            {
                _cart.RemoveFromCart(selectedProduct);
            }

            return RedirectToPage();
        }
    }
}
