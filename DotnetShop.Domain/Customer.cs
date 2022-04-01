using System.ComponentModel.DataAnnotations;

namespace DotnetShop.Domain
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Display(Name = "Customer name")]
        public string CustomerName { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; } 
        public string Cart { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
