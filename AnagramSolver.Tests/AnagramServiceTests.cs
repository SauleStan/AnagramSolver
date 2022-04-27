using System.Collections.Generic;
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
    
    [Test]
    public void ConvertToAnagram_ReturnsAnagram_WhenProvidedAString()
    {
        // Arrange
        string word = "kalis";

        // Act
        var result = _anagramService.ConvertToAnagram(word);

        // Assert
        Assert.IsInstanceOf<Anagram>(result);
    }
    
    [Test]
    public void ConvertToAnagram_ReturnsAnagramWithCorrectCrib_WhenProvidedAString()
    {
        // Arrange
        string word = "alus";
        string crib = "alsu";

        // Act
        var result = _anagramService.ConvertToAnagram(word);

        // Assert
        Assert.AreEqual(result.Crib, crib);
    }
    
    [Test]
    public void ConvertToAnagrams_ReturnsAnagramHashSet_WhenProvidedStringHashSet()
    {
        // Arrange
        HashSet<string> words = new HashSet<string>();
        words.Add("alkis");
        words.Add("alus");

        // Act
        var result = _anagramService.ConvertToAnagrams(words);

        // Assert
        Assert.IsInstanceOf<HashSet<Anagram>>(result);
    }
    [Test]
    public void ConvertToAnagrams_ReturnsAnagramHashSetWithCorrectCribs_WhenProvidedStringHashSet()
    {
        // Arrange
        HashSet<string> words = new HashSet<string>();
        words.Add("alkis");
        words.Add("alus");
        
        HashSet<string> cribs = new HashSet<string>();
        cribs.Add("aikls");
        cribs.Add("alsu");
        
        var resultCribs = new HashSet<string>();

        // Act
        var result = _anagramService.ConvertToAnagrams(words);
        foreach (var anagram in result)
        {
            resultCribs.Add(anagram.Crib);
        }

        // Assert
        Assert.AreEqual(cribs, resultCribs);
    }
}