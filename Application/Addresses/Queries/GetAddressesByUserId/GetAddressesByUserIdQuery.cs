namespace Application.Addresses.Queries.GetAddressesByUserId;

public class GetAddressesByUserIdQuery : IQuery<List<AddressResponse>>
{
    public required int UserId { get; set; }

}
