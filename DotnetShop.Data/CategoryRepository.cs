using DotnetShop.Domain;

namespace DotnetShop.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DotnetShopDbContext _context;

        public CategoryRepository(DotnetShopDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories;
        }
    }
}
