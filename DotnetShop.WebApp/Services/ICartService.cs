using DotnetShop.Domain;

namespace DotnetShop.WebApp.Services
{
    public interface ICartService
    {
        Cart GetCart();
        void AddToCart(int productId, int quantity);
        void ClearCart();
        decimal GetCartTotal();
        int GetCartLineItemsQuantity();
    }
}
