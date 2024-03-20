using Cocktail.Application.Handlers.Commands;
using Cocktail.Application.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cocktail.Api.Controllers;


[ApiController]
[Route("[controller]")]
public class CocktailController(ISender mediator) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? ingredientName)
    {
        return Ok(await mediator.Send(new CocktailQuery(ingredientName)));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CocktailCreateCommand request)
    {
        return Ok(await mediator.Send(request));
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(CocktailUpdateCommand request)
    {
        return Ok(await mediator.Send(request));
    }
    
    [HttpPut("add-step")]
    public async Task<IActionResult> AddStep(CocktailAddStepCommand request)
    {
        await mediator.Send(request);
        return NoContent();
    }
    
    [HttpPut("remove-step")]
    public async Task<IActionResult> RemoveStep(CocktailRemoveStepCommand request)
    {
        await mediator.Send(request);
        return NoContent();
    }
    
    [HttpPut("add-ingredient")]
    public async Task<IActionResult> AddIngredient(CocktailAddIngredientCommand request)
    {
        await mediator.Send(request);
        return NoContent();
    }
    
    [HttpPut("remove-ingredient")]
    public async Task<IActionResult> RemoveIngredient(CocktailRemoveIngredientCommand request)
    {
        await mediator.Send(request);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await mediator.Send(new CocktailDeleteCommand(id));
        return NoContent();
    }
}