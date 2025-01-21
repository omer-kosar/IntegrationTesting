using Customers.Api.Contracts.Data;
using Customers.Api.Domain;
using Customers.Api.Mapping;
using Customers.Api.Repositories;
using FluentValidation.Results;

namespace Customers.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        private readonly IGitHubService _githubService;
        public CustomerService(ICustomerRepository customerRepository, IGitHubService githubService)
        {
            _customerRepository = customerRepository;
            _githubService = githubService;
        }
        public async Task<bool> CreateAsync(Customer customer)
        {
            var existingUser = await _customerRepository.GetByIdAsync(customer.Id.Value);
            if (existingUser is not null)
            {
                var message = $"Customer with id {customer.Id.Value} already exists";
                throw new FluentValidation.ValidationException(message, GenerateValidationError(message));
            }
            var isValidGithubUser = await _githubService.IsValidGitHubUser(customer.GithubUserName.Value);

            if (!isValidGithubUser)
            {
                var message = $"{customer.GithubUserName.Value} is not a valid GitHub username";
                throw new FluentValidation.ValidationException(message, GenerateValidationError(message));
            }
            var customerDto = customer.ToCustomerDto();
            return await _customerRepository.CreateAsync(customerDto);
        }
        public async Task<Customer> GetByIdAsync(Guid id)
        {
            var customerDto = await _customerRepository.GetByIdAsync(id);
            return customerDto?.ToCustomer();
        }
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var customerDtos = await _customerRepository.GetAllAsync();
            return customerDtos.Select(c => c.ToCustomer());
        }
        public async Task<bool> UpdateAsync(Customer customer)
        {
            var customerDto = customer.ToCustomerDto();
            var isValidGitHubUsesr = await _githubService.IsValidGitHubUser(customer.GithubUserName.Value);
            if (!isValidGitHubUsesr)
            {
                var message = $"{customer.GithubUserName.Value} is not a valid GitHub username";
                throw new FluentValidation.ValidationException(message, GenerateValidationError(message));
            }
            return await _customerRepository.UpdateAsync(customerDto);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _customerRepository.DeleteAsync(id);
        }
        private ValidationFailure[] GenerateValidationError(string message)
        {
            return new[] { new ValidationFailure(nameof(Customer), message) };
        }
    }
}
