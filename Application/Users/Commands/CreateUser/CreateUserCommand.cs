namespace Application.Users.Commands.CreateUser;

public class CreateUserCommand : ICommand<UserResponse>
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public string? Password { get; init; }
}
