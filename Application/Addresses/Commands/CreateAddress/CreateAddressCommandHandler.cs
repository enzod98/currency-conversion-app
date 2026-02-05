
using Domain.Addresses;
using Domain.Users;

namespace Application.Addresses.Commands.CreateAddress;

public class CreateAddressCommandHandler : ICommandHandler<CreateAddressCommand, AddressResponse>
{
    private readonly IAddressRepository _addressRepository;
    private readonly IUserRepository _userRepository;
    public CreateAddressCommandHandler(IAddressRepository addressRepository, IUserRepository userRepository)
    {
        _addressRepository = addressRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<AddressResponse>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(request.UserId);
        if(user is null)
            return Result.Fail<AddressResponse>("El usuario indicado no existe.");

        var address = request.Adapt<Address>();
        var addressDb = await _addressRepository.CreateAddressAsync(address);
        return addressDb.Adapt<AddressResponse>();

    }
}
