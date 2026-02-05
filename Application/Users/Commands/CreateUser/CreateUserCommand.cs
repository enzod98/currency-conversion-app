using Domain.Users;
using FluentValidation;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommand : ICommand<UserResponse>
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public string? Password { get; init; }
}

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    public CreateUserCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es requerido")
            .EmailAddress().WithMessage("Formato de email inválido")
            .MustAsync(async (email, cancellation) =>
            {
                var exists = await _userRepository.ExistsByEmailAsync(email);
                return !exists;
            })
            .WithMessage("Este correo ya está registrado en el sistema.");

    }
}