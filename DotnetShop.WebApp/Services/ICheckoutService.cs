using DotnetShop.WebApp.Models;

namespace DotnetShop.WebApp.Services
{
    public interface ICheckoutService
    {
        void Checkout(int customerId, CartViewModel cartViewModel);
    }
}
