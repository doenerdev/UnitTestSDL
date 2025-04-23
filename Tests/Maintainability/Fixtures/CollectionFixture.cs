using Xunit;

namespace Tests.Maintainability.Fixtures;


//Test Code
public class DatabaseCollectionFixture : IDisposable
{
    public DatabaseCollectionFixture()
    {
        //Setup
    }

    public void Dispose()
    {
        //Teardown
    }
}

[CollectionDefinition("Database collection")]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
}

[Collection("Database collection")]
public class DatabaseCollectionFixtureTests1 : IClassFixture<DatabaseFixture>
{
    private DatabaseFixture databaseFixture;
    
    [Fact]
    public void SomeTest()
    {
        //test something here
        Assert.True(true);
    }
}

[Collection("Database collection")]
public class DatabaseCollectionFixtureTests12: IClassFixture<DatabaseFixture>
{
    private DatabaseFixture databaseFixture;
    
    [Fact]
    public void SomeTest()
    {
        //test something here
        Assert.True(true);
    }
}



