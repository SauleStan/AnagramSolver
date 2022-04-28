using AnagramSolver.BusinessLogic;
using NUnit.Framework;

namespace AnagramSolver.Tests;

public class AnagramControllerTests
{
    [Test]
    public void FindAnagrams_ReturnsAnagramHashSet_WhenGivenInputWord()
    {
        // Arrange
        AnagramController anagramController = new (new AnagramService());

        // Act
        var result = anagramController.FindAnagrams("alus");
        
        // Assert
        Assert.IsTrue(result.Contains("sula"));
    }
}