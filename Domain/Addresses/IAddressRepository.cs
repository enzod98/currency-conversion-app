namespace Domain.Addresses;

public interface IAddressRepository
{
    Task<Address?> GetAddressByIdAsync(int id);
    Task<Address> CreateAddressAsync(Address address);
    Task<List<Address?>> GetAddressesByUserIdAsync(int userId);
    Task<Address?> UpdateAddressAsync(Address address);
    Task<bool> DeleteAddressAsync(int id);
}
