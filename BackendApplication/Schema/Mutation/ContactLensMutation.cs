using BackendApplication.Schema.Types;
using BackendApplication.Services;

namespace BackendApplication.Schema.Mutation
{
    public class ContactLensMutation
    {
        public async Task<ContactLensType> AddContactLens(
            [Service] IContactLensRepository contactLensRepository,
            ContactLensRequest contactLensRequest)
        {
            var newContactLens = await contactLensRepository.AddAsync(contactLensRequest);
            return newContactLens;
        }
    }
}
