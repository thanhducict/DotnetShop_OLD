using DotnetShop.Data;
using DotnetShop.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotnetShop.WebApp.APIs.CartManagement
{
    [ApiController]
    //[Authorize(Roles = "Admin")]
    [EnableCors("Policy1")]
    [Route("api/carts")]
    public class CartManagementController : ControllerBase
    {
        private readonly DotnetShopDbContext _context;

        public CartManagementController(DotnetShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartLineItemDto>>> GetCarts()
        {
            var cart = new Cart();
            var result = new List<CartLineItemDto>();
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
                        result.AddRange(cart.CartLineItems.Select(item => new CartLineItemDto
                        {
                            CustomerId = customer.CustomerId,
                            CustomerName = customer.CustomerName,
                            ProductId = item.ProductId,
                            ProductName = products.SingleOrDefault(p => p.ProductId == item.ProductId)?.Name
                                    ?? string.Empty,
                            Quantity = item.Quantity
                        }).ToList());
                    }
                }
            }

            return Ok(result);
        }
    }
}
