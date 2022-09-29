using DotnetShop.Data;
using Microsoft.AspNetCore.Mvc;

namespace DotnetShop.WebApp.Pages.Shared.Components.CategoryMenu
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryMenu(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryRepository.GetCategories().OrderBy(c => c.CategoryId);
            return View(categories);
        }
    }
}
