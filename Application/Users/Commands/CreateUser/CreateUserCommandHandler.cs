using Domain.Users;
using System.Security.Cryptography;
using System.Text;

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
        var inputBytes = Encoding.UTF8.GetBytes(request.Password);
        var hashBytes = SHA256.HashData(inputBytes);

        var newUser = request.Adapt<User>();
        newUser.Password = Convert.ToHexString(hashBytes);
        var user = await _userRepository.CreateUserAsync(newUser);
        return user.Adapt<UserResponse>();
    }
}
