using Domain.Addresses;
using Domain.Users;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly ApplicationDbContext _context;
    public AddressRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Address> CreateAddressAsync(Address address)
    {
        await _context.Adresses.AddAsync(address);
        await _context.SaveChangesAsync();
        return address;
    }

    public async Task<bool> DeleteAddressAsync(int id)
    {
        var address = await _context.Adresses.FindAsync(id);
        if (address is null)
            return false;

        _context.Adresses.Remove(address);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Address?> GetAddressByIdAsync(int id)
    {
        var address = await _context.Adresses.FindAsync(id);
        return address;
    }

    public async Task<List<Address?>> GetAddressesByUserIdAsync(int userId)
    {
        var addresses = await _context.Adresses.Where(a => a.UserId.Equals(userId)).ToListAsync();
        return addresses;
    }

    public async Task<Address?> UpdateAddressAsync(Address address)
    {
        _context.Update(address);
        await _context.SaveChangesAsync();
        return address;
    }
}
