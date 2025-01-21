namespace Customers.Api.Contracts.Requests
{
    public class CustomerRequest
    {
        public string GithubUserName { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime DateOfBirth { get; set; } = default!;
    }
}
