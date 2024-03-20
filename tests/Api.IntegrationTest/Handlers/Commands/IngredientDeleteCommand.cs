using Cocktail.Domain.Aggregates;
using FluentAssertions;
using ApplicationException = Cocktail.Application.Exceptions.ApplicationException;

namespace Api.IntegrationTest.Handlers.Commands;

public class IngredientDeleteCommand : CocktailIntegrationTests
{
    [Fact]
    public async Task ShouldHaveAExceptionWhenIDeleteAIngredientUsedByCocktail()
    {
        // Arrange
        var gin = new Ingredient("Gin", 47);
        var campari = new Ingredient("Campari", 25);
        var vermouth = new Ingredient("Vermouth", 16);
        var ice = new Ingredient("Ice", 0);

        await IngredientRepository.AddAsync(gin);
        await IngredientRepository.AddAsync(campari);
        await IngredientRepository.AddAsync(vermouth);
        await IngredientRepository.AddAsync(ice);
        
        var negroni = new Cocktail.Domain.Aggregates.Cocktail("Negroni", "A cocktail made with rum, citrus juice (typically lime), and sugar or other sweetener.");
        negroni.AddComposition(new Composition(30, Unit.Ml, gin));
        negroni.AddComposition(new Composition(30, Unit.Ml, vermouth));
        negroni.AddComposition(new Composition(30, Unit.Ml, campari));
        negroni.AddComposition(new Composition(5, Unit.Pc, ice));
        negroni.AddStep(new Step(0, "Stir all ingredients in mixing glass with ice and strain into chilled glass. Garnish with orange twist."));

        await CocktailRepository.AddAsync(negroni);
        
        var request = new Cocktail.Application.Handlers.Commands.IngredientDeleteCommand(campari.Id);
        
        // Act
        try
        {
            await Mediator.Send(request);
        }
        catch (ApplicationException e)
        {
            // Assert
            e.Code.Should().Be("IngredientUsed");
            e.Message.Should().Be("The ingredient is used in a cocktail and cannot be deleted.");
        }
    }
}