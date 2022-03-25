using IngredientSupplier.Domain;

namespace IngredientSupplier.Data
{
    public interface IIngredientRepository
    {
        IEnumerable<Ingredient> AllIngredients { get; }
        IEnumerable<Ingredient> IngredientsOfTheWeek { get; }
        Ingredient GetIngredientById(int ingredientId);
    }
}
