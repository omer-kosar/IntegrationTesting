using Customers.Api.Contracts.Responses;
using Customers.Api.Domain;

namespace Customers.Api.Mapping
{
    public static class DomainToApiContractMapper
    {
        public static CustomerResponse ToCustomerResponse(this Customer customer)
        {
            return new CustomerResponse
            {
                Id = customer.Id.Value,
                GithubUserName = customer.GithubUserName.Value,
                FullName = customer.FullName.Value,
                Email = customer.Email.Value,
                DateOfBirth = customer.DateOfBirth.Value
            };
        }
        public static GetAllCustomerResponse ToCustomersResponse(this IEnumerable<Customer> customers)
        {
            return new GetAllCustomerResponse
            {
                Customers = customers.Select(ToCustomerResponse)
            };
        }
    }
}
