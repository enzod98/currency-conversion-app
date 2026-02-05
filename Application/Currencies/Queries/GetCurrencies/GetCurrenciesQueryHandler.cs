using Application.Users;
using Domain.Currencies;
using Domain.Users;

namespace Application.Currencies.Queries.GetCurrencies;

public class GetCurrenciesQueryHandler : IQueryHandler<GetCurrenciesQuery, List<CurrencyResponse>>
{
    private readonly ICurrencyRepository _currencyRepository;  
    public GetCurrenciesQueryHandler(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public async Task<Result<List<CurrencyResponse>>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var currencies = await _currencyRepository.GetAllAsync();
        var response = new List<CurrencyResponse>();
        foreach (var currency in currencies)
            response.Add(currency.Adapt<CurrencyResponse>());

        return response;
    }
}
