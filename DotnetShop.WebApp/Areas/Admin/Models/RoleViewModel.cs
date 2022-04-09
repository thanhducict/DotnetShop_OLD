using System.ComponentModel.DataAnnotations;

namespace DotnetShop.WebApp.Areas.Admin.Models
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
