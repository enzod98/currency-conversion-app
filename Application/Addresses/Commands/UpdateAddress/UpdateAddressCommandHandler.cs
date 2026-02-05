
using Application.Users;
using Domain.Addresses;
using Domain.Users;

namespace Application.Addresses.Commands.UpdateAddress;

public class UpdateAddressCommandHandler : ICommandHandler<UpdateAddressCommand, AddressResponse>
{
    private readonly IAddressRepository _addressRepository;
    public UpdateAddressCommandHandler(IAddressRepository addressRepository)    
    {
        _addressRepository = addressRepository;
    }
    public async Task<Result<AddressResponse>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var addressDb = await _addressRepository.GetAddressByIdAsync(request.Id);

        if (addressDb is null)
            return Result.Fail<AddressResponse>("El id especificado no existe");

        addressDb.Street = request.Street ?? addressDb.Street;
        addressDb.City = request.City ?? addressDb.City;
        addressDb.Country = request.Country ?? addressDb.Country;
        addressDb.ZipCode = request.ZipCode ?? addressDb.ZipCode;

        var result = await _addressRepository.UpdateAddressAsync(addressDb);
        return result.Adapt<AddressResponse>();
    }
}
