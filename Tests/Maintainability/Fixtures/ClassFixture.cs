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

public class DatabaseFixtureTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _databaseFixture;

    public DatabaseFixtureTests(DatabaseFixture databaseFixture)
    {
        _databaseFixture = databaseFixture;
    }

    [Fact]
    public void SomeTest()
    {
        //test something here
        Assert.True(true);
    }
}



