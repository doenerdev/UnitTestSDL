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
       
        throw new ArgumentException("No appropriate genus provided");
    }
    
    //--> 100% coverage
    public bool IsAnimalV2(string genus) => genus is "Dog" ? true : throw new ArgumentException("No appropriate genus provided");
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

        //Act & Assert
        Assert.True(actual);
    }
    
    [Fact]
    public void IsAnimalV2_Dog_ReturnsTrue()
    {
        //Arrange
        var sut = new Classifier();

        //Act
        var actual = sut.IsAnimalV2("Dog");

        //Act & Assert
        Assert.True(actual);
    }
}



