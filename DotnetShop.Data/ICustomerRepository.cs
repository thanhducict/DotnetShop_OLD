using DotnetShop.Domain;

namespace DotnetShop.Data
{
    public interface ICustomerRepository
    {
        Customer GetCustomerById(int customerId);
        Customer Add(Customer newCustomer);
        Customer Update(Customer updatedCustomer);
    }
}
