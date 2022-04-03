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
            var customerCart = new Cart()
            {
                CartLineItems = new List<CartLineItem>()
            };
            if (!string.IsNullOrEmpty(customer.Cart))
            {
                customerCart = JsonSerializer.Deserialize<Cart>(customer.Cart);
                if (customerCart.CartLineItems == null)
                {
                    customerCart.CartLineItems = new List<CartLineItem>();
                }
            }

            return customerCart;
        }

        public void AddToCart(int productId, int quantity)
        {
            var selectedProduct = _productRepository.AllProducts
                .FirstOrDefault(p => p.ProductId == productId);
            var cart = GetCart();

            var cartItem = cart.CartLineItems.SingleOrDefault(c => c.ProductId == productId);
            if (cartItem == null)
            {
                cartItem = new CartLineItem
                {
                    ProductId = selectedProduct.ProductId,
                    Quantity = 1
                };

                cart.CartLineItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }

            var customer = GetCustomer();
            customer.Cart = JsonSerializer.Serialize(cart);
            _customerRepository.Update(customer);
        }

        public int RemoveFromCart(Product product)
        {
            var cart = GetCart();

            var cartLineItems = cart.CartLineItems.SingleOrDefault(s => s.ProductId == product.ProductId);

            var localQuantity = 0;

            if (cartLineItems != null)
            {
                if (cartLineItems.Quantity > 1)
                {
                    cartLineItems.Quantity--;
                    localQuantity = cartLineItems.Quantity;
                }
                else
                {
                    cart.CartLineItems.Remove(cartLineItems);
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
            var products = _productRepository.AllProducts;
            var total = GetCart().CartLineItems
                .Select(c =>
                    products.Where(p => p.ProductId == c.ProductId).SingleOrDefault().Price * c.Quantity
                )
                .Sum();
            return total;
        }

        public int GetCartLineItemsQuantity()
        {
            var cart = GetCart();

            return cart.CartLineItems.Count();
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
