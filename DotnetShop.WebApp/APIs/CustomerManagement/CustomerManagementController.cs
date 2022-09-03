using DotnetShop.Data;
using DotnetShop.Domain;
using DotnetShop.WebApp.APIs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DotnetShop.WebApp.APIs.CustomerManagement
{
    /// <summary>
    /// APIs to work with customers.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [EnableCors("Policy1")]
    [Route("api/v{version:apiVersion}/customers")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    public class CustomerManagementController : ControllerBase
    {
        private readonly DotnetShopDbContext _context;
        private readonly ILogger<CustomerManagementController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// The constructor of controller.
        /// </summary>
        /// <param name="context">The Db Context.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="userManager">The user manager.</param>
        public CustomerManagementController(DotnetShopDbContext context,
            ILogger<CustomerManagementController> logger,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        /// <summary>
        /// Gets customers by name.
        /// </summary>
        /// <param name="name">The name of customer. Leaves blank to get all customers.</param>
        /// <param name="searchQuery">Filters customres by name, address and country.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>A list of <see cref="CustomerDto"/>.</returns>
        /// <response code="200">Returns a list of customers or a customer.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                    || (a.Address != null && a.Address.Contains(searchQuery))
                    || (a.Country != null && a.Country.Contains(searchQuery)));
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
                Country = c.Country,
                Cart = c.Cart
            });

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(customers);
        }

        /// <summary>
        /// Gets a customer by Id.
        /// </summary>
        /// <param name="customerId">The customer Id.</param>
        /// <returns>A <see cref="CustomerDto"/>.</returns>
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
                Country = result.Country,
                Cart = result.Cart,
            };

            return Ok(customer);
        }

        /// <summary>
        /// Gets a customer by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>A <see cref="CustomerDto"/>.</returns>
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
                Country = result.Customer.Country,
                Cart = result.Customer.Cart,
            };

            return Ok(customer);
        }

        /// <summary>
        /// Adds a customer.
        /// </summary>
        /// <param name="customerCreationDto">The model of a customer.</param>
        /// <returns>A <see cref="CreatedAtRouteResult"/>.</returns>
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
                    Country = customerCreationDto.Country,
                    Cart = string.Empty
                }
            };

            var result = await _userManager.CreateAsync(user, "Cvbnm123@");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Constants.Roles.CustomerRole);
                return CreatedAtRoute("GetCustomerByUsername", new { username = customerCreationDto.Username }, "A user is created.");
            }
            else
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// Updates a customer.
        /// </summary>
        /// <param name="customerUpdateDto">The model of a customer.</param>
        /// <returns>A <see cref="NoContentResult"/>.</returns>
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
            user.Customer.Country = customerUpdateDto.Country;

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

        /// <summary>
        /// Deletes a customer.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>A <see cref="NoContentResult"/>.</returns>
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
