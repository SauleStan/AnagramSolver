using System.Collections.Generic;
using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.Contracts.Models;
using NUnit.Framework;
using Moq;

namespace AnagramSolver.Tests;

public class AnagramControllerTests
{
    private readonly Mock<IAnagramService> _anagramServiceMock = new ();
    private readonly Mock<IWordService> _wordServiceMock = new ();
    private readonly HashSet<string> _wordSetMock = new ();
    private readonly HashSet<Anagram> _anagramSetMock = new ();

    [SetUp]
    public void SetUp()
    {
        Anagram anagram1 = new Anagram("alus", "alsu"); 
        Anagram anagram2 = new Anagram("sula", "alsu"); 
        Anagram anagram3 = new Anagram("alkis", "aikls");
        Anagram anagram4 = new Anagram("anagram", "aaagmnr");
        Anagram anagramSentence = new Anagram("nag a ram", "aaagmnr");

        _wordSetMock.Add(anagram1.Name);
        _wordSetMock.Add(anagram2.Name);
        _wordSetMock.Add(anagram3.Name);
        _wordSetMock.Add(anagram4.Name);
        _wordSetMock.Add(anagramSentence.Name);

        _anagramSetMock.Add(anagram1);
        _anagramSetMock.Add(anagram2);
        _anagramSetMock.Add(anagram3);
        _anagramSetMock.Add(anagram4);
        _anagramSetMock.Add(anagramSentence);
        
        _wordServiceMock.Setup(x => x.GetWords()).Returns(_wordSetMock);
        _anagramServiceMock.Setup(x => x.ConvertToAnagrams(_wordSetMock)).Returns(_anagramSetMock);
    }
    
    [Test]
    public void FindAnagrams_ReturnsAnagramHashSet_WhenGivenInputWord()
    {
        // Arrange
        AnagramResolver anagramResolver = new (_anagramServiceMock.Object, _wordServiceMock.Object);
        string word = "alus";
        string crib = "alsu";
        _anagramServiceMock.Setup(x => x.ConvertToAnagram(word)).Returns(new Anagram(word, crib));

        // Act
        var result = anagramResolver.FindAnagrams(word);
        
        // Assert
        Assert.IsTrue(result.Contains("sula"));
    }
    [Test]
    public void FindAnagrams_ReturnsEmptyAnagramHashSet_WhenGivenInputWordThatHasNoAnagrams()
    {
        // Arrange
        AnagramResolver anagramResolver = new (_anagramServiceMock.Object, _wordServiceMock.Object);
        string word = "vdsdvs";
        string crib = "ddssvv";
        _anagramServiceMock.Setup(x => x.ConvertToAnagram(word)).Returns(new Anagram(word, crib));

        // Act
        var result = anagramResolver.FindAnagrams(word);
        
        // Assert
        Assert.IsEmpty(result);
    }
    [Test]
    public void FindAnagrams_ReturnsAnagramHashSet_WhenGivenInputSentence()
    {
        // Arrange
        AnagramResolver anagramResolver = new (_anagramServiceMock.Object, _wordServiceMock.Object);
        string sentence = "nag a ram";
        string crib = "aaagmnr";
        _anagramServiceMock.Setup(x => x.ConvertToAnagram(sentence)).Returns(new Anagram(sentence, crib));

        // Act
        var result = anagramResolver.FindAnagrams(sentence);
        
        // Assert
        Assert.IsTrue(result.Contains("anagram"));
    }
}