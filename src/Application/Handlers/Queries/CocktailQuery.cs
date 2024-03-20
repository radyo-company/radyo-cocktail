using Cocktail.Application.Models.Dtos;
using Cocktail.Application.Repositories;
using Cocktail.Application.Specifications;
using MediatR;

namespace Cocktail.Application.Handlers.Queries;

public record CocktailQuery(string? IngredientName) : IRequest<IEnumerable<CocktailDto>>;

public class CocktailQueryHandler(IQueryProcessor<Domain.Aggregates.Cocktail> cocktailRepository) : IRequestHandler<CocktailQuery, IEnumerable<CocktailDto>>
{
    public async Task<IEnumerable<CocktailDto>> Handle(CocktailQuery request, CancellationToken cancellationToken)
    {
        var spec = new CocktailSpec()
            .WithIngredients()
            .WithStep();

        if (!string.IsNullOrEmpty(request.IngredientName))
        {
            spec.WithIngredientName(request.IngredientName);
        }

        return await cocktailRepository.ListAsync(spec, cancellationToken);
    }
}
