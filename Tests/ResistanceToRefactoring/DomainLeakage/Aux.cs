namespace Tests.ResistanceToRefactoring.PrivateMethods;

public interface IConditionListRepository
{
    ConditionList GetConditionListById(Guid conditionListId);
}

public class ConditionList(decimal factor)
{
    public decimal Factor { get; } = factor;
}
