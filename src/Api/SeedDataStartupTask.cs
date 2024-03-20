using Cocktail.Api.Interfaces;
using Cocktail.Application.Repositories;
using Cocktail.Application.Specifications;
using Cocktail.Domain.Aggregates;

namespace Cocktail.Api;

public class SeedDataStartupTask(IServiceProvider serviceProvider) : IStartupTask
{
    public async Task Execute()
    {
        using var scope = serviceProvider.CreateScope();
        var ingredientRepository = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Ingredient>>();
        var cocktailRepository = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Domain.Aggregates.Cocktail>>();

        var ingredients = await ingredientRepository.ListAsync(new IngredientSpec());
        if (ingredients.Count != 0)
            return;
            
        var rum = new Ingredient("Rum", 40);
        var vodka = new Ingredient("Vodka", 40);
        var gin = new Ingredient("Gin", 47);
        var tequila = new Ingredient("Tequila", 35);
        var tripleSec = new Ingredient("Triple Sec", 30);
        var lemonJuice = new Ingredient("Lemon Juice", 0);
        var sugarSyrup = new Ingredient("Sugar Syrup", 0);
        var orangeJuice = new Ingredient("Orange Juice", 0);
        var soda = new Ingredient("Soda", 0);
        var amaretto = new Ingredient("Amaretto", 28);
        var campari = new Ingredient("Campari", 25);
        var vermouth = new Ingredient("Vermouth", 16);
        var angosturaBitters = new Ingredient("Angostura Bitters", 44.7);
        var blueCuracao = new Ingredient("Blue Curacao", 20);
        var grenadine = new Ingredient("Grenadine", 0);
        var tonicWater = new Ingredient("Tonic Water", 0);
        var cola = new Ingredient("Cola", 0);
        var limeJuice = new Ingredient("Lime Juice", 0);
        var absinthe = new Ingredient("Absinthe", 60);
        var cointreau = new Ingredient("Cointreau", 40);
        var peachSchnapps = new Ingredient("Peach Schnapps", 20);
        var appleLiqueur = new Ingredient("Apple Liqueur", 20);
        var coffeeLiqueur = new Ingredient("Coffee Liqueur", 20);
        var apricotBrandy = new Ingredient("Apricot Brandy", 25);
        var prosecco = new Ingredient("Prosecco", 11);
        var champagne = new Ingredient("Champagne", 12);
        var gingerBeer = new Ingredient("Ginger Beer", 0);
        var ice = new Ingredient("Ice", 0);
        var mint = new Ingredient("Mint", 0);
        var aperol = new Ingredient("Aperol", 12);
        var bourbon = new Ingredient("Bourbon", 40);
        var cranberryJuice = new Ingredient("Craberry Juice", 0);
        var espresso = new Ingredient("Expresso", 0);
        
        await ingredientRepository.AddAsync(rum);
        await ingredientRepository.AddAsync(vodka);
        await ingredientRepository.AddAsync(gin);
        await ingredientRepository.AddAsync(tequila);
        await ingredientRepository.AddAsync(tripleSec);
        await ingredientRepository.AddAsync(lemonJuice);
        await ingredientRepository.AddAsync(sugarSyrup);
        await ingredientRepository.AddAsync(orangeJuice);
        await ingredientRepository.AddAsync(soda);
        await ingredientRepository.AddAsync(amaretto);
        await ingredientRepository.AddAsync(campari);
        await ingredientRepository.AddAsync(vermouth);
        await ingredientRepository.AddAsync(angosturaBitters);
        await ingredientRepository.AddAsync(blueCuracao);
        await ingredientRepository.AddAsync(grenadine);
        await ingredientRepository.AddAsync(tonicWater);
        await ingredientRepository.AddAsync(cola);
        await ingredientRepository.AddAsync(limeJuice);
        await ingredientRepository.AddAsync(absinthe);
        await ingredientRepository.AddAsync(cointreau);
        await ingredientRepository.AddAsync(peachSchnapps);
        await ingredientRepository.AddAsync(appleLiqueur);
        await ingredientRepository.AddAsync(coffeeLiqueur);
        await ingredientRepository.AddAsync(apricotBrandy);
        await ingredientRepository.AddAsync(prosecco);
        await ingredientRepository.AddAsync(champagne);
        await ingredientRepository.AddAsync(gingerBeer);
        await ingredientRepository.AddAsync(ice);
        await ingredientRepository.AddAsync(mint);
        await ingredientRepository.AddAsync(aperol);
        await ingredientRepository.AddAsync(bourbon);
        await ingredientRepository.AddAsync(cranberryJuice);
        await ingredientRepository.AddAsync(espresso);

        var negroni = new Domain.Aggregates.Cocktail("Negroni",
            "A popular Italian cocktail, made of one part gin, one part vermouth rosso, and one part Campari, garnished with orange peel.");
        negroni.AddComposition(new Composition(30, Unit.Ml, gin));
        negroni.AddComposition(new Composition(30, Unit.Ml, vermouth));
        negroni.AddComposition(new Composition(30, Unit.Ml, campari));
        negroni.AddComposition(new Composition(5, Unit.Pc, ice));
        negroni.AddStep(new Step(0, "Stir all ingredients in mixing glass with ice and strain into chilled glass. Garnish with orange twist."));

        var margarita = new Domain.Aggregates.Cocktail("Margarita",
            "A cocktail consisting of tequila, orange liqueur, and lime juice often served with salt on the rim of the glass.");
        margarita.AddComposition(new Composition(40, Unit.Ml, tequila));
        margarita.AddComposition(new Composition(20, Unit.Ml, cointreau));
        margarita.AddComposition(new Composition(20, Unit.Ml, limeJuice));
        margarita.AddComposition(new Composition(3, Unit.Pc, ice));
        margarita.AddStep(new Step(0, "Shake all ingredients with ice and strain into a cocktail glass rimmed with salt."));

        var mojito = new Domain.Aggregates.Cocktail("Mojito",
            "A refreshing cocktail made from five ingredients: white rum, sugar, lime juice, soda water, and mint.");
        mojito.AddComposition(new Composition(50, Unit.Ml, rum));
        mojito.AddComposition(new Composition(20, Unit.Ml, limeJuice));
        mojito.AddComposition(new Composition(6, Unit.Pc, mint));
        mojito.AddComposition(new Composition(10, Unit.Ml, sugarSyrup));
        mojito.AddComposition(new Composition(100, Unit.Ml, soda));
        mojito.AddComposition(new Composition(10, Unit.Pc, ice));
        mojito.AddStep(new Step(0, "Muddle mint leaves with sugar and lime juice. Add a splash of soda water and fill the glass with cracked ice. Pour the rum and top with soda water. Garnish with sprig of mint."));

        var spritz = new Domain.Aggregates.Cocktail("Spritz",
            "An Italian wine-based cocktail, commonly served as an aperitif in Northeast Italy.");
        spritz.AddComposition(new Composition(60, Unit.Ml, prosecco));
        spritz.AddComposition(new Composition(60, Unit.Ml, aperol));
        spritz.AddComposition(new Composition(30, Unit.Ml, soda));
        spritz.AddComposition(new Composition(5, Unit.Pc, ice));
        spritz.AddStep(new Step(0, "Build into glass over ice, garnish and serve."));

        var screwdriver = new Domain.Aggregates.Cocktail("Screwdriver",
            "A simple but invigorating cocktail made with vodka and orange juice, served tall over ice.");
        screwdriver.AddComposition(new Composition(50, Unit.Ml, vodka));
        screwdriver.AddComposition(new Composition(100, Unit.Ml, orangeJuice));
        screwdriver.AddComposition(new Composition(5, Unit.Pc, ice));
        screwdriver.AddStep(new Step(0, "Build all ingredients in a highball glass filled with ice. Garnish with orange slice."));

        var bellini = new Domain.Aggregates.Cocktail("Bellini",
            "An Italian cocktail composed of Prosecco sparkling wine and peach purée or nectar.");
        bellini.AddComposition(new Composition(100, Unit.Ml, prosecco));
        bellini.AddComposition(new Composition(50, Unit.Ml, peachSchnapps));
        bellini.AddStep(new Step(0, "Pour peach puree into chilled glass and add prosecco."));

        var cosmopolitan = new Domain.Aggregates.Cocktail("Cosmopolitan",
            "A classy cocktail made with vodka, triple sec, cranberry juice, and freshly squeezed or sweetened lime juice.");
        cosmopolitan.AddComposition(new Composition(40, Unit.Ml, vodka));
        cosmopolitan.AddComposition(new Composition(15, Unit.Ml, cointreau));
        cosmopolitan.AddComposition(new Composition(30, Unit.Ml, limeJuice));
        cosmopolitan.AddComposition(new Composition(30, Unit.Ml, cranberryJuice));
        cosmopolitan.AddComposition(new Composition(2, Unit.Pc, ice));
        cosmopolitan.AddStep(new Step(0, "Shake all ingredients with ice and strain into a martini glass. Garnish with a lime wedge."));

        var oldFashioned = new Domain.Aggregates.Cocktail("Old Fashioned",
            "Known as a classic cocktail, the Old Fashioned is a blend of bourbon (or rye), sugar, and a few dashes of bitters.");
        oldFashioned.AddComposition(new Composition(60, Unit.Ml, bourbon));
        oldFashioned.AddComposition(new Composition(5, Unit.Ml, sugarSyrup));
        oldFashioned.AddComposition(new Composition(5, Unit.Ml, angosturaBitters));
        oldFashioned.AddComposition(new Composition(5, Unit.Pc, ice));

        var espressoMartini = new Domain.Aggregates.Cocktail("Espresso Martini",
            "A cold, coffee-flavored cocktail made with vodka, espresso coffee, coffee liqueur, and sugar syrup.");
        espressoMartini.AddComposition(new Composition(50, Unit.Ml, vodka));
        espressoMartini.AddComposition(new Composition(10, Unit.Ml, coffeeLiqueur));
        espressoMartini.AddComposition(new Composition(50, Unit.Ml, espresso));
        espressoMartini.AddComposition(new Composition(5, Unit.Pc, ice));
        espressoMartini.AddStep(new Step(0, "Shake all ingredients with ice and strain into a martini glass. Garnish with coffee beans."));

        var daiquiri = new Domain.Aggregates.Cocktail("Daiquiri",
            "A cocktail made with rum, citrus juice (typically lime), and sugar or other sweetener.");
        daiquiri.AddComposition(new Composition(50, Unit.Ml, rum));
        daiquiri.AddComposition(new Composition(20, Unit.Ml, limeJuice));
        daiquiri.AddComposition(new Composition(10, Unit.Ml, sugarSyrup));
        daiquiri.AddComposition(new Composition(5, Unit.Pc, ice));
        daiquiri.AddStep(new Step(0, "Shake all ingredients with ice and strain into a cocktail glass."));
    
        await cocktailRepository.AddAsync(negroni);
        await cocktailRepository.AddAsync(margarita);
        await cocktailRepository.AddAsync(mojito);
        await cocktailRepository.AddAsync(spritz);
        await cocktailRepository.AddAsync(screwdriver);
        await cocktailRepository.AddAsync(bellini);
        await cocktailRepository.AddAsync(cosmopolitan);
        await cocktailRepository.AddAsync(oldFashioned);
        await cocktailRepository.AddAsync(espressoMartini);
        await cocktailRepository.AddAsync(daiquiri);
    }
}