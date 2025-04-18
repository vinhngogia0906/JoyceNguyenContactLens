using BackendApplication.Schema.Types;
using BackendApplication.Services.Abstractions;

namespace BackendApplication.Schema.Query
{
    public class ContactLensQuery
    {
        public async Task<IEnumerable<ContactLensType>> GetAllContactLenses(
            [Service] IContactLensRepository contactLensRepository)
        {
            return await contactLensRepository.GetAllAsync();
        }

        public async Task<ContactLensType?> GetContactLensById(
            [Service] IContactLensRepository contactLensRepository,
            Guid id)
        {
            return await contactLensRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ContactLensType>> GetContactLensesByColor(
            [Service] IContactLensRepository contactLensRepository,
            string color)
        {
            var allContactLenses = await contactLensRepository.GetByColorAsync(color);
            return allContactLenses.Where(cl => cl.Color.Equals(color, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<IEnumerable<Address?>> GetAllAddresses(
            [Service] IAddressRepository addressRepository)
        {
            return await addressRepository.GetAllAsync();
        }
        public async Task<Address?> GetAddressById(
            [Service] IAddressRepository addressRepository,
            Guid id)
        {
            return await addressRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Address?>> GetAddressByUserId(
            [Service] IAddressRepository addressRepository,
            Guid userId)
        {
            return await addressRepository.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<User?>> GetAllUsers(
            [Service] IUserRepository userRepository)
        {
            return await userRepository.GetAllAsync();
        }

        public async Task<User?> GetUserById(
            [Service] IUserRepository userRepository,
            Guid id)
        {
            return await userRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Order?>> GetAllOrdersAsync(
            [Service] IOrderRepository orderRepository)
        {
            return await orderRepository.GetAllAsync();
        }
        public async Task<Order?> GetOrderByIdAsync(
            [Service] IOrderRepository orderRepository,
            Guid id)
        {
            return await orderRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Order?>> GetOrdersByUserIdAsync(
            [Service] IOrderRepository orderRepository,
            Guid userId)
        {
            return await orderRepository.GetByUserIdAsync(userId);
        }
    }
}
