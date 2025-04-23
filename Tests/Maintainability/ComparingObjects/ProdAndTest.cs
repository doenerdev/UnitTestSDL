using Xunit;

namespace Tests.Maintainability.ComparingObjects;

//Productive Code
public class Calculator(IConditionListRepository repository) 
{
    public LeaseRate CalculateLeaseRate(
        decimal baseInstallment, 
        Guid conditionListId)
    {
        var conditionList = repository.GetConditionListById(conditionListId);

        var rate = baseInstallment * conditionList.Factor;
        return new LeaseRate
        {
            AppliedFactor = conditionList.Factor,
            Rate = rate
        };
    }
}


//Test Code
public class CalculatorTests
{
    [Fact]
    public void CalculateLeaseRate_RateIsCalculatedWithFactor()
    {
        //Arrange
        const decimal installment = 100m;
        const decimal factor = 1.5m;
        const decimal expectedRateValue = 150m;

        var expected = new LeaseRate{
            AppliedFactor = factor,
            Rate = expectedRateValue
        };;
        
        var stubRepo = new StubRepository(factor);
        var sut = new Calculator(stubRepo);

        //Act
        var actual = sut.CalculateLeaseRate(installment, Guid.NewGuid());

        //Act & Assert
        Assert.Equivalent(expected, actual);
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



