using DotnetShop.Domain;

namespace DotnetShop.Data
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
    }
}
