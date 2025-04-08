using BackendApplication.Schema.Types;

namespace BackendApplication.Services
{
    public interface IContactLensRepository
    {
        Task<IEnumerable<ContactLensType>> GetAllAsync();
        Task<ContactLensType?> GetByIdAsync(int id);
        Task<ContactLensType> AddAsync(ContactLensRequest contactLensRequest);
        Task<ContactLensType> UpdateAsync(ContactLensType contactLens);
        Task<Boolean> DeleteAsync(int id);
    }
}
