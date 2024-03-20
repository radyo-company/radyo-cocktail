using Cocktail.Application.Exceptions;
using Cocktail.Application.Models.Dtos;
using Cocktail.Application.Repositories;
using Cocktail.Domain.Aggregates;
using Cocktail.Domain.Specifications;
using FluentValidation;
using MediatR;

namespace Cocktail.Application.Handlers.Commands;

public record CocktailAddStepCommand(Guid Id, string Step) : IRequest;

public class CocktailAddStepValidator : AbstractValidator<CocktailAddStepCommand>
{
    public CocktailAddStepValidator()
    {
        RuleFor(x => x.Step)
            .NotNull()
            .NotEmpty();
    }
}

public class CocktailAddStepCommandHandler(IAsyncRepository<Domain.Aggregates.Cocktail> cocktailRepository) : IRequestHandler<CocktailAddStepCommand>
{
    public async Task Handle(CocktailAddStepCommand request, CancellationToken cancellationToken)
    {
        var cocktail = await cocktailRepository.GetAsync(new CocktailSpec().ById(request.Id).WithSteps(), cancellationToken);
        if (cocktail is null)
            throw new EntityNotFoundException<Domain.Aggregates.Cocktail>(nameof(request.Id), request.Id);
        var lastStep = cocktail.Steps.Max(s => s.Order);
        cocktail.AddStep(new Step(++lastStep, request.Step));
        await cocktailRepository.UpdateAsync(cocktail, cancellationToken);
    }
}