using BackendApplication.Schema.Types;
using BackendApplication.Services.Abstractions;
using Dapper;
using Npgsql;
using System.Runtime.InteropServices;

namespace BackendApplication.Services
{
    public class AddressRepository : IAddressRepository
    {
        private readonly string _connectionString;
        public AddressRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<Address> AddAsync(Address address)
        {
            var newId = Guid.NewGuid();
            var parameters = new
            {
                Id = newId,
                Street1 = address.Street1,
                Street2 = address.Street2,
                Suburb = address.Suburb,
                State = address.State,
                Postcode = address.Postcode,
                UserId = address.UserId,
                PhoneNumber = address.PhoneNumber
            };
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"
                INSERT INTO ""Addresses"" (""Id"", ""Street1"", ""Street2"", ""Suburb"", ""State"", ""Postcode"", ""UserId"", ""PhoneNumber"") 
                VALUES (@Id, @Street1, @Street2, @Suburb, @State, @Postcode, @UserId, @PhoneNumber)
                RETURNING *";
            return await connection.QuerySingleAsync<Address>(query, parameters);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"DELETE FROM ""Addresses"" WHERE ""Id"" = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"SELECT * FROM ""Addresses""";
            return await connection.QueryAsync<Address>(query);
        }

        public async Task<Address?> GetByIdAsync(Guid id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"SELECT * FROM ""Addresses"" WHERE ""Id"" = @Id";
            return await connection.QuerySingleOrDefaultAsync<Address>(query, new { Id = id });
        }

        public async Task<Address> UpdateAsync(Address address)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"
                UPDATE ""Addresses"" 
                SET ""Street1"" = @Street1, 
                    ""Street2"" = @Street2, 
                    ""Suburb"" = @Suburb, 
                    ""State"" = @State, 
                    ""Postcode"" = @Postcode, 
                    ""UserId"" = @UserId, 
                    ""PhoneNumber"" = @PhoneNumber
                WHERE ""Id"" = @Id
                RETURNING *";
            return await connection.QuerySingleAsync<Address>(query, address);
        }
    }
}
