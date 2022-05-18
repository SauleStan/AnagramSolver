using System.Collections.Generic;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Models;
using NUnit.Framework;

namespace AnagramSolver.Tests.Services;

public class AnagramServiceTests
{
    private readonly AnagramService _anagramService = new AnagramService();
    
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
    public void ConvertToAnagram_ReturnsAnagramWithCorrectCrib_WhenProvidedASentence()
    {
        // Arrange
        string word = "nag a ram";
        string crib = "aaagmnr";

        // Act
        var result = _anagramService.ConvertToAnagram(word);

        // Assert
        Assert.AreEqual(crib, result.Crib);
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