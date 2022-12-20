using DotnetShop.Data.Domain;

namespace DotnetShop.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DotnetShopDbContext _context;

        public OrderRepository(DotnetShopDbContext context)
        {
            _context = context;
        }

        public Order CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }
    }
}
