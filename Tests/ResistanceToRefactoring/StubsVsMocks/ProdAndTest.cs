using Moq;
using Xunit;

namespace Tests.ResistanceToRefactoring.StubsVsMocks;

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

        //Act & Assert
        Assert.Equal(expected, actual);
        mockRepo.Verify(x => x.GetConditionListById(conditionListId), Times.Once);
        mockRepo.VerifyNoOtherCalls();
    }
}



