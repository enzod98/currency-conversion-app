namespace Application.Users;

public record struct UserResponse
(
    int Id,
    string Name,
    string Email,
    bool IsActive
);
