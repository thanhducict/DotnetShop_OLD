using IngredientSupplier.Domain;

namespace IngredientSupplier.WebApp.Areas.Admin.Models
{
    public class IngredientListViewModel
    {
        public IEnumerable<Ingredient> Ingredients { get; set; }
        public string CurrentCategory { get; set; }
    }
}
