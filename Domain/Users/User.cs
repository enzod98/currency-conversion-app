using Domain.Abstractions;
using Domain.Addresses;

namespace Domain.Users;

public class User : Entity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Password { get; set; }
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
}
