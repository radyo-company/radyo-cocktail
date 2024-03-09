using Cocktail.Api.Extensions;
using Cocktail.Application.Extensions;
using Cocktail.Application.Repositories;
using Cocktail.Domain.Aggregates;
using Cocktail.Infrastructure.Data;
using Cocktail.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.IntegrationTest;

public class CocktailIntegrationTests : IDisposable
{
    protected ISender Mediator { get; }
    protected IAsyncRepository<Cocktail.Domain.Aggregates.Cocktail> CocktailRepository { get; }
    protected IAsyncRepository<Ingredient> IngredientRepository { get; }
    private readonly CocktailContext _context;

    protected CocktailIntegrationTests()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath( Directory.GetCurrentDirectory() )
            .AddJsonFile( "appsettings.json" )
            .AddEnvironmentVariables();
        var configuration = builder.Build();

        var services = new ServiceCollection();
        services.AddScoped( _ => configuration );
        services.AddLogging();
        services.AddApi(configuration);
        services.AddApplication(configuration);
        services.AddDbContext<CocktailContext>(options =>
            options.UseSqlite(configuration.GetConnectionString(nameof(Cocktail))));
        services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>))
            .AddScoped(typeof(IQueryProcessor<>), typeof(QueryProcessor<>));
        
        var provider = services.BuildServiceProvider();
        var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
        var serviceProvider = scopeFactory.CreateScope().ServiceProvider;
        
        _context = serviceProvider.GetRequiredService<CocktailContext>();
        _context.Database.OpenConnection();
        _context.Database.Migrate();

        Mediator = serviceProvider.GetRequiredService<ISender>();
        CocktailRepository = serviceProvider.GetRequiredService<IAsyncRepository<Cocktail.Domain.Aggregates.Cocktail>>();
        IngredientRepository = serviceProvider.GetRequiredService<IAsyncRepository<Ingredient>>();
    }


    public void Dispose()
    {
        _context.Database.CloseConnection();
        _context.Dispose();
    }
}