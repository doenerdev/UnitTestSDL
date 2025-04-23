namespace Tests.Maintainability.SetupAndTeardown;

public interface IConditionListRepository
{
    ConditionList GetConditionListById(Guid conditionListId);
}

public class ConditionList(decimal factor)
{
    public decimal Factor { get; } = factor;
}
