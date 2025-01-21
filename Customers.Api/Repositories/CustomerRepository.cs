using Customers.Api.Contracts.Data;
using Customers.Api.Database;
using Customers.Api.Domain.Common;
using Dapper;

namespace Customers.Api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public CustomerRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<bool> CreateAsync(CustomerDto customer)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var parameters = new
            {
                customer.Id,
                customer.GithubUserName,
                customer.Email,
                customer.DateOfBirth,
                customer.FullName
            };

            var result = await connection.ExecuteAsync(@"INSERT INTO Customers (Id, GithubUserName, Email, DateOfBirth,FullName)
                VALUES (@Id, @GithubUserName, @Email, @DateOfBirth,@FullName)", customer);
            return result > 0;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var result = await connection.ExecuteAsync(@"DELETE FROM Customers WHERE Id = @Id", new { Id = id });
            return result > 0;
        }
        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            return await connection.QueryAsync<CustomerDto>("SELECT * FROM Customers");
        }

        public async Task<CustomerDto?> GetByIdAsync(Guid id)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            return await connection.QuerySingleOrDefaultAsync<CustomerDto>("SELECT * FROM Customers WHERE Id = @Id LIMIT 1", new { Id = id });
        }

        public async Task<bool> UpdateAsync(CustomerDto customer)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var parameters = new
            {
                customer.Id,
                customer.GithubUserName,
                customer.Email,
                customer.DateOfBirth,
                customer.FullName
            };

            var result = await connection.ExecuteAsync(@"UPDATE Customers SET GithubUserName = @GithubUserName, Email = @Email, DateOfBirth = @DateOfBirth, FullName = @FullName WHERE Id = @Id", customer);
            return result > 0;
        }
    }
}
