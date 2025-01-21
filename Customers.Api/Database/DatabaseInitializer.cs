using Dapper;

namespace Customers.Api.Database
{
    public class DatabaseInitializer
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public DatabaseInitializer(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task InitializeAsync()
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS Customers (Id UUID PRIMARY KEY,
               GithubUserName TEXT NOT NULL,
               FullName TEXT NOT NULL,
               Email TEXT NOT NULL,
               DateOfBirth TEXT NOT NULL)");
        }
    }
}
