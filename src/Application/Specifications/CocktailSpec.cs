using Ardalis.Specification;
using Cocktail.Application.Models.Dtos;
using Mapster;

namespace Cocktail.Application.Specifications;

public sealed class CocktailSpec : Specification<Domain.Aggregates.Cocktail, CocktailDto>
{
    
    private string? IngredientName { get; set; }
    
    public CocktailSpec()
    {
        Query.Select(c => c.Adapt<CocktailDto>());
    }

    public CocktailSpec WithIngredientName(string? ingredientName)
    {
        IngredientName = ingredientName;
        
        if (!string.IsNullOrEmpty(IngredientName))
        {
            Query.Where(c => c.Compositions.Any(comp => comp.Ingredient.Name.Contains(IngredientName)));
        }

        return this;
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
}