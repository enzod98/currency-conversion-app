using Application.Users.Commands.CreateUser;
using Domain.Currencies;
using Domain.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Currencies.Commands.CreateCurrency;

public class CreateCurrencyCommand : ICommand<CurrencyResponse>
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public decimal RateToBase { get; set; }
}


public class CreateCurrencyCommandValidator : AbstractValidator<CreateCurrencyCommand>
{
    private readonly ICurrencyRepository _currencyRepository;
    public CreateCurrencyCommandValidator(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name es requerido");

        RuleFor(x => x.RateToBase)
            .GreaterThan(0).WithMessage("El campo Rate to Base debe ser mayor a 0");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El campo Code es requerido")
            .MustAsync(async (code, cancellation) =>
            {
                var exists = await _currencyRepository.ExistsByCodeAsync(code);
                return !exists;
            })
            .WithMessage("Este código ya está registrado en el sistema.");

    }
}