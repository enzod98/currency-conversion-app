using Domain.Currencies;

namespace Application.Currencies.Commands.CreateCurrency;

public class CreateCurrencyCommandHandler : ICommandHandler<CreateCurrencyCommand, CurrencyResponse>
{
    private readonly ICurrencyRepository _currencyRepository;
    public CreateCurrencyCommandHandler(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public async Task<Result<CurrencyResponse>> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
    {
        var newCurrency = request.Adapt<Currency>();
        var currency = await _currencyRepository.CreateCurrencyAsync(newCurrency);
        return currency.Adapt<CurrencyResponse>();
    }
}
