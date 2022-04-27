using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Models;
using NUnit.Framework;

namespace AnagramSolver.Tests;

public class AnagramServiceTests
{
    private readonly AnagramService _anagramService = new AnagramService();
    
    [Test]
    public void IsEqualCrib_ReturnsTrue_IfAnagramCribsAreTheSame()
    {
        // Arrange
        string crib = "alsu";
        Anagram anagram1 = new Anagram("alus", crib);
        Anagram anagram2 = new Anagram("sula", crib);

        // Act
        bool result = _anagramService.IsEqualCrib(anagram1, anagram2);

        // Assert
        Assert.IsTrue(result);
    }
    
    [Test]
    public void IsEqualCrib_ReturnsFalse_IfAnagramCribsAreDifferent()
    {
        // Arrange
        string crib1 = "alsu";
        string crib2 = "aikls";
        Anagram anagram1 = new Anagram("alus", crib1);
        Anagram anagram2 = new Anagram("kalis", crib2);

        // Act
        bool result = _anagramService.IsEqualCrib(anagram1, anagram2);

        // Assert
        Assert.IsFalse(result);
    }
}