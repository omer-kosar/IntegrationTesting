using Npgsql;
using System.Data;

namespace Customers.Api.Database
{
    public class NpgSqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public NpgSqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IDbConnection> CreateConnectionAsync()
        {
            var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}
