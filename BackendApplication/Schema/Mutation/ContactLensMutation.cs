using BackendApplication.Helper;
using BackendApplication.Schema.Types;
using BackendApplication.Services.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace BackendApplication.Schema.Mutation
{
    public class ContactLensMutation
    {
        private readonly TokenGenerator _tokenGenerator;
        public async Task<ContactLensType> AddContactLens(
            [Service] IContactLensRepository contactLensRepository,
            ContactLensRequest contactLensRequest)
        {
            var newContactLens = await contactLensRepository.AddAsync(contactLensRequest);
            return newContactLens;
        }

        public async Task<User> Register(
            [Service] IUserRepository userRepository,
            UserRegisterRequest userRegisterRequest)
        {
            var newUser = await userRepository.AddAsync(userRegisterRequest);
            return newUser;
        }

        public async Task<string> Login(
            [Service] IUserRepository userRepository,
            string email,
            string password)
        {
            return await userRepository.Login(email, password);

        }

    }
}
