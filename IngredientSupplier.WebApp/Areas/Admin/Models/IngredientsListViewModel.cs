using IngredientSupplier.Domain;

namespace IngredientSupplier.WebApp.Areas.Admin.Models
{
    public class IngredientsListViewModel
    {
        public IEnumerable<Ingredient> Ingredients { get; set; }
        public string CurrentCategory { get; set; }
    }
}
