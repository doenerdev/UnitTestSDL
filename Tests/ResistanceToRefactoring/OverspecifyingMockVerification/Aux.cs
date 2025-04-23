namespace Tests.ResistanceToRefactoring.OverspecifyingMockVerification;

public interface IConditionListRepository
{
    ConditionList GetConditionListById();
}

public class ConditionList(decimal factor)
{
    public decimal Factor { get; } = factor;
}

public static class FactorLookup
{
    private static readonly IDictionary<CalculationType, decimal> Factors = new Dictionary<CalculationType, decimal>
    {
        { CalculationType.Default, 1.5m}
    };
    
    public static decimal GetFactor(CalculationType type)
    {
        return Factors.TryGetValue(type, out var factor) ? factor : 0m;
    }
}

public enum CalculationType
{
    Default
}