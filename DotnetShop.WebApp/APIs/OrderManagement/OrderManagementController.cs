using DotnetShop.Data;
using DotnetShop.Data.Domain;
using DotnetShop.WebApp.APIs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders(int pageNumber = 1, int pageSize = 20)
        {
            if (pageSize > PaginationMetadata.maxCitiesPageSize)
            {
                return BadRequest("The PageSize exceeded the maximum of 20.");
            }

            var orders = _context.Orders.Include(o => o.Customer).Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                OrderPlaced = DateTime.SpecifyKind(o.OrderPlaced, DateTimeKind.Utc),
                OrderTotal = o.OrderTotal,
                CustomerName = o.Customer.CustomerName
            });
            var result = await orders.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();

            var paginationMetadata = new PaginationMetadata(orders.Count(), pageSize, pageNumber);
            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

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

        [HttpPut]
        public async Task<ActionResult<OrderWithDetailDto>> UpdateOrder(OrderWithDetailUpdateDto orderUpdateDto)
        {
            var order = await _context.Orders.FindAsync(orderUpdateDto.OrderId);
            if (order == null)
            {
                return NotFound($"Order does not exist.");
            }

            order.OrderPlaced = orderUpdateDto.OrderPlaced;
            await _context.SaveChangesAsync();

            return Ok("Updated successfully.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(DeleteOrderDto deleteOrderDto)
        {
            var orders = new List<Order>();

            foreach (var id in deleteOrderDto.OrderIds)
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
                else
                {
                    orders.Add(order);
                }
            }

            _context.Orders.RemoveRange(orders);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
