using Domain.Currencies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CurrencyConversion.Commands;

public class ConversionCommandHandler : ICommandHandler<ConversionCommand, CurrencyConversionResponse>
{
    private readonly ICurrencyRepository _currencyRepository;
    public ConversionCommandHandler(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public async Task<Result<CurrencyConversionResponse>> Handle(ConversionCommand request, CancellationToken cancellationToken)
    {
        var fromCurrency = await _currencyRepository.GetCurrencyByCodeAsync(request.FromCurrencyCode);
        var toCurrency = await _currencyRepository.GetCurrencyByCodeAsync(request.ToCurrencyCode);

        var montoBase = request.Amount * fromCurrency.RateToBase;
        var convertedAmount = montoBase / toCurrency.RateToBase;

        return new CurrencyConversionResponse()
        {
            FromCurrency = fromCurrency.Code,
            ToCurrency = toCurrency.Code,
            OriginalAmount = request.Amount,
            ConvertedAmount = convertedAmount
        };
    }
}
