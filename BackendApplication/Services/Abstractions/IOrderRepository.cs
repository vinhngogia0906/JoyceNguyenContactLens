using BackendApplication.Schema.Types;

namespace BackendApplication.Services.Abstractions
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(Guid id);
        Task<Order> AddAsync(Order order);
        Task<Order> UpdateAsync(Order order);
        Task<bool> DeleteAsync(Guid id);
    }
}
