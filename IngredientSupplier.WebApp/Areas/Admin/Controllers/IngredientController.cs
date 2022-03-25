using IngredientSupplier.Data;
using IngredientSupplier.WebApp.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace IngredientSupplier.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IngredientController : Controller
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ICategoryRepository _categoryRepository;

        public IngredientController(IIngredientRepository ingredientRepository,
            ICategoryRepository categoryRepository)
        {
            _ingredientRepository = ingredientRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult List()
        {
            var ingredientListViewModel = new IngredientListViewModel();
            ingredientListViewModel.Ingredients = _ingredientRepository.AllIngredients;

            ingredientListViewModel.CurrentCategory = "Beverage";
            return View(ingredientListViewModel);
        }
    }
}

