using DotnetShop.Data.Domain;

namespace DotnetShop.Data.Repositories
{
    public interface IOrderRepository
    {
        Order CreateOrder(Order order);
    }
}
