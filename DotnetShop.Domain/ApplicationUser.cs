using Microsoft.AspNetCore.Identity;

namespace DotnetShop.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public int CustomerId { get; set; }
    }
}
