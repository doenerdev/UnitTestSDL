using Xunit;

namespace Tests.Example1;

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
    [Theory]
    [InlineData(100, 1.5, 150)]
    public void CalculateLeaseRate_RateIsCalculatedWithFactor(
        decimal installment, 
        decimal factor, 
        decimal expected)
    {
        //Arrange
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



