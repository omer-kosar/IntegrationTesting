using Customers.Api.Contracts.Data;
using Customers.Api.Domain;

namespace Customers.Api.Services
{
    public interface ICustomerService
    {
        Task<bool> CreateAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<bool> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(Guid id);
        Task<Customer> GetByIdAsync(Guid id);
    }
}
