using Moq;
using Xunit;

namespace Tests.Maintainability.ParameterizedTests;

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
    //Two separate test cases with 'Facts'
    [Fact]
    public void CalculateLeaseRate_WithSmallFactor_RateIsCalculatedCorrectly()
    {
        //Arrange
        const decimal installment = 100m;
        const decimal factor = 1.5m;
        const decimal expected = 150m;
        var stubRepo = new StubRepository(factor);
        var sut = new Calculator(stubRepo);

        //Act
        var actual = sut.CalculateLeaseRate(installment, Guid.NewGuid());

        //Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void CalculateLeaseRate_WithBigFactor_RateIsCalculatedCorrectly()
    {
        //Arrange
        const decimal installment = 100m;
        const decimal factor = 4;
        const decimal expected = 400m;
        var stubRepo = new StubRepository(factor);
        var sut = new Calculator(stubRepo);

        //Act
        var actual = sut.CalculateLeaseRate(installment, Guid.NewGuid());

        //Assert
        Assert.Equal(expected, actual);
    }
    
    //One test case as a 'Theory'
    [Theory]
    [InlineData(100, 1.5, 150)]
    [InlineData(100, 4, 400)]
    public void CalculateLeaseRate_WithDifferentFactors_RateIsCalculatedWithFactor(
        decimal installment, 
        decimal factor, 
        decimal expected)
    {
        //Arrange
        var stubRepo = new StubRepository(factor);
        var sut = new Calculator(stubRepo);

        //Act
        var actual = sut.CalculateLeaseRate(installment, Guid.NewGuid());

        //Assert
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
    
    [Theory]
    [InlineData(100, 1.5, 150, "e605f128-bac8-4ed6-a582-a658616d8a6f")]
    public void CalculateLeaseRate_WithMock_RateIsCalculatedWithFactor(
        decimal installment, 
        decimal factor, 
        decimal expected, 
        string conditionListIdAsString)
    {
        //Arrange
        var conditionListId = Guid.Parse(conditionListIdAsString);
        var mockRepo = new Mock<IConditionListRepository>(MockBehavior.Strict);
        mockRepo.Setup(x => x.GetConditionListById(conditionListId))
            .Returns(new ConditionList(factor));
        var sut = new Calculator(mockRepo.Object);

        //Act
        var actual = sut.CalculateLeaseRate(installment, conditionListId);

        //Assert
        Assert.Equal(expected, actual);
        mockRepo.Verify(x => x.GetConditionListById(conditionListId));
    }
}



