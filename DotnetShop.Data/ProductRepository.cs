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

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.Include(c => c.Category);
        }

        public Product GetProductById(int productId)
        {
            return _context.Products.FirstOrDefault(p => p.ProductId == productId);
        }
    }
}
