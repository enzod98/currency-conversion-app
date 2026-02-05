using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        string apiPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../Currency.API"));

        var configuration = new ConfigurationBuilder()
                    .SetBasePath(apiPath)
                    .AddJsonFile("appsettings.json")
                    .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var dbName = connectionString.Split('=').Last();
        var dbFullPath = Path.Combine(apiPath, dbName);
        connectionString = $"Data Source={dbFullPath}";

        optionsBuilder.UseSqlite(connectionString, b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
