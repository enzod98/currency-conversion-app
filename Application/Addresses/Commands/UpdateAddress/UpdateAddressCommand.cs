namespace Application.Addresses.Commands.UpdateAddress;

public class UpdateAddressCommand : ICommand<AddressResponse>
{
    public int Id { get; set; }
    public string? Street { get; init; }
    public string? City { get; init; }
    public string? Country { get; init; }
    public string? ZipCode { get; init; }
}
