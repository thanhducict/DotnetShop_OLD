using DotnetShop.Data.Domain;

namespace DotnetShop.Data.Repositories
{
    public interface ICustomerRepository
    {
        Customer GetCustomerById(int customerId);
        Customer Add(Customer newCustomer);
        Customer Update(Customer updatedCustomer);
    }
}
