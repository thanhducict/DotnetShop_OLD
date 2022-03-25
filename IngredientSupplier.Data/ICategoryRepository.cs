using IngredientSupplier.Domain;

namespace IngredientSupplier.Data
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
    }
}
