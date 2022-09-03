using Microsoft.AspNetCore.Identity;

namespace DotnetShop.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string Level { get; set; }
        public Customer Customer { get; set; }
    }
}
