namespace Domain.Currencies;

public interface ICurrencyRepository
{
    Task<List<Currency>> GetAllAsync();
    Task<Currency> CreateCurrencyAsync(Currency currency);
    Task<bool> ExistsByCodeAsync(string code);


}
