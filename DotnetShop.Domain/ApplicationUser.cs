using Microsoft.AspNetCore.Identity;

namespace DotnetShop.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public Customer Customer { get; set; }
    }
}
