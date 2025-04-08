using BackendApplication.Schema.Types;
using Dapper;
using Npgsql;

namespace BackendApplication.Services
{
    public class ContactLensRepository : IContactLensRepository
    {
        private readonly string _connectionString;
        public ContactLensRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<ContactLensType> AddAsync(ContactLensRequest contactLensRequest)
        {
            var newId = Guid.NewGuid();

            var parameters = new
            {
                Id = newId,
                Name = contactLensRequest.Name,
                Color = contactLensRequest.Color,
                Degree = contactLensRequest.Degree,
                Price = contactLensRequest.Price,
                Quantity = contactLensRequest.Quantity
            };

            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"
                INSERT INTO ""ContactLenses"" (""Id"", ""Name"", ""Color"", ""Degree"", ""Price"", ""Quantity"") 
                VALUES (@Id, @Name, @Color, @Degree, @Price, @Quantity)
                RETURNING *";
            return await connection.QuerySingleAsync<ContactLensType>(query, parameters);

        }
        public async Task<Boolean> DeleteAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = "DELETE FROM ContactLenses WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
        public async Task<IEnumerable<ContactLensType>> GetAllAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"SELECT * FROM ""ContactLenses""";
            return await connection.QueryAsync<ContactLensType>(query);
        }
        public async Task<ContactLensType?> GetByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = "SELECT * FROM ContactLenses WHERE Id = @Id";
            return await connection.QuerySingleOrDefaultAsync<ContactLensType>(query, new { Id = id });
        }
        public async Task<ContactLensType> UpdateAsync(ContactLensType contactLens)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"
                UPDATE ContactLenses 
                SET Name = @Name, Color = @Color, Degree = @Degree, Price = @Price, Quantity = @Quantity
                WHERE Id = @Id
                RETURNING *";
            return await connection.QuerySingleAsync<ContactLensType>(query, contactLens);

        }
    }
}
