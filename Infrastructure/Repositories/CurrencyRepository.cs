using Domain.Currencies;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly ApplicationDbContext _context;
    public CurrencyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Currency> CreateCurrencyAsync(Currency currency)
    {
        _context.Currencies.Add(currency);
        await _context.SaveChangesAsync();
        return currency;
    }

    public async Task<bool> ExistsByCodeAsync(string code)
    {
        return _context.Currencies.Where(c => c.Code.Equals(code)).FirstOrDefault() is not null;
    }

    public async Task<List<Currency>> GetAllAsync()
    {
        return await _context.Currencies.ToListAsync();
    }

    public async Task<Currency?> GetCurrencyByCodeAsync(string code)
    {
        return _context.Currencies.Where(c => c.Code.Equals(code)).FirstOrDefault();
    }
}
