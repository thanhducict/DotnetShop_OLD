using DotnetShop.Data;
using DotnetShop.WebApp.APIs.OrderManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DotnetShop.WebApp.APIs.ReportManagement
{
    [ApiController]
    [EnableCors("Policy1")]
    [Route("api/reports")]
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "MustBeAdminAndLeader")]
    public class ReportManagementController : ControllerBase
    {
        private readonly DotnetShopDbContext _context;

        public ReportManagementController(DotnetShopDbContext context)
        {
            _context = context;
        }

        [HttpGet("orders/customer/{customerId}")]
        public IActionResult GetCustomerOrderReport(int customerId)
        {
            var model = _context.Orders
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
                        Country = o.Customer.Country,
                    }
                })
                .Where(o => o.Customer.CustomerId == customerId)
                .ToList();

            if (model != null)
            {
                var sb = new StringBuilder();
                sb.AppendLine($"ORDERS of {model[0].Customer.CustomerName.ToUpper()}");
                sb.AppendLine("====================");
                sb.AppendLine("");
                foreach (var item in model)
                {
                    sb.AppendLine($"Order Id: {item.OrderId}");
                    sb.AppendLine($"Order Placed: {item.OrderPlaced}");
                    sb.AppendLine($"Order Total: {item.OrderId}");

                    foreach (var subItem in item.OrderDetailViewModels)
                    {
                        sb.AppendLine($"Order Detail Id: {subItem.OrderDetailId}");
                        sb.AppendLine($"ProductName: {subItem.ProductName}");
                        sb.AppendLine($"ProductPrice: {subItem.ProductPrice}");
                        sb.AppendLine($"Quantity: {subItem.Quantity}");
                        sb.AppendLine($"Line Item Total: {subItem.LineItemTotal}");
                    }
                    sb.AppendLine();
                }

                var fileBytes = Encoding.ASCII.GetBytes(sb.ToString());

                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet,
                    string.Format("OrderReport_{0}.txt", DateTime.Now.ToString("yy-MM-dd")));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
