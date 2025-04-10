using BackendApplication.Schema.Types;

namespace BackendApplication.Services.Abstractions
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<User> AddAsync(UserRegisterRequest userRegisterRequest);
        Task<User> UpdateAsync(User user);
        Task<bool> DeleteAsync(Guid id);
        Task<string> Login(string email, string password);
    }
}
