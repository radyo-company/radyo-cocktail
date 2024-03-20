using Cocktail.Application.Mappers;

namespace Cocktail.Application.Models.Dtos;

public class CocktailDto : IMapFrom<Domain.Aggregates.Cocktail>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<CompositionDto> Compositions { get; set; } = new();
    public List<StepDto> Steps { get; set; } = new();
}