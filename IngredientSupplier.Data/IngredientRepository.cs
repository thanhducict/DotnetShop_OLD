using IngredientSupplier.Domain;
using Microsoft.EntityFrameworkCore;

namespace IngredientSupplier.Data
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly IngredientSupplierDbContext _context;

        public IngredientRepository(IngredientSupplierDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Ingredient> AllIngredients
        {
            get
            {
                return _context.Ingredients.Include(c => c.Category);
            }
        }

        public IEnumerable<Ingredient> IngredientsOfTheWeek
        {
            get
            {
                return _context.Ingredients.Include(c => c.Category).Where(p => p.IsIngredientOfTheWeek);
            }
        }

        public Ingredient GetIngredientById(int IngredientId)
        {
            return _context.Ingredients.FirstOrDefault(p => p.IngredientId == IngredientId);
        }
    }
}
