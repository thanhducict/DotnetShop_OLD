using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetShop.Domain;

namespace DotnetShop.Data
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
