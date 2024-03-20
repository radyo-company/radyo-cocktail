using Cocktail.Application.Models.Dtos;
using Cocktail.Application.Repositories;
using FluentValidation;
using Mapster;
using MediatR;

namespace Cocktail.Application.Handlers.Commands;

public record CocktailCreateCommand(string Name, string Description) : IRequest<CocktailDto>;

public class CocktailCreateValidator : AbstractValidator<CocktailCreateCommand>
{
    public CocktailCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);
    }
}

public class CocktailCreateCommandHandler(IAsyncRepository<Domain.Aggregates.Cocktail> cocktailRepository) : IRequestHandler<CocktailCreateCommand, CocktailDto>
{
    public async Task<CocktailDto> Handle(CocktailCreateCommand request, CancellationToken cancellationToken)
    {
        var cocktail = new Domain.Aggregates.Cocktail(request.Name, request.Description);
        await cocktailRepository.AddAsync(cocktail, cancellationToken);
        return cocktail.Adapt<CocktailDto>();
    }
}