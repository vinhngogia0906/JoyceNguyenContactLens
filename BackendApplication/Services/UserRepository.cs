using BackendApplication.Helper;
using BackendApplication.Schema.Types;
using BackendApplication.Services.Abstractions;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Crypto.Generators;

namespace BackendApplication.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        private readonly TokenGenerator _tokenGenerator;
        public UserRepository(IConfiguration configuration, TokenGenerator tokenGenerator)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _tokenGenerator = tokenGenerator;
        }
        public async Task<User> AddAsync(UserRegisterRequest userRegisterRequest)
        {
            var newId = Guid.NewGuid();
            var passwordHash = new PasswordHasher<User>().HashPassword(null, userRegisterRequest.Password);
            var parameters = new
            {
                Id = newId,
                FirstName = userRegisterRequest.FirstName,
                LastName = userRegisterRequest.LastName,
                Email = userRegisterRequest.Email,
                IsAdmin = false,
                PasswordHash = passwordHash,
                Addresses = new List<Address>(),
                Orders = new List<Order>()
            };
            using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            const string query = @"
                INSERT INTO ""Users"" (""Id"", ""FirstName"", ""LastName"", ""Email"", ""PasswordHash"", ""IsAdmin"") 
                VALUES (@Id, @FirstName, @LastName, @Email, @PasswordHash, @IsAdmin)
                RETURNING *";
            return await connection.QuerySingleAsync<User>(query, parameters);
        }
        public async Task<string> Login(string email, string password)
        {
            var user = await GetByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("Invalid email or password");
            }
            else if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid email or password");
            }
            else
            {
                return _tokenGenerator.GenerateToken(email, new PasswordHasher<User>().HashPassword(null, password));
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            const string query = @"DELETE FROM ""Users"" WHERE ""Id"" = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
           using var connection = new Npgsql.NpgsqlConnection(_connectionString);
           const string query = @"SELECT * FROM ""Users""";
           return await connection.QueryAsync<User>(query);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            const string query = @"SELECT * FROM ""Users"" WHERE ""Id"" = @Id";
            return await connection.QuerySingleOrDefaultAsync<User>(query, new { Id = id });
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            const string query = @"SELECT * FROM ""Users"" WHERE ""Email"" = @Email";
            return await connection.QuerySingleOrDefaultAsync<User>(query, new { Email = email });
        }

        public async Task<User> UpdateAsync(User user)
        {
            using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            const string query = @"
                UPDATE ""Users"" 
                SET ""FirstName"" = @FirstName, ""LastName"" = @LastName, ""Email"" = @Email, ""PasswordHash"" = @PasswordHash
                WHERE ""Id"" = @Id
                RETURNING *";
            return await connection.QuerySingleAsync<User>(query, user);
        }
    }
}
