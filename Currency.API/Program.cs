using Application;
using Currency.API.Endpoints;
using Currency.API.Middlewares;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TechNotes API",
        Version = "v1",
        Description = "API Prueba Técnica"
    });
});


builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

await DbInitializer.ApplyMigrations(app.Services);

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

var apiGroup = app.MapGroup("/api");

apiGroup.MapUserEndpoints();
apiGroup.MapAddressEndpoints();
apiGroup.MapCurrencyEndpoints();

app.Run();
