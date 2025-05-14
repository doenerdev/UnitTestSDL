using Moq;
using Xunit;

namespace Tests.ResistanceToRefactoring.OverspecifyingMockVerification;

//Productive Code
public class Calculator(IConditionListRepository repository) 
{
    public decimal CalculateLeaseRate(decimal baseInstallment)
    {
        var conditionList = repository.GetConditionListById();
        return baseInstallment * conditionList.Factor;
    }
    
    //potential refactoring, leading to failing tests
    // public decimal CalculateLeaseRate(decimal baseInstallment)
    // {
    //     var factor = FactorLookup.GetFactor(CalculationType.Default);
    //     return baseInstallment * factor;
    // }
}


//Test Code
public class CalculatorTests
{
    [Theory]
    [InlineData(100, 1.5, 150)]
    public void CalculateLeaseRate_WithMock_RateIsCalculatedWithFactor(
        decimal installment, 
        decimal factor, 
        decimal expected)
    {
        //Arrange
        var mockRepo = new Mock<IConditionListRepository>(MockBehavior.Strict);
        mockRepo.Setup(x => x.GetConditionListById())
            .Returns(new ConditionList(factor));
        var sut = new Calculator(mockRepo.Object);

        //Act
        var actual = sut.CalculateLeaseRate(installment);

        //Assert
        Assert.Equal(expected, actual);
        mockRepo.Verify(x => x.GetConditionListById(), Times.Once);
    }
}



