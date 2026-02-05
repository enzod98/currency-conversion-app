

using Domain.Addresses;

namespace Application.Addresses.Queries.GetAddressesByUserId;

public class GetAddressesByUserIdQueryHandler : IQueryHandler<GetAddressesByUserIdQuery, List<AddressResponse>>
{
    private readonly IAddressRepository _addressRepository;
    public GetAddressesByUserIdQueryHandler(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<Result<List<AddressResponse>>> Handle(GetAddressesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var addresses = await _addressRepository.GetAddressesByUserIdAsync(request.UserId);
        var response = new List<AddressResponse>();
        foreach (var address in addresses)
            response.Add(address.Adapt<AddressResponse>());

        return response;
    }
}
