using DotnetShop.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotnetShop.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DotnetShopDbContext _context;

        public CategoryRepository(DotnetShopDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            return _context.Categories.FirstOrDefault(p => p.CategoryId == categoryId);
        }

        public void CreateCategory(Category category)
        {
            _context.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            // update Category when tracked
            var selectedCategory = _context.Categories.Find(category.CategoryId);
            if (selectedCategory != null)
            {
                selectedCategory.CategoryName = category.CategoryName;
                selectedCategory.Description = category.Description;
            }
            _context.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            var product = _context.Categories.Find(categoryId);
            if (product != null)
            {
                _context.Categories.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
