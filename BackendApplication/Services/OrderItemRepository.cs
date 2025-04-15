using BackendApplication.Schema.Types;
using BackendApplication.Services.Abstractions;
using Dapper;

namespace BackendApplication.Services
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly string _connectionString;
        public OrderItemRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<OrderItem> AddAsync(OrderItem orderItem)
        {
            var newId = Guid.NewGuid();
            var parameters = new
            {
                Id = newId,
                OrderId = orderItem.OrderId,
                ContactLensTypeId = orderItem.ContactLensTypeId,
                OrderQuantity = orderItem.OrderQuantity,
                Price = orderItem.Price
            };
            using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            const string query = @"
                INSERT INTO ""OrderItems"" (""Id"", ""OrderId"", ""ContactLensTypeId"", ""OrderQuantity"", ""Price"") 
                VALUES (@Id, @OrderId, @ContactLensTypeId, @OrderQuantity, @Price)
                RETURNING *";
            return await connection.QuerySingleAsync<OrderItem>(query, parameters);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            const string query = @"DELETE FROM ""OrderItems"" WHERE ""Id"" = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            const string query = @"SELECT * FROM ""OrderItems""";
            return await connection.QueryAsync<OrderItem>(query);
        }

        public async Task<OrderItem?> GetByIdAsync(Guid id)
        {
            using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            const string query = @"SELECT * FROM ""OrderItems"" WHERE ""Id"" = @Id";
            return await connection.QuerySingleOrDefaultAsync<OrderItem>(query, new { Id = id });
        }

        public async Task<OrderItem> UpdateAsync(OrderItem orderItem)
        {
            using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            const string query = @"
                UPDATE ""OrderItems"" 
                SET ""OrderId"" = @OrderId, ""ContactLensTypeId"" = @ContactLensTypeId, ""OrderQuantity"" = @OrderQuantity, ""Price"" = @Price
                WHERE ""Id"" = @Id
                RETURNING *";
            return await connection.QuerySingleAsync<OrderItem>(query, orderItem);
        }
    }
}
