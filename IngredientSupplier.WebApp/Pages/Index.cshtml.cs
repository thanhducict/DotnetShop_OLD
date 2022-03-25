using IngredientSupplier.Data;
using IngredientSupplier.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IngredientSupplier.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IEnumerable<Ingredient> IngredientsOfTheWeek { get; set; }

        public IndexModel(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public void OnGet()
        {
            IngredientsOfTheWeek = _ingredientRepository.IngredientsOfTheWeek;
        }
    }
}