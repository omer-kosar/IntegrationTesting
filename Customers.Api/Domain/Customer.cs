using Customers.Api.Domain.Common;

namespace Customers.Api.Domain
{
    public class Customer
    {
        public CustomerId Id { get; set; } = CustomerId.From(Guid.NewGuid());
        public GithubUserName GithubUserName { get; set; } = default!;
        public FullName FullName { get; set; } = default!;
        public Email Email { get; set; } = default!;
        public DateOfBirth DateOfBirth { get; set; } = default!;
    }
}
