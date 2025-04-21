using BackendApplication.Schema.Types;
using BackendApplication.Services.Abstractions;
using Dapper;

namespace BackendApplication.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;
        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<Order> AddAsync(Order order)
        {
            var newId = Guid.NewGuid();
            var parameters = new
            {
                Id = newId,
                UserId = order.UserId,
                User = order.User,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                DeliveryAddress = order.DeliveryAddress,
                OrderItems = order.OrderItems
            };
            using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            const string query = @"
                INSERT INTO ""Orders"" (""Id"", ""UserId"", ""OrderDate"", ""TotalPrice"", ""DeliveryAddress"") 
                VALUES (@Id, @UserId, @OrderDate, @TotalPrice, @DeliveryAddress)
                RETURNING *";
            return await connection.QuerySingleAsync<Order>(query, parameters);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            const string query = @"DELETE FROM ""Orders"" WHERE ""Id"" = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            const string query = @"SELECT * FROM ""Orders""";
            return await connection.QueryAsync<Order>(query);
        }

        public async Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId)
        {
            using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            const string query = @"SELECT * FROM ""Orders"" WHERE ""UserId"" = @UserId";
            return await connection.QueryAsync<Order>(query, new { UserId = userId });
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            const string query = @"SELECT * FROM ""Orders"" WHERE ""Id"" = @Id";
            return await connection.QuerySingleOrDefaultAsync<Order>(query, new { Id = id });
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            const string query = @"
                UPDATE ""Orders"" 
                SET ""UserId"" = @UserId, ""OrderDate"" = @OrderDate, ""TotalPrice"" = @TotalPrice, ""DeliveryAddress"" = @DeliveryAddress
                WHERE ""Id"" = @Id
                RETURNING *";
            return await connection.QuerySingleAsync<Order>(query, order);
        }
    }
}
