using DotnetShop.Domain;

namespace DotnetShop.Data
{
    public class MockCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Shirt", Description = "Dotnet shirt."},
                new Category { CategoryId = 2, CategoryName = "Cup", Description = "Dotnet cup."},
                new Category { CategoryId = 3, CategoryName = "Badge", Description = "Dotnet badge."}
            };
        }
    }
}
