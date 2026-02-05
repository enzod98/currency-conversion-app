using Application.Addresses.Commands.CreateAddress;
using Application.Addresses.Queries.GetAddressesByUserId;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.Queries.GetUserById;
using Application.Users.Queries.GetUsers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Currency.API.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/users")
                       .WithTags("Users");

        group.MapGet("/", async (ISender sender) =>
        {
            var query = new GetUsersQuery();

            var result = await sender.Send(query);

            return result;
        });

        group.MapGet("/{id:int}", async (int id, ISender sender) =>
        {
            var query = new GetUserByIdQuery()
            {
                Id = id
            };

            var result = await sender.Send(query);

            return result.Value is null ? Results.NotFound(result) : Results.Ok(result);

        });

        group.MapDelete("/{id:int}", async (int id, ISender sender) =>
        {
            var query = new DeleteUserCommand()
            {
                Id = id
            };

            var result = await sender.Send(query);

            return result.IsSuccessful ? Results.Ok(result) : Results.InternalServerError(result) ;

        });


        group.MapPost("/", async (CreateUserCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            if (result.IsSuccessful)
                return Results.Created($"/users/{result.Value.Id}", result);

            return Results.BadRequest(result);
        });

        group.MapPut("/{id:int}", async (int id, UpdateUserCommand command, ISender sender) =>
        {
            command.Id = id;

            var result = await sender.Send(command);
            if (result.IsSuccessful)
                return Results.Created($"/users/{result.Value.Id}", result);

            return Results.BadRequest(result);
        });


        group.MapGet("/{id:int}/addresses", async (int id, ISender sender) =>
        {
            var query = new GetAddressesByUserIdQuery()
            {
                UserId = id
            };

            var result = await sender.Send(query);

            return result.Value is null ? Results.NotFound(result) : Results.Ok(result);

        });

        group.MapPost("/{id:int}/addresses", async (int id, CreateAddressCommand command, ISender sender) =>
        {
            command.UserId = id;

            var result = await sender.Send(command);
            if (result.IsSuccessful)
                return Results.Created($"/users/{result.Value.UserId}/addresses", result);

            return Results.NotFound(result);
        });

    }
}
