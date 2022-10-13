using DotnetShop.Data.Domain;

namespace DotnetShop.WebApp.Services
{
    public interface ICartService
    {
        Cart GetCart();
        void AddToCart(int productId, int quantity);
        void RemoveFromCart(int productId);
        void ClearCart();
        decimal GetCartTotal();
        int GetCartLineItemsQuantity();
    }
}
