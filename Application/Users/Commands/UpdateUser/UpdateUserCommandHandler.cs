

using Domain.Users;

namespace Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, UserResponse>
{
    private readonly IUserRepository _userRepository;
    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userDb = await _userRepository.GetUserByIdAsync(request.Id);
        if (userDb is null)
            return Result.Fail<UserResponse>("El usuario indicado no existe");

        userDb.Name = request.Name ?? userDb.Name;
        userDb.Email = request.Email ?? userDb.Email;
        userDb.IsActive = request.IsActive ?? userDb.IsActive;

        var result = await _userRepository.UpdateUserAsync(userDb);
        return result.Adapt<UserResponse>();
    }
}
