namespace Tests.Maintainability.Fixtures;

public interface IConditionListRepository
{
    ConditionList GetConditionListById(Guid conditionListId);
}

public class LeaseRate
{
    public decimal Rate { get; set; }
    public decimal AppliedFactor { get; set; }
}

public class ConditionList(decimal factor)
{
    public decimal Factor { get; } = factor;
}
