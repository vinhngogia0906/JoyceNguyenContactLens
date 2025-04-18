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
        public async Task<bool> DeleteContactLens(
            [Service] IContactLensRepository contactLensRepository,
            Guid id)
        {
            return await contactLensRepository.DeleteAsync(id);
        }
        public async Task<bool> DeleteUser(
            [Service] IUserRepository userRepository,
            Guid id)
        {
            return await userRepository.DeleteAsync(id);
        }

        public async Task<User> UpdateUser(
            [Service] IUserRepository userRepository,
            User user)
        {
            return await userRepository.UpdateAsync(user);
        }
        public async Task<ContactLensType> UpdateContactLens(
            [Service] IContactLensRepository contactLensRepository,
            ContactLensType contactLens)
        {
            return await contactLensRepository.UpdateAsync(contactLens);
        }

        public async Task<Order> SubmitOrder(
            [Service] IOrderRepository orderRepository,
            Order order)
        {
            return await orderRepository.AddAsync(order);
        }
        public async Task<bool> DeleteOrder(
            [Service] IOrderRepository orderRepository,
            Guid id)
        {
            return await orderRepository.DeleteAsync(id);
        }
        public async Task<Order> UpdateOrder(
            [Service] IOrderRepository orderRepository,
            Order order)
        {
            return await orderRepository.UpdateAsync(order);
        }
        public async Task<Address> AddAddress(
            [Service] IAddressRepository addressRepository,
            Address address)
        {
            return await addressRepository.AddAsync(address);
        }
        public async Task<bool> DeleteAddress(
            [Service] IAddressRepository addressRepository,
            Guid id)
        {
            return await addressRepository.DeleteAsync(id);
        }

        public async Task<Address> UpdateAddress(
            [Service] IAddressRepository addressRepository,
            Address address)
        {
            return await addressRepository.UpdateAsync(address);
        }

    }
}
