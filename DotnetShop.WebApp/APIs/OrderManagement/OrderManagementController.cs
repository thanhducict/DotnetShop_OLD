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
                    Customer = new CustomerDto
                    {
                        CustomerId = o.CustomerId,
                        CustomerName = o.Customer.CustomerName,
                        Gender = o.Customer.Gender,
                        Address = o.Customer.Address,
                        Country = o.Customer.Country
                    }
                })
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderWithDetailDto model)
        {
            var order = _context.Orders.Include(o => o.OrderDetails).FirstOrDefault(o => o.OrderId == model.OrderId);
            if (order != null)
            {
                order.OrderTotal = model.OrderTotal;
                order.OrderDetails.ForEach(od => model.OrderDetailViewModels.ForEach(odvm =>
                {
                    if (odvm.OrderDetailId == od.OrderDetailId)
                    {
                        od.Quantity = odvm.Quantity;
                        od.LineItemTotal = odvm.LineItemTotal;
                    };
                }));

                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
