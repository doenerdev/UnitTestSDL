namespace Tests.Maintainability.ParameterizedTests;

public interface IConditionListRepository
{
    ConditionList GetConditionListById(Guid conditionListId);
}

public class ConditionList(decimal factor)
{
    public decimal Factor { get; } = factor;
}
