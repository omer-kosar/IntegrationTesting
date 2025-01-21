using System.ComponentModel;

namespace Customers.Api.Contracts.Responses
{
    public class GetAllCustomerResponse
    {
        public IEnumerable<CustomerResponse> Customers { get; set; } = Enumerable.Empty<CustomerResponse>();
    }
}
