using DotnetShop.Domain;

namespace DotnetShop.Data
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProductById(int productId);
    }
}
