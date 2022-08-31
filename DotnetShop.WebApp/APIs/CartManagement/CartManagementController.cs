using DotnetShop.Data;
using DotnetShop.Domain;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotnetShop.WebApp.APIs.CartManagement
{
    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/carts")]
    [EnableCors("Policy1")]
    public class CartManagementController : ControllerBase
    {
        private readonly DotnetShopDbContext _context;

        public CartManagementController(DotnetShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartDto>>> GetCarts()
        {
            var cart = new Cart();
            var result = new List<CartDto>();
            var customers = await _context.Customers.ToListAsync();
            var products = await _context.Products.ToListAsync();

            if (customers != null && customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    cart = JsonSerializer.Deserialize<Cart>(customer.Cart, new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve
                    });

                    if (cart != null)
                    {
                        result.Add(new CartDto
                        {
                            CartLineItemDtos = cart.CartLineItems.Select(item => new CartLineItemDto
                            {
                                CustomerId = customer.CustomerId,
                                CustomerName = customer.CustomerName,
                                ProductId = item.ProductId,
                                ProductName = products.SingleOrDefault(p => p.ProductId == item.ProductId)?.Name
                                    ?? string.Empty,
                                Quantity = item.Quantity
                            })
                        });
                    }
                }
            }

            return Ok(result);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<CartDto>> GetCustomerCart(int customerId)
        {
            var cart = new Cart();
            var result = new List<CartDto>();
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);
            var products = await _context.Products.ToListAsync();

            if (customer != null)
            {
                cart = JsonSerializer.Deserialize<Cart>(customer.Cart, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                });

                if (cart != null)
                {
                    result.Add(new CartDto
                    {
                        CartLineItemDtos = cart.CartLineItems.Select(item => new CartLineItemDto
                        {
                            CustomerId = customer.CustomerId,
                            CustomerName = customer.CustomerName,
                            ProductId = item.ProductId,
                            ProductName = products.SingleOrDefault(p => p.ProductId == item.ProductId)?.Name
                            ?? string.Empty,
                            Quantity = item.Quantity
                        })
                    });
                }
            }

            return Ok(result);
        }
    }
}
