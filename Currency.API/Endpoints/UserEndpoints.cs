using Application.Users.Commands.CreateUser;
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
    }
}
