﻿using BackendApplication.Schema.Types;

namespace BackendApplication.Services.Abstractions
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAllAsync();
        Task<Address?> GetByIdAsync(Guid id);
        Task<IEnumerable<Address>> GetByUserIdAsync(Guid userId);
        Task<Address> AddAsync(Address address);
        Task<Address> UpdateAsync(Address address);
        Task<bool> DeleteAsync(Guid id);
    }
}
