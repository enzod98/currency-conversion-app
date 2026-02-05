
using Domain.Users;

namespace Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var deleted = await _userRepository.DeleteUserAsync(request.Id);
        if(deleted)
            return Result.Ok();

        return Result.Fail("Fallo al eliminar el usuario.");
    }
}
