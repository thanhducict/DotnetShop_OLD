using DotnetShop.Data;
using DotnetShop.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
                customerCart = JsonSerializer.Deserialize<Cart>(customer.Cart, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                });
                if (customerCart.CartLineItems == null)
                {
                    customerCart.CartLineItems = new List<CartLineItem>();
                }
            }

            return customerCart;
        }

        public void AddToCart(int productId, int quantity)
        {
            var selectedProduct = _productRepository.GetProducts()
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

        public void RemoveFromCart(int productId)
        {
            var cart = GetCart();

            var cartLineItems = cart.CartLineItems.SingleOrDefault(s => s.ProductId == productId);

            if (cartLineItems != null)
            {
                if (cartLineItems.Quantity > 1)
                {
                    cartLineItems.Quantity--;
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
            });

            _customerRepository.Update(customer);
        }

        public void ClearCart()
        {
            var customer = GetCustomer();
            customer.Cart = string.Empty;
            _customerRepository.Update(customer);
        }

        public decimal GetCartTotal()
        {
            var products = _productRepository.GetProducts();
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
            var customer = _userManager.Users
                .Include(u => u.Customer)
                .FirstOrDefault(u => u.UserName.ToLower() == user.Identity.Name.ToLower())
                .Customer;

            return customer;
        }
    }
}
