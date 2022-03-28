using DotnetShop.Domain;

namespace ProductSupplier.WebApp.Areas.Admin.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public string CurrentCategory { get; set; }
    }
}
