using Application.Addresses.Commands.DeleteAddress;
using Application.Addresses.Commands.UpdateAddress;
using MediatR;

namespace Currency.API.Endpoints;

public static class AddressEndpoints
{
    public static void MapAddressEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/address")
                       .WithTags("Address");

        group.MapPut("/{id:int}", async (int id, UpdateAddressCommand command, ISender sender) =>
        {
            command.Id = id;

            var result = await sender.Send(command);
            if (result.IsSuccessful)
                return Results.Created($"/users/{result.Value.UserId}", result);

            return Results.BadRequest(result);
        });

        group.MapDelete("/{id:int}", async (int id, ISender sender) =>
        {
            var query = new DeleteAddressCommand()
            {
                Id = id
            };

            var result = await sender.Send(query);

            return result.IsSuccessful ? Results.Ok(result) : Results.InternalServerError(result);

        });
    }
}
