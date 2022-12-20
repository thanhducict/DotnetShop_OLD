using DotnetShop.Data.Domain;

namespace DotnetShop.Data.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProductById(int productId);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int productId);
    }
}
