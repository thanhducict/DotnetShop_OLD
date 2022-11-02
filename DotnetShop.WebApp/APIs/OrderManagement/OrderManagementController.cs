using DotnetShop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetShop.WebApp.APIs.OrderManagement
{
    [ApiController]
    [EnableCors("Policy1")]
    [Route("api/orders")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    public class OrderManagementController : ControllerBase
    {
        private readonly DotnetShopDbContext _context;

        public OrderManagementController(DotnetShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var result = await _context.Orders.Include(o => o.Customer).Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                OrderPlaced = o.OrderPlaced,
                OrderTotal = o.OrderTotal,
                CustomerName = o.Customer.CustomerName
            }).ToListAsync();

            return Ok(result);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderWithDetailDto>> GetOrderById(int orderId)
        {
            var model = await _context.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.Customer)
                .Select(o => new OrderWithDetailDto
                {
                    OrderId = o.OrderId,
                    OrderPlaced = o.OrderPlaced,
                    OrderTotal = o.OrderTotal,
                    OrderDetailViewModels = o.OrderDetails.Select(od => new OrderDetailDto
                    {
                        OrderDetailId = od.OrderDetailId,
                        ProductName = od.ProductName,
                        ProductPrice = od.ProductPrice,
                        Quantity = od.Quantity,
                        LineItemTotal = od.LineItemTotal
                    }).ToList(),
                    Customer = new OrderCustomerDto
                    {
                        CustomerId = o.CustomerId,
                        CustomerName = o.Customer.CustomerName,
                        Gender = o.Customer.Gender,
                        Address = o.Customer.Address,
                    }
                })
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            if (!int.TryParse(id, out int orderId))
            {
                return NotFound($"{id} does not exist.");
            }

            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound($"Order does not exist.");
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
