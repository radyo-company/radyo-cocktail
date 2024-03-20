using Cocktail.Application.Models.Dtos;
using Cocktail.Application.Repositories;
using Cocktail.Application.Specifications;
using FluentValidation;
using Mapster;
using MediatR;

namespace Cocktail.Application.Handlers.Commands;

public record CocktailCreateCommand(string Name) : IRequest<CocktailDto>;

public class CocktailCreateValidator : AbstractValidator<CocktailCreateCommand>
{
    
    public CocktailCreateValidator()
    {

        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();
    }
    
}

public class CocktailCreateCommandHandler(IAsyncRepository<Domain.Aggregates.Cocktail> cocktailRepository) : IRequestHandler<CocktailCreateCommand, CocktailDto>
{
    public async Task<CocktailDto> Handle(CocktailCreateCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Name))
            throw new ArgumentNullException("Name cannot be null or empty");

        var spec = new CocktailSpec().ForName(request.Name);
        var existingCocktail = await cocktailRepository.FindAsync(spec, cancellationToken);

        if (existingCocktail != null)
            throw new ArgumentException("The cocktail name must be unique.");

        var cocktail = new Domain.Aggregates.Cocktail(request.Name);

        await cocktailRepository.AddAsync(cocktail, cancellationToken);

        return cocktail.Adapt<CocktailDto>();
    }
}