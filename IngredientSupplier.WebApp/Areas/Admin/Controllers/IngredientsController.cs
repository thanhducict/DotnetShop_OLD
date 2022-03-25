using IngredientSupplier.Data;
using IngredientSupplier.WebApp.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace IngredientSupplier.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IngredientsController : Controller
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ICategoryRepository _categoryRepository;

        public IngredientsController(IIngredientRepository ingredientRepository,
            ICategoryRepository categoryRepository)
        {
            _ingredientRepository = ingredientRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult List()
        {
            var ingredientListViewModel = new IngredientsListViewModel();
            ingredientListViewModel.Ingredients = _ingredientRepository.AllIngredients;

            ingredientListViewModel.CurrentCategory = "Beverage";
            return View(ingredientListViewModel);
        }

        public IActionResult Details(int id)
        {
            var ingredient = _ingredientRepository.GetIngredientById(id);
            if (ingredient == null)
                return NotFound();

            return View(ingredient);
        }
    }
}

