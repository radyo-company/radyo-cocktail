using Ardalis.Specification;
using Cocktail.Application.Models.Dtos;
using Mapster;

namespace Cocktail.Application.Specifications;

public sealed class CocktailSpec : Specification<Domain.Aggregates.Cocktail, CocktailDto>
{
    public CocktailSpec()
    {
        Query.Select(c => c.Adapt<CocktailDto>());
    }
    public CocktailSpec WithStep()
    {
        Query.Include(c => c.Steps);
        return this;
    }
    public CocktailSpec WithIngredients()
    {
        Query.Include(c => c.Compositions).ThenInclude(c => c.Ingredient);
        return this;
    }

    public CocktailSpec ForName(string name)
    {
        Query.Where(c => c.Name == name);
        return this;
    }
}