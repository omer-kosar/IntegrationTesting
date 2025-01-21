using Customers.Api.Contracts.Data;

namespace Customers.Api.Repositories
{
    public interface ICustomerRepository
    {
        Task<bool> CreateAsync(CustomerDto customer);
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<bool> UpdateAsync(CustomerDto customer);
        Task<bool> DeleteAsync(Guid id);
        Task<CustomerDto> GetByIdAsync(Guid id);
    }
}
