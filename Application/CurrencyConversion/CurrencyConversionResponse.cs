using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CurrencyConversion;

public record struct CurrencyConversionResponse
(
    string FromCurrency,
    string ToCurrency,
    decimal OriginalAmount,
    decimal ConvertedAmount
);