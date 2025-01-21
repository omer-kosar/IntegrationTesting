using Customers.Api.Contracts.Data;
using Customers.Api.Domain;

namespace Customers.Api.Mapping
{
    public static class DomainToDtoMapper
    {
        public static CustomerDto ToCustomerDto(this Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id.Value,
                GithubUserName = customer.GithubUserName.Value,
                FullName = customer.FullName.Value,
                Email = customer.Email.Value,
                DateOfBirth = customer.DateOfBirth.Value
            };
        }
    }
}