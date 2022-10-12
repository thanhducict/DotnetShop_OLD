using System.ComponentModel.DataAnnotations;

namespace DotnetShop.WebApp.Areas.Admin.Models
{
    public class RoleModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
