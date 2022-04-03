using DotnetShop.Domain;

namespace DotnetShop.Data
{
    public interface IOrderRepository
    {
        Order CreateOrder(Order order);
    }
}
