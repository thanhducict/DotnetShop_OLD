using DotnetShop.Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotnetShop.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DotnetShopDbContext _context;

        public CustomerRepository(DotnetShopDbContext context)
        {
            _context = context;
        }

        public Customer GetCustomerById(int customerId)
        {
            return _context.Customers.AsNoTracking()
                .Where(c => c.CustomerId == customerId).SingleOrDefault();
        }

        public Customer Add(Customer newCustomer)
        {
            _context.Customers.Add(newCustomer);
            _context.SaveChanges();
            return newCustomer;
        }

        public Customer Update(Customer updatedCustomer)
        {
            var customer = _context.Customers.Attach(updatedCustomer);
            customer.State = EntityState.Modified;

            _context.SaveChanges();

            return updatedCustomer;
        }
    }
}
