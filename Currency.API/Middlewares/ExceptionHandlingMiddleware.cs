using Domain.Abstractions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace Currency.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error no controlado: { ex.Message }");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = HttpStatusCode.InternalServerError;
        string message = string.Empty;

        if (exception is ValidationException validationException)
        {
            statusCode = HttpStatusCode.BadRequest;
            var errors = validationException.Errors
                .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");

            message = string.Join(" | ", errors);
        }
        else if (exception is DbUpdateException dbUpdateException && dbUpdateException.InnerException is SqliteException sqliteException)
        {
            if (sqliteException.SqliteErrorCode == 19)
            {
                statusCode = HttpStatusCode.Conflict;
                message = "Ya existe un registro con estos datos.";
            }
        }
        else
        {
            message = "Ha ocurrido un error inesperado.";
        }

        context.Response.StatusCode = (int)statusCode;

        var result = Result.Fail(message);
        return context.Response.WriteAsJsonAsync(result);
    }
}
