using Cocktail.Application.Models.Dtos;
using Cocktail.Application.Repositories;
using Cocktail.Application.Specifications;
using MediatR;

namespace Cocktail.Application.Handlers.Queries;

public record CocktailQuery : IRequest<IEnumerable<CocktailDto>>;

public class CocktailQueryHandler(IQueryProcessor<Domain.Aggregates.Cocktail> cocktailRepository) : IRequestHandler<CocktailQuery, IEnumerable<CocktailDto>>
{
    public async Task<IEnumerable<CocktailDto>> Handle(CocktailQuery request, CancellationToken cancellationToken)
    {
        var cocktails = await cocktailRepository.ListAsync(new CocktailSpec()
            .WithIngredients()
            .WithStep(), cancellationToken);

        

        foreach (var cocktail in cocktails)
        {
            
            int numOfIceCubes = cocktail.Compositions.Count(c => c.Ingredient.Name == "IceCube");
            
            double totalAlcohol = cocktail.Compositions.Sum(ingredient => ingredient.Quantity * ingredient.Ingredient.AlcoholLevel); 

            double dilution = numOfIceCubes * 0.1;

            double totalVolume = cocktail.Compositions.Sum(ingredient => ingredient.Quantity) + dilution;

            double alcoholContent = (totalAlcohol / totalVolume);
            cocktail.AlcoholContent = Math.Round(alcoholContent, 2);
        }

        return cocktails;
    }
}
