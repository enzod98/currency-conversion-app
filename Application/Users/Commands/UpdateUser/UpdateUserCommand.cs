
namespace Application.Users.Commands.UpdateUser;

public class UpdateUserCommand : ICommand<UserResponse>
{
    public required int Id { get; set; }
    public required string? Name { get; init; }
    public required string? Email { get; init; }
    public bool? IsActive { get; init; }

}
