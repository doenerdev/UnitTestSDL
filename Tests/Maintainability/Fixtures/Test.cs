using Xunit;

namespace Tests.Maintainability.Fixtures;


//Test Code
public class DatabaseFixture : IDisposable
{
    public DatabaseFixture()
    {
        //Setup
    }

    public void Dispose()
    {
        //Teardown
    }
}

public class CalculatorTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _databaseFixture;

    public CalculatorTests(DatabaseFixture databaseFixture)
    {
        _databaseFixture = databaseFixture;
    }

    [Fact]
    public void CalculateLeaseRate_RateIsCalculatedWithFactor()
    {
        //test something here
        Assert.True(true);
    }
}



