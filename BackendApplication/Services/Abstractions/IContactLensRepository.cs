using BackendApplication.Schema.Types;

namespace BackendApplication.Services.Abstractions
{
    public interface IContactLensRepository
    {
        Task<IEnumerable<ContactLensType>> GetAllAsync();
        Task<IEnumerable<ContactLensType>> GetByColorAsync(string color);
        Task<ContactLensType?> GetByIdAsync(Guid id);
        Task<ContactLensType> AddAsync(ContactLensRequest contactLensRequest);
        Task<ContactLensType> UpdateAsync(ContactLensType contactLens);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> AddImageAsync(Guid contactLensId, IFile imageFile);
        Task<bool> ReplaceImagesAsync(Guid contactLensId, IEnumerable<string> imageUrls);
        Task<bool> RemoveImageAsync(Guid contactLensId, string imageUrl);

    }
}
