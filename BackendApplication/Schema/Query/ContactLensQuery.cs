using BackendApplication.Schema.Types;
using BackendApplication.Services;

namespace BackendApplication.Schema.Query
{
    public class ContactLensQuery
    {
        public async Task<IEnumerable<ContactLensType>> GetAllContactLenses(
            [Service] IContactLensRepository contactLensRepository)
        {
            return await contactLensRepository.GetAllAsync();
        }
    }
}
