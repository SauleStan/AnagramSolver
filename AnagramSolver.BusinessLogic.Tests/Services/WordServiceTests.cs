using System.Collections.Generic;
using System.Threading.Tasks;
using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using NUnit.Framework;
using Moq;

namespace AnagramSolver.Tests.Services;

[TestFixture]
public class WordServiceTests
{
    private IWordService _wordService;
    private Mock<IWordRepository> _wordRepositoryMock;
    
    [SetUp]
    public void Setup()
    {
        _wordRepositoryMock = new Mock<IWordRepository>();
        _wordService = new WordService(_wordRepositoryMock.Object);
    }

    [Test]
    public async Task GetWords_ReturnsAllWordNames_WhenCalled()
    {
        // Arrange
        _wordRepositoryMock.Setup(repo => repo.GetWordsAsync()).ReturnsAsync(
            new List<Word>
            {
                new ()
                {
                    Id = 1,
                    Name = "word1"
                },
                new ()
                {
                    Id = 1,
                    Name = "word2"
                }
            });
        var expected = new List<string>{"word1", "word2"};
        
        // Act
        var result = await _wordService.GetWordsAsync();
        
        // Assert
        Assert.That(result, Is.EquivalentTo(expected));
    }
    
    [Test]
    public async Task GetWords_ReturnsInputWordName_WhenInputExistsInStorage()
    {
        // Arrange
        var input = "word2";
        _wordRepositoryMock.Setup(repo => repo.GetWordsAsync()).ReturnsAsync(
            new List<Word>
            {
                new ()
                {
                    Id = 1,
                    Name = "word1"
                },
                new ()
                {
                    Id = 1,
                    Name = "word2"
                }
            });
        var expected = new List<string>{"word2"};
        
        // Act
        var result = await _wordService.GetWordAsync(input);
        
        // Assert
        Assert.That(result, Is.EquivalentTo(expected));
    }
    
    [Test]
    public async Task GetWords_ReturnsEmptyList_WhenInputDoesNotExistInStorage()
    {
        // Arrange
        var input = "word2";
        _wordRepositoryMock.Setup(repo => repo.GetWordsAsync())
            .ReturnsAsync(new List<Word>());
        
        // Act
        var result = await _wordService.GetWordAsync(input);
        
        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void GetFilteredWords_ReturnsListWithWordNames_WhenGivenAFilterWord()
    {
        // Arrange
        var filterWord = "mod";
        var expected = new List<string> { "model", "moderator" };
        
        _wordRepositoryMock.Setup(repo => repo.GetFilteredWords("%mod%"))
            .Returns(new List<Word>
            {
                new()
                {
                    Id = 1,
                    Name = "model"
                },
                new()
                {
                    Id = 2,
                    Name = "moderator"
                }
            });
        
        // Act
        var result = _wordService.GetFilteredWords(filterWord);
        
        // Assert
        Assert.That(result, Is.EquivalentTo(expected));
    }

    [Test]
    public async Task AddWord_ReturnsSuccessfulActionResult_WhenWordIsAdded()
    {
        // Arrange
        var word = "borb";
        _wordRepositoryMock.Setup(repo => repo.AddWordAsync(word)).ReturnsAsync(true);
        
        // Act
        var result = await _wordService.AddWordAsync(word);
        
        // Assert
        Assert.That(result.IsSuccessful, Is.True);
    }
    
    [Test]
    public async Task AddWord_ReturnsFailedActionResult_WhenWordIsNotAdded()
    {
        // Arrange
        var word = "borb";
        _wordRepositoryMock.Setup(repo => repo.AddWordAsync(word)).ReturnsAsync(false);
        
        // Act
        var result = await _wordService.AddWordAsync(word);
        
        // Assert
        Assert.That(result.IsSuccessful, Is.False);
    }
    
    [Test]
    public async Task AddWord_ReturnsFailedActionResult_WhenWordAlreadyExists()
    {
        // Arrange
        var word = "borb";
        _wordRepositoryMock.Setup(repo => repo.GetWordsAsync())
            .ReturnsAsync(new List<Word>
            {
                new()
                {
                    Id = 1,
                    Name = word
                }
            });
        
        // Act
        var result = await _wordService.AddWordAsync(word);
        
        // Assert
        Assert.That(result.IsSuccessful, Is.False);
    }
    
    [Test]
    public async Task GetCachedWord_ReturnsCachedWord_IfItExistsInCache()
    {
        // Arrange
        var input = "word1";
        var expected = new CachedWord
        {
            InputWord = input,
            Anagrams = new List<string> { "1word", "wor1d" }
        };
        _wordRepositoryMock.Setup(repo => repo.GetCachedWordAsync(input))
            .ReturnsAsync(expected);
        
        // Act
        var result = await _wordService.GetCachedWordAsync(input);
        
        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _wordRepositoryMock.Verify(repo => repo.GetCachedWordAsync(input), Times.Exactly(1));
    }
}