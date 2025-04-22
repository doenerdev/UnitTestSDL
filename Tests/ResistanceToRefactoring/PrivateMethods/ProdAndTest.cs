using System.Reflection;
using Xunit;

namespace Tests.ResistanceToRefactoring.PrivateMethods;

//Productive Code
public class Calculator(IConditionListRepository repository) 
{
    public decimal CalculateLeaseRate(
        decimal baseInstallment, 
        Guid conditionListId)
    {
        var factor = DetermineFactor(conditionListId);

        return baseInstallment * factor;
    }

    private decimal DetermineFactor(Guid conditionListId) 
    {
        return repository
            .GetConditionListById(conditionListId).Factor;
    }
}


//Test Code
public class CalculatorTests
{
    [Fact]
    public void DetermineFactor_WithConditionListId_ReturnsCorrectFactor()
    {
        //Arrange
        var expected = 1.5m;
        var sut = new Calculator(new StubRepository(expected));
        MethodInfo methodInfo = typeof(Calculator).GetMethod("DetermineFactor", BindingFlags.NonPublic | BindingFlags.Instance);
        
        //Act
        var result = methodInfo!.Invoke(sut, [Guid.NewGuid()]); //the private method is invoked via refelection -> tight coupling to implementation

        //Assert
        Assert.Equal(1.5m, (decimal) result!);
	
    }

    private class StubRepository(decimal factor) 
        : IConditionListRepository
    {
        public ConditionList GetConditionListById(
            Guid conditionListId)
        {
            return new ConditionList(factor);
        }
    }
}



