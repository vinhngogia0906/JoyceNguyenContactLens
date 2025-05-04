using BackendApplication.Schema.Types;
using BackendApplication.Services.Abstractions;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace BackendApplication.Services
{
    public class ContactLensRepository : IContactLensRepository
    {
        private readonly string _connectionString;
        private readonly string _uploadPath;
        public ContactLensRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _uploadPath = configuration.GetValue<string>("UploadPath");
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
                Quantity = contactLensRequest.Quantity,
                ImageUrls = new string[0]
            };

            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"
                INSERT INTO ""ContactLenses"" (""Id"", ""Name"", ""Color"", ""Degree"", ""Price"", ""Quantity"", ""ImageUrls"") 
                VALUES (@Id, @Name, @Color, @Degree, @Price, @Quantity, @ImageUrls)
                RETURNING *";
            return await connection.QuerySingleAsync<ContactLensType>(query, parameters);

        }
        public async Task<Boolean> DeleteAsync(Guid id)
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

        public async Task<IEnumerable<ContactLensType>> GetByColorAsync(string color)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"SELECT * FROM ""ContactLenses"" WHERE ""Color"" = @Color";
            return await connection.QueryAsync<ContactLensType>(query);
        }
        public async Task<ContactLensType?> GetByIdAsync(Guid id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"SELECT * FROM ""ContactLenses"" WHERE ""Id"" = @Id";
            return await connection.QuerySingleOrDefaultAsync<ContactLensType>(query, new { Id = id });
        }
        public async Task<ContactLensType> UpdateAsync(ContactLensType contactLens)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"
                UPDATE ""ContactLenses"" 
                SET ""Name"" = @Name, ""Color"" = @Color, ""Degree"" = @Degree, ""Price"" = @Price, ""Quantity"" = @Quantity, ""ImageUrls"" = @ImageUrls
                WHERE ""Id"" = @Id
                RETURNING *";
            return await connection.QuerySingleAsync<ContactLensType>(query, contactLens);

        }

        public async Task<bool> AddImageAsync(Guid contactLensId, IFile imageFile)
        {
            var filePath = System.IO.Path.Combine(_uploadPath, $"{contactLensId}_{imageFile.Name}");
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            var imageUrl = filePath.Replace("D:/JoyceNguyenContactLens", "").Replace("\\", "/");

            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"
        UPDATE ""ContactLenses""
        SET ""ImageUrls"" = array_append(""ImageUrls"", @ImageUrl)
        WHERE ""Id"" = @ContactLensId";
            var rowsAffected = await connection.ExecuteAsync(query, new { ContactLensId = contactLensId, ImageUrl = imageUrl });
            return rowsAffected > 0;
        }

        public async Task<bool> ReplaceImagesAsync(Guid contactLensId, IEnumerable<string> imageUrls)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"
        UPDATE ""ContactLenses""
        SET ""ImageUrls"" = @ImageUrls
        WHERE ""Id"" = @ContactLensId";
            var rowsAffected = await connection.ExecuteAsync(query, new { ContactLensId = contactLensId, ImageUrls = imageUrls.ToArray() });
            return rowsAffected > 0;
        }

        public async Task<bool> RemoveImageAsync(Guid contactLensId, string imageUrl)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string query = @"
        UPDATE ""ContactLenses""
        SET ""ImageUrls"" = array_remove(""ImageUrls"", @ImageUrl)
        WHERE ""Id"" = @ContactLensId";
            var rowsAffected = await connection.ExecuteAsync(query, new { ContactLensId = contactLensId, ImageUrl = imageUrl });
            return rowsAffected > 0;
        }
    }
}
