using Domain.Abstractions;
using Domain.Addresses;
using Domain.Currencies;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Currency> Currencies => Set<Currency>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Address> Adresses => Set<Address>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Address>()
            .HasOne(a => a.User)
            .WithMany(u => u.Addresses)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Currency>()
            .HasIndex(c => c.Code)
            .IsUnique();


        // Para autoincrementar el Id de todas las entidade que heredan de Entity
        foreach (var entityType in  modelBuilder.Model.GetEntityTypes())
            if (typeof(Entity).IsAssignableFrom(entityType.ClrType) && !entityType.ClrType.IsAbstract)
                modelBuilder.Entity(entityType.ClrType, builder =>
                {
                    builder.HasKey("Id");
                    builder.Property("Id")
                            .ValueGeneratedOnAdd();
                });
        
        base.OnModelCreating(modelBuilder);

    }
}
