using DotnetShop.Data;
using DotnetShop.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotnetShop.WebApp.Models
{
    public class Cart
    {
        private readonly DotnetShopDbContext _context;

        public string CartId { get; set; }

        public List<CartItem> CartItems { get; set; }

        private Cart(DotnetShopDbContext context)
        {
            _context = context;
        }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<DotnetShopDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new Cart(context) { CartId = cartId };
        }

        public void AddToCart(Product product, int amount)
        {
            var cartItem =
                    _context.CartItems.SingleOrDefault(
                        s => s.Product.ProductId == product.ProductId && s.CartId == CartId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    CartId = CartId,
                    Product = product,
                    Amount = 1
                };

                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Amount++;
            }

            _context.SaveChanges();
        }

        public int RemoveFromCart(Product product)
        {
            var cartItem =
                    _context.CartItems.SingleOrDefault(
                        s => s.Product.ProductId == product.ProductId && s.CartId == CartId);

            var localAmount = 0;

            if (cartItem != null)
            {
                if (cartItem.Amount > 1)
                {
                    cartItem.Amount--;
                    localAmount = cartItem.Amount;
                }
                else
                {
                    _context.CartItems.Remove(cartItem);
                }
            }

            _context.SaveChanges();

            return localAmount;
        }

        public List<CartItem> GetCartItems()
        {
            return CartItems ??
                   (CartItems =
                       _context.CartItems.Where(c => c.CartId == CartId)
                           .Include(s => s.Product)
                           .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _context
                .CartItems
                .Where(cart => cart.CartId == CartId);

            _context.CartItems.RemoveRange(cartItems);

            _context.SaveChanges();
        }

        public decimal GetCartTotal()
        {
            var total = _context.CartItems.Where(c => c.CartId == CartId)
                .Select(c => c.Product.Price * c.Amount).Sum();
            return total;
        }
    }
}
