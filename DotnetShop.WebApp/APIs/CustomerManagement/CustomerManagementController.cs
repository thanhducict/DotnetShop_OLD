using DotnetShop.Data;
using DotnetShop.Domain;
using DotnetShop.WebApp.APIs.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DotnetShop.WebApp.APIs.CustomerManagement
{
    [ApiController]
    //[Authorize(Roles = "Admin")]
    [EnableCors("Policy1")]
    [Route("api/customers")]
    public class CustomerManagementController : ControllerBase
    {
        private readonly DotnetShopDbContext _context;
        private readonly ILogger<CustomerManagementController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomerManagementController(DotnetShopDbContext context,
            ILogger<CustomerManagementController> logger,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers(
            string? name, string? searchQuery, int pageNumber = 1, int pageSize = 5)
        {
            var collection = _context.Customers as IQueryable<Customer>;

            if (pageSize > PaginationMetadata.maxCitiesPageSize)
            {
                return BadRequest("The PageSize exceeded the maximum of 20.");
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.CustomerName == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.CustomerName.Contains(searchQuery)
                    || (a.Address != null && a.Address.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var result = await collection
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            var customers = result.Select(c => new CustomerDto
            {
                CustomerName = c.CustomerName,
                Gender = c.Gender,
                Address = c.Address,
                Cart = c.Cart
            });

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(customers);
        }

        [HttpGet("{customerId}", Name = "GetCustomerById")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int customerId)
        {
            var result = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (result == null)
            {
                _logger.LogInformation($"Customer with Id {customerId} was not found.");
                return NotFound();
            }

            var customer = new CustomerDto
            {
                CustomerName = result.CustomerName,
                Gender = result.Gender,
                Address = result.Address,
                Cart = result.Cart,
            };

            return Ok(customer);
        }

        [HttpGet("username/{username}", Name = "GetCustomerByUsername")]
        public async Task<ActionResult<CustomerDto>> GetCustomerByUsername(string username)
        {
            var result = await _userManager.Users.Include(u => u.Customer).FirstOrDefaultAsync(c => c.UserName == username);

            if (result == null)
            {

                return NotFound();
            }

            var customer = new CustomerDto
            {
                CustomerName = result.Customer.CustomerName,
                Gender = result.Customer.Gender,
                Address = result.Customer.Address,
                Cart = result.Customer.Cart,
            };

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> AddCustomer([FromBody] CustomerCreationDto customerCreationDto) // [FromBody] is not necessary because it is default by ApiController attribute
        {
            if (await _userManager.FindByNameAsync(customerCreationDto.Username) != null)
            {
                return NotFound($"{customerCreationDto.Username} already exists.");
            }

            var user = new ApplicationUser
            {
                UserName = customerCreationDto.Username,
                Email = customerCreationDto.Email,
                Customer = new Customer
                {
                    CustomerName = customerCreationDto.CustomerName,
                    Gender = customerCreationDto.Gender,
                    Address = customerCreationDto.Address,
                    Cart = string.Empty
                }
            };

            var result = await _userManager.CreateAsync(user, "Cvbnm123@");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Constants.CustomerRole);
                return CreatedAtRoute("GetCustomerByUsername", new { username = customerCreationDto.Username }, "A user is created.");
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public async Task<ActionResult<CustomerDto>> UpdateCustomer(CustomerUpdateDto customerUpdateDto)
        {
            var user = await _userManager.Users.Include(u => u.Customer)
                .FirstOrDefaultAsync(u => u.UserName == customerUpdateDto.Username);
            if (user == null)
            {
                return NotFound($"{customerUpdateDto.Username} does not exist.");
            }


            user.UserName = customerUpdateDto.Username;
            user.Email = customerUpdateDto.Email;
            user.Customer.CustomerName = customerUpdateDto.CustomerName;
            user.Customer.Gender = customerUpdateDto.Gender;
            user.Customer.Address = customerUpdateDto.Address;


            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{username}")]
        public async Task<ActionResult<CustomerDto>> DeleteCustomer(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound($"{username} does not exist.");
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
