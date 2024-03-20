using System.Collections.Immutable;

namespace Cocktail.Domain.Aggregates;

public class Cocktail : Entity<Guid>
{
    private List<Composition> _compositions;
    private List<Step> _steps;
    public string Name { get; private set; }
    
    public Cocktail(string name)
    {
        _steps = new List<Step>();
        _compositions = new List<Composition>();
        Name = name;
        CreatedOn = DateTimeOffset.UtcNow;
    }
    
    public IReadOnlyCollection<Composition> Compositions
    {
        get => _compositions.ToImmutableList();
        private set => _compositions = value.ToList();
    }
    
    
    public IReadOnlyCollection<Step> Steps
    {
        get => _steps.ToImmutableList();
        private set => _steps = value.ToList();
    }
    
    public void AddComposition(Composition composition)
    {
        _compositions.Add(composition);
    }
    
    public void RemoveComposition(Composition composition)
    {
        _compositions.Remove(composition);
    }
    
    public void AddStep(Step step)
    {
        _steps.Add(step);
    }
    
    public void RemoveStep(Step step)
    {
        _steps.Remove(step);
        var order = 0;
        foreach (var s in _steps.OrderBy(e => e.Order))
        {
            s.SetOrder(order++);
        }
    }

    public void UpdateName(string name)
    {
        Name = name;
    }

    public void Remove()
    {
        DeletedOn = DateTimeOffset.UtcNow;
    }
}