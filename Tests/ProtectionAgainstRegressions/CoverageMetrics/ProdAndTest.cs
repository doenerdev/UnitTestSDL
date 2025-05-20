using Xunit;

namespace Tests.ProtectionAgainstRegressions.CoverageMetrics;

//Productive Code
public class Classifier() 
{
    //--> 80% coverage
    public bool IsAnimal(string genus)
    {
        if (genus is "Dog")
            return true;

        return false;
    }
    
    //--> 100% coverage
    public bool IsAnimalV2(string genus) => genus is "Dog";
}


//Test Code
public class ClassifierTests
{
    [Fact]
    public void IsAnimal_Dog_ReturnsTrue()
    {
        //Arrange
        var sut = new Classifier();

        //Act
        var actual = sut.IsAnimal("Dog");

        //Assert
        Assert.True(actual);
    }
    
    [Fact]
    public void IsAnimalV2_Dog_ReturnsTrue()
    {
        //Arrange
        var sut = new Classifier();

        //Act
        var actual = sut.IsAnimalV2("Dog");

        //Assert
        Assert.True(actual);
    }
    
    //Faking code coverage via tests
    [Fact]
    public void IsAnimal_FakeCoverage()
    {
        var sut = new Classifier();
        sut.IsAnimal("Dog");
        sut.IsAnimal("somethingElse");

        //Assert
        Assert.True(true);
    }
}



