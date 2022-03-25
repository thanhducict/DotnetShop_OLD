using IngredientSupplier.Data;
using IngredientSupplier.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IngredientSupplier.WebApp.Pages.Ingredients
{
    public class ListModel : PageModel
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IEnumerable<Ingredient> Ingredients { get; set; }
        public string CurrentCategory { get; set; }

        public ListModel(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public void OnGet()
        {
            Ingredients = _ingredientRepository.AllIngredients;
            CurrentCategory = "Beverage";
        }
    }
}
