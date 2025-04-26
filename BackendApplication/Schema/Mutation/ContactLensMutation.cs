using BackendApplication.Helper;
using BackendApplication.Schema.Types;
using BackendApplication.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Path = System.IO.Path;

namespace BackendApplication.Schema.Mutation
{
    public class ContactLensMutation
    {
        private readonly TokenGenerator _tokenGenerator;
        private readonly IConfiguration _configuration;
        private string DEFAULT_IMAGE_PATH = "D:/JoyceNguyenContactLens/imageUploads";
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

        public async Task<string> AdminLogin(
            [Service] IUserRepository userRepository,
            string email,
            string password)
        {
            return await userRepository.AdminLogin(email, password);

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
            Guid id,
            ContactLensRequest contactLensRequest)
        {
            try
            {
                var contactLens = await contactLensRepository.GetByIdAsync(id);
                if (contactLens == null)
                {
                    throw new Exception("Contact lens not found");
                }
                contactLens.Name = contactLensRequest.Name;
                contactLens.Color = contactLensRequest.Color;
                contactLens.Degree = contactLensRequest.Degree;
                contactLens.Price = contactLensRequest.Price;
                contactLens.Quantity = contactLensRequest.Quantity;
                return await contactLensRepository.UpdateAsync(contactLens);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating contact lens: {ex.Message}");
            }
            
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

        public async Task<bool> AddContactLensImage(
            [Service] IContactLensRepository contactLensRepository,
            Guid contactLensId,
            IFile imageFile)
        {
            var filePath = Path.Combine(_configuration.GetValue<string>("UploadPath") ?? DEFAULT_IMAGE_PATH, $"{contactLensId}_{imageFile.Name}");
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            var imageUrl = filePath.Replace("D:/JoyceNguyenContactLens", "").Replace("\\", "/");
            return await contactLensRepository.AddImageAsync(contactLensId, imageUrl);
        }

        public async Task<bool> ReplaceContactLensImages(
            [Service] IContactLensRepository contactLensRepository,
            Guid contactLensId,
            IEnumerable<IFile> imageFiles)
        {
            var imageUrls = new List<string>();
            foreach (var imageFile in imageFiles)
            {
                var filePath = Path.Combine(_configuration.GetValue<string>("UploadPath") ?? DEFAULT_IMAGE_PATH, $"{Guid.NewGuid()}_{imageFile.Name}");
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                imageUrls.Add(filePath.Replace("D:/JoyceNguyenContactLens", "").Replace("\\", "/"));
            }

            return await contactLensRepository.ReplaceImagesAsync(contactLensId, imageUrls);
        }

        public async Task<bool> RemoveContactLensImage(
            [Service] IContactLensRepository contactLensRepository,
            Guid contactLensId,
            string imageUrl)
        {
            return await contactLensRepository.RemoveImageAsync(contactLensId, imageUrl);
        }

    }
}
