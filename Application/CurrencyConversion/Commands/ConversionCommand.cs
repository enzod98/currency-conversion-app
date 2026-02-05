
using Domain.Currencies;
using FluentValidation;

namespace Application.CurrencyConversion.Commands;

public class ConversionCommand : ICommand<CurrencyConversionResponse>
{
    public required string FromCurrencyCode { get; set; }
    public required string ToCurrencyCode { get; set; }
    public required decimal Amount { get; set; }
}


public class ConversionCommandValidator : AbstractValidator<ConversionCommand>
{
    private readonly ICurrencyRepository _currencyRepository;
    public ConversionCommandValidator(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;


        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("El campo del monto debe ser mayor a 0");

        RuleFor(x => x.FromCurrencyCode)
            .MustAsync(async (code, cancellation) =>
            {
                var exists = await _currencyRepository.ExistsByCodeAsync(code);
                return exists;
            })
            .WithMessage("El código de origen no esta registrado en el sistema.");

        RuleFor(x => x.ToCurrencyCode)
            .MustAsync(async (code, cancellation) =>
            {
                var exists = await _currencyRepository.ExistsByCodeAsync(code);
                return exists;
            })
            .WithMessage("El código de destino no esta registrado en el sistema.");

    }
}