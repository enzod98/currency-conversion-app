
namespace Application.Users.Queries.GetUserById;

public class GetUserByIdQuery : IQuery<UserResponse?>
{
    public required int Id { get; set; }
}
