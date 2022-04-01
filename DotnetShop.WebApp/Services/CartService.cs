using DotnetShop.Data;
using DotnetShop.Domain;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotnetShop.WebApp.Services
{
    public class CartService : ICartService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IProductRepository productRepository,
            ICustomerRepository customerRepository,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public Cart GetCart()
        {
            var customer = GetCustomer();
            var customerCart = new Cart();
            if (customer.Cart != null)
            {
                var customerCartJson = customer.Cart;
                customerCart = JsonSerializer.Deserialize<Cart>(customerCartJson, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                });
            }
            else
            {
                customerCart.CartItems = new List<CartItem>();
            }

            return customerCart;
        }

        public void AddToCart(int productId, int quantity)
        {
            var selectedProduct = _productRepository.AllProducts.FirstOrDefault(p => p.ProductId == productId);
            var cart = GetCart();

            var cartItem = cart.CartItems.SingleOrDefault(c => c.Product.ProductId == productId);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Product = selectedProduct,
                    Quantity = 1
                };

                cart.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }

            var customer = GetCustomer();
            customer.Cart = JsonSerializer.Serialize(cart, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            _customerRepository.Update(customer);
        }

        public int RemoveFromCart(Product product)
        {
            var cart = GetCart();

            var cartItems = cart.CartItems.SingleOrDefault(s => s.Product.ProductId == product.ProductId);

            var localQuantity = 0;

            if (cartItems != null)
            {
                if (cartItems.Quantity > 1)
                {
                    cartItems.Quantity--;
                    localQuantity = cartItems.Quantity;
                }
                else
                {
                    cart.CartItems.Remove(cartItems);
                }
            }

            var customer = GetCustomer();
            customer.Cart = JsonSerializer.Serialize(cart, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            }); ;
            _customerRepository.Update(customer);
            return 0;
        }

        public void ClearCart()
        {
            var customer = GetCustomer();
            customer.Cart = string.Empty;
            _customerRepository.Update(customer);
        }

        public decimal GetCartTotal()
        {
            var cart = GetCart();
            var total = cart.CartItems.Select(c => c.Product.Price * c.Quantity).Sum();
            return total;
        }

        public int GetCartLineItemsQuantity()
        {
            var cart = GetCart();

            return cart.CartItems.Count();
        }

        private Customer GetCustomer()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var customerId = _userManager.GetUserAsync(user).Result.CustomerId;
            var customer = _customerRepository.GetCustomerById(customerId);

            return customer;
        }
    }
}
