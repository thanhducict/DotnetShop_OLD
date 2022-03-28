using DotnetShop.Data;
using DotnetShop.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotnetShop.WebApp.Pages
{
    public class ShoppingCartModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartModel(IProductRepository ProductRepository, ShoppingCart shoppingCart)
        {
            _productRepository = ProductRepository;
            _shoppingCart = shoppingCart;
        }

        public ShoppingCart ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }

        public void OnGet()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            ShoppingCart = _shoppingCart;
            ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal();
        }

        public IActionResult OnGetAddToShoppingCart(int productId)
        {
            var selectedProduct = _productRepository.AllProducts.FirstOrDefault(p => p.ProductId == productId);

            if (selectedProduct != null)
            {
                _shoppingCart.AddToCart(selectedProduct, 1);
            }

            return RedirectToPage();
        }

        public IActionResult OnPostRemoveFromShoppingCart(int productId)
        {
            var selectedProduct = _productRepository.AllProducts.FirstOrDefault(p => p.ProductId == productId);

            if (selectedProduct != null)
            {
                _shoppingCart.RemoveFromCart(selectedProduct);
            }

            return RedirectToPage();
        }
    }
}
