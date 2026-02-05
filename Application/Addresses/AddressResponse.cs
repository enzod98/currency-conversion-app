using Domain.Users;

namespace Application.Addresses;

public record struct AddressResponse
(
    int Id,
    int UserId,
    string Street,
    string City,
    string Country,
    string? ZipCode
);


