using Domain.Abstractions;
using Domain.Users;

namespace Domain.Addresses;

public class Address : Entity
{
    public required int UserId { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public string? ZipCode { get; set; }
    public User User { get; set; } = null!;
}
