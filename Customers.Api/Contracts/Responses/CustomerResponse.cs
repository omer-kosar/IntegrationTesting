namespace Customers.Api.Contracts.Responses
{
    public class CustomerResponse
    {
        public Guid Id { get; set; }
        public string GithubUserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
