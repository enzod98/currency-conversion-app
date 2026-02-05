
using Domain.Addresses;

namespace Application.Addresses.Commands.DeleteAddress;

public class DeleteAddressCommandhandler : ICommandHandler<DeleteAddressCommand>
{
    private readonly IAddressRepository _addressRepository;
    public DeleteAddressCommandhandler(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<Result> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        var deleted = await _addressRepository.DeleteAddressAsync(request.Id);
        if (deleted)
            return Result.Ok();

        return Result.Fail("Fallo al eliminar la dirección.");
    }
}
