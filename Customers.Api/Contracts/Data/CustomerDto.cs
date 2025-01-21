namespace Customers.Api.Contracts.Data
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string GithubUserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
