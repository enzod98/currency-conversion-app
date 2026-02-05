using Application.Currencies.Commands.CreateCurrency;
using Application.Currencies.Queries.GetCurrencies;
using MediatR;

namespace Currency.API.Endpoints;

public static class CurrencyEndpoints
{
    public static void MapCurrencyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/currencies")
                       .WithTags("Currencies");

        group.MapGet("/", async (ISender sender) =>
        {
            var query = new GetCurrenciesQuery();

            var result = await sender.Send(query);

            return result;
        });

        group.MapPost("/", async (CreateCurrencyCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            if (result.IsSuccessful)
                return Results.Created($"/currencies/{result.Value.Id}", result);

            return Results.BadRequest(result);
        });
    }
}
