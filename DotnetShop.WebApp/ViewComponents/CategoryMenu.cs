using DotnetShop.Data;
using Microsoft.AspNetCore.Mvc;

namespace DotnetShop.WebApp.ViewComponents
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
            var categories = _categoryRepository.AllCategories.OrderBy(c => c.CategoryId);
            return View(categories);
        }
    }
}
