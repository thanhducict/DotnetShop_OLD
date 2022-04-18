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
            var result = await _context.Orders.Include(o => o.Customer).Select(o => new OrderListViewModel
            {
                OrderId = o.OrderId,
                OrderPlaced = o.OrderPlaced,
                OrderTotal = o.OrderTotal,
                CustomerName = o.Customer.CustomerName
            }).ToListAsync();

            return new JsonResult(result);
        }

        [HttpGet("{id}")]
        [Route("api/[controller]/GetOrderById")]
        public async Task<IActionResult> GetOrderByOrderId([FromQuery] int orderId)
        {
            var model = await _context.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.Customer)
                .Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    OrderPlaced = o.OrderPlaced,
                    OrderTotal = o.OrderTotal,
                    OrderDetailViewModels = o.OrderDetails.Select(od => new OrderDetailViewModel
                    {
                        ProductName = od.ProductName,
                        ProductPrice = od.ProductPrice,
                        Quantity = od.Quantity,
                        LineItemTotal = od.LineItemTotal
                    }).ToList(),
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
