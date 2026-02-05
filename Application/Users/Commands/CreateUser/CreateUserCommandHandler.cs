using Application.Abstractions.RequestHandling;
using Domain.Abstractions;
using Domain.Users;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserResponse>
{
    private readonly IUserRepository _userRepository;
    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = request.Adapt<User>();
        var user = await _userRepository.CreateUserAsync(newUser);
        return user.Adapt<UserResponse>();
    }
}
