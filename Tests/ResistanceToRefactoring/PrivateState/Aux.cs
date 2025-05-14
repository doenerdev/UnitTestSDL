namespace Tests.ResistanceToRefactoring.PrivateState;

public interface IConditionListRepository
{
    ConditionList GetConditionListById(Guid conditionListId);
}

public class ConditionList(decimal factor)
{
    public decimal Factor { get; } = factor;
}

public enum VerificationStatus
{
    Unverified,
    Pending,
    Verified
}
