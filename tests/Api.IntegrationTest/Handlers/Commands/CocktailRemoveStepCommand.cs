using Cocktail.Domain.Aggregates;
using FluentAssertions;

namespace Api.IntegrationTest.Handlers.Commands;

public class CocktailRemoveStepCommand : CocktailIntegrationTests
{
    [Fact]
    public async Task ShouldHaveACocktailWithOneStepLessAndNewOrderWhenIDeletedAStep()
    {
        // Arrange
        var gin = new Ingredient("Gin", 47);
        var campari = new Ingredient("Campari", 25);
        var vermouth = new Ingredient("Vermouth", 16);
        var ice = new Ingredient("Ice", 0);
        var stepToDelete = new Step(2, "StepToDelete");

        await IngredientRepository.AddAsync(gin);
        await IngredientRepository.AddAsync(campari);
        await IngredientRepository.AddAsync(vermouth);
        await IngredientRepository.AddAsync(ice);
        
        var negroni = new Cocktail.Domain.Aggregates.Cocktail("Negroni", "A cocktail made with rum, citrus juice (typically lime), and sugar or other sweetener.");
        negroni.AddComposition(new Composition(30, Unit.Ml, gin));
        negroni.AddComposition(new Composition(30, Unit.Ml, vermouth));
        negroni.AddComposition(new Composition(30, Unit.Ml, campari));
        negroni.AddComposition(new Composition(5, Unit.Pc, ice));
        negroni.AddStep(new Step(0, "Stir all ingredients in mixing glass with ice"));
        negroni.AddStep(new Step(1, "Strain into chilled glass."));
        negroni.AddStep(stepToDelete);
        negroni.AddStep(new Step(3, "Garnish with orange twist."));

        await CocktailRepository.AddAsync(negroni);
        
        var request = new Cocktail.Application.Handlers.Commands.CocktailRemoveStepCommand(negroni.Id, stepToDelete.Id);
        
        // Act
        await Mediator.Send(request);
        
        // Assert
        var cocktail = await CocktailRepository.GetAsync(new Cocktail.Domain.Specifications.CocktailSpec().ById(negroni.Id).WithSteps());
        cocktail.Should().NotBeNull();
        cocktail.Steps.Should().NotContain(stepToDelete);
        cocktail.Steps.Count.Should().Be(3);
        cocktail.Steps.Should().BeInAscendingOrder(s => s.Order);
    }
}