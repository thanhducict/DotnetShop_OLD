using DotnetShop.Admin.OrderManagement.Models;
using DotnetShop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetShop.Admin.OrderManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class OrderManagementController : ControllerBase
    {
        private readonly DotnetShopDbContext _context;

        public OrderManagementController(DotnetShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var result = await _context.Orders.Include(o => o.Customer).Select(o => new OrderViewModel
            {
                OrderId = o.OrderId,
                OrderPlaced = o.OrderPlaced,
                OrderTotal = o.OrderTotal,
                Customer = new CustomerViewModel
                {
                    CustomerName = o.Customer.CustomerName
                }
            }).ToListAsync();

            return new JsonResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderSummaryByOrderId([FromQuery] int orderId)
        {
            var model = await _context.Orders
                .Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    OrderPlaced = o.OrderPlaced,
                    OrderTotal = o.OrderTotal
                })
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (model == null)
            {
                return NotFound();
            }

            return new JsonResult(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailsByOrderId([FromQuery] int orderId)
        {
            var model = await _context.Orders
                .Include(o => o.OrderDetails)
                .Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    OrderDetails = o.OrderDetails.Select(od => new OrderDetailViewModel
                    {
                        ProductName = od.ProductName,
                        ProductPrice = od.ProductPrice,
                        Quantity = od.Quantity,
                        LineItemTotal = od.LineItemTotal
                    })
                })
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (model == null)
            {
                return NotFound();
            }

            return new JsonResult(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderCustomerByOrderId([FromQuery] int orderId)
        {
            var model = await _context.Orders
                .Include(o => o.Customer)
                .Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    Customer = new CustomerViewModel
                    {
                        CustomerName = o.Customer.CustomerName,
                        Gender = o.Customer.Gender,
                        Address = o.Customer.Address
                    }
                })
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (model == null)
            {
                return NotFound();
            }

            return new JsonResult(model);
        }
    }
}
