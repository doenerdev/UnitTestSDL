using Xunit;

namespace Tests.Maintainability.SetupAndTeardown;

//Productive Code
public class Calculator(IConditionListRepository repository) 
{
    public decimal CalculateLeaseRate(
        decimal baseInstallment, 
        Guid conditionListId)
    {
        var conditionList = repository.GetConditionListById(conditionListId);

        return baseInstallment * conditionList.Factor;
    }
}


//Test Code
public class CalculatorTests
{
    private readonly IConditionListRepository _stubRepository;
    private const decimal DefaultFactor = 1.5M;
    
    public CalculatorTests()
    {
        _stubRepository = new StubRepository(DefaultFactor);
    }
    
    //with global setup
    [Theory]
    [InlineData(100, 150)]
    public void CalculateLeaseRate_RateIsCalculatedWithFactor(decimal installment, decimal expected)
    {
        //Arrange
        var sut = new Calculator(_stubRepository);

        //Act
        var actual = sut.CalculateLeaseRate(installment, Guid.NewGuid());

        //Assert
        Assert.Equal(expected, actual);
    }
    
    //with factory method
    [Theory]
    [InlineData(100, 1.5, 150)]
    public void CalculateLeaseRate_WithFactoryMethod_RateIsCalculatedWithFactor(decimal installment, decimal factor, decimal expected)
    {
        //Arrange
        var stubRepository = CreateStubRepository(factor);
        var sut = new Calculator(stubRepository);

        //Act
        var actual = sut.CalculateLeaseRate(installment, Guid.NewGuid());

        //Assert
        Assert.Equal(expected, actual);
    }

    private static StubRepository CreateStubRepository(decimal factor)
    {
        return new StubRepository(DefaultFactor);
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



