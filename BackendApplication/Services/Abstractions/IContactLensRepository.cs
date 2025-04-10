using BackendApplication.Schema.Types;

namespace BackendApplication.Services.Abstractions
{
    public interface IContactLensRepository
    {
        Task<IEnumerable<ContactLensType>> GetAllAsync();
        Task<ContactLensType?> GetByIdAsync(Guid id);
        Task<ContactLensType> AddAsync(ContactLensRequest contactLensRequest);
        Task<ContactLensType> UpdateAsync(ContactLensType contactLens);
        Task<bool> DeleteAsync(Guid id);
    }
}
