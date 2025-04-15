using BackendApplication.Schema.Types;

namespace BackendApplication.Services.Abstractions
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> GetAllAsync();
        Task<OrderItem?> GetByIdAsync(Guid id);
        Task<OrderItem> AddAsync(OrderItem orderItem);
        Task<OrderItem> UpdateAsync(OrderItem orderItem);
        Task<bool> DeleteAsync(Guid id);
    }
}
