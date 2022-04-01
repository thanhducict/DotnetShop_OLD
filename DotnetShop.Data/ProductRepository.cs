using DotnetShop.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotnetShop.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly DotnetShopDbContext _context;

        public ProductRepository(DotnetShopDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> AllProducts
        {
            get
            {
                return _context.Products.Include(c => c.Category);
            }
        }

        public IEnumerable<Product> ProductsOfTheWeek
        {
            get
            {
                return _context.Products.Include(c => c.Category).Where(p => p.IsProductOfTheWeek);
            }
        }

        public Product GetProductById(int productId)
        {
            return _context.Products.FirstOrDefault(p => p.ProductId == productId);
        }
    }
}
