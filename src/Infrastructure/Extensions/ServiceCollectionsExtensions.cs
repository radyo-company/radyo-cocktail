using Cocktail.Application.Repositories;
using Cocktail.Infrastructure.Data;
using Cocktail.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cocktail.Infrastructure.Extensions;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(nameof(Domain.Aggregates.Cocktail));
        services
            .AddDbContext<CocktailContext>(options => options.UseSqlite(connectionString))
            .AddRepositories();
        
        return services;
    }
    
    private static IServiceCollection AddRepositories( this IServiceCollection services )
    {
        return services
            .AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>))
            .AddScoped(typeof(IQueryProcessor<>), typeof(QueryProcessor<>));
    }
}