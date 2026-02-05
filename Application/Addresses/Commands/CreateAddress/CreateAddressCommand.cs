
using Application.Users.Commands.CreateUser;
using Domain.Users;
using FluentValidation;

namespace Application.Addresses.Commands.CreateAddress;

public class CreateAddressCommand : ICommand<AddressResponse>
{
    public int UserId { get; set; }
    public required string Street { get; init; }
    public required string City { get; init; }
    public required string Country { get; init; }
    public string? ZipCode { get; init; }
}

public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidator()
    {

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("El campo Street es requerido");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("El campo City es requerido");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("El campo Country es requerido");


    }
}