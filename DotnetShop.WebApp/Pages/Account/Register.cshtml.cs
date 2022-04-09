using DotnetShop.Data;
using DotnetShop.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DotnetShop.WebApp.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICustomerRepository _customerRepository;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ICustomerRepository customerRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _customerRepository = customerRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public CustomerViewModel Customer { get; set; }
        }

        public class CustomerViewModel
        {
            [Required]
            [Display(Name = "Customer name")]
            public string CustomerName { get; set; }
            public bool Gender { get; set; }

            // use nullable to avoid required
            public string? Address { get; set; }
        }

        public async Task OnGetAsync()
        {
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    CustomerName = Input.Customer.CustomerName,
                    Gender = Input.Customer.Gender,
                    Address = Input.Customer.Address
                };
                var user = new ApplicationUser
                {
                    UserName = Input.Username,
                    Email = Input.Email,
                    Customer = customer
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (!await _roleManager.RoleExistsAsync(Constants.CustomerRole))
                {
                    var customerRole = new IdentityRole { Name = Constants.CustomerRole };
                    await _roleManager.CreateAsync(customerRole);
                }
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Constants.CustomerRole);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(Url.Content("~/"));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
