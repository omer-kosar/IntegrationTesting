using Customers.Api.Contracts.Data;
using Customers.Api.Domain;
using Customers.Api.Domain.Common;

namespace Customers.Api.Mapping
{
    public static class DtoToDomainMapper
    {
        public static Customer ToCustomer(this CustomerDto customerDto)
        {
            return new Customer
            {
                Id = CustomerId.From(customerDto.Id),
                GithubUserName = GithubUserName.From(customerDto.GithubUserName),
                FullName = FullName.From(customerDto.FullName),
                Email = Email.From(customerDto.Email),
                DateOfBirth = DateOfBirth.From(customerDto.DateOfBirth)
            };
        }
    }
}
