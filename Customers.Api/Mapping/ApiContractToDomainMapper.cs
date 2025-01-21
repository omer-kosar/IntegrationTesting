using Customers.Api.Contracts.Requests;
using Customers.Api.Domain;
using Customers.Api.Domain.Common;

namespace Customers.Api.Mapping
{
    public static class ApiContractToDomainMapper
    {
        public static Customer ToCustomer(this CustomerRequest customerRequest)
        {
            return new Customer
            {
                Id = CustomerId.From(Guid.NewGuid()),
                GithubUserName = GithubUserName.From(customerRequest.GithubUserName),
                FullName = FullName.From(customerRequest.FullName),
                Email = Email.From(customerRequest.Email),
                DateOfBirth = DateOfBirth.From(customerRequest.DateOfBirth)
            };
        }
        public static Customer ToCustomer(this UpdateCustomerRequest updateCustomerRequest)
        {
            return new Customer
            {
                Id = CustomerId.From(Guid.NewGuid()),
                GithubUserName = GithubUserName.From(updateCustomerRequest.Customer.GithubUserName),
                FullName = FullName.From(updateCustomerRequest.Customer.FullName),
                Email = Email.From(updateCustomerRequest.Customer.Email),
                DateOfBirth = DateOfBirth.From(updateCustomerRequest.Customer.DateOfBirth)
            };
        }
    }
}