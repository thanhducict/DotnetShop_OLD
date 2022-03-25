using IngredientSupplier.Domain;

namespace IngredientSupplier.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IngredientSupplierDbContext _context;

        public CategoryRepository(IngredientSupplierDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> AllCategories => _context.Categories;

    }
}
