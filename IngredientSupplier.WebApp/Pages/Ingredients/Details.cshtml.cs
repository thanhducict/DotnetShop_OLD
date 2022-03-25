using IngredientSupplier.Data;
using IngredientSupplier.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IngredientSupplier.WebApp.Pages.Ingredients
{
    public class DetailsModel : PageModel
    {
        private readonly IIngredientRepository _ingredientRepository;

        public Ingredient Ingredient { get; set; }

        public DetailsModel(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public IActionResult OnGet(int id)
        {
            var result = _ingredientRepository.GetIngredientById(id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                Ingredient = result;
            }

            return Page();
        }
    }
}
