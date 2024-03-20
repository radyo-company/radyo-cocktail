using System.Reflection;
using Cocktail.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Cocktail.Infrastructure.Data;

public class CocktailContext : DbContext
{
    public CocktailContext(
        DbContextOptions< CocktailContext > options
    ) : base( options)
    {
    }
    
    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Domain.Aggregates.Cocktail>()
            .HasIndex(c => c.Name)
            .IsUnique();

        modelBuilder.ApplyAllConfigurationsAssemblyNamespace(Assembly.GetExecutingAssembly(),
            typeof(CocktailContext).Namespace + ".EntityConfigurations");
    }
}