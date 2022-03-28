using DotnetShop.Domain;

namespace DotnetShop.WebApp.Areas.Admin.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public string CurrentCategory { get; set; }
    }
}
