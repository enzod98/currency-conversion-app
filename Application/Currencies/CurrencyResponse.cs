namespace Application.Currencies;

public record struct CurrencyResponse
(
    int Id,
    string Code,
    string Name,
    decimal RateToBase
);