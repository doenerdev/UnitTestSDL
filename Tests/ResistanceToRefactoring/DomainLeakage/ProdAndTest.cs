using Xunit;

namespace Tests.ResistanceToRefactoring.DomainLeakage;

//Productive Code
public class Calculator(IConditionListRepository repository) 
{
    public decimal CalculateLeaseRate(
        decimal baseInstallment, 
        Guid conditionListId)
    {
        var conditionList = repository
            .GetConditionListById(conditionListId);


        return baseInstallment * conditionList.Factor;
    }
}


//Test Code
public class CalculatorTests
{
    [Fact]
        public void CalculateLeaseRate_RateIsCalculatedWithFactor()
    {
        //Arrange
        var factor = 1.5m;
        var installment = 100m;
        
        var expected = factor * installment; //implementation or algorithm details leaked to the test

        var stubRepo = new StubRepository(factor);
        var sut = new Calculator(stubRepo);

        //Act
        var actual = sut.CalculateLeaseRate(installment, Guid.NewGuid());

        //Act & Assert
        Assert.Equal(expected, actual);
	
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



