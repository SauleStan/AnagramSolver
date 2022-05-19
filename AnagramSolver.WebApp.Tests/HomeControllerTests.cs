using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Controllers;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace AnagramSolver.WebApp.Tests;

[TestFixture]
public class HomeControllerTests
{
    private Mock<IWordService> _wordServiceMock;
    private Mock<IAnagramResolver> _anagramResolverMock;
    private HomeController _homeController;
    
    [SetUp]
    public void Setup()
    {
        _wordServiceMock = new Mock<IWordService>();
        _anagramResolverMock = new Mock<IAnagramResolver>();
        _homeController = new HomeController(_anagramResolverMock.Object, _wordServiceMock.Object);
    }

    [Test]
    public void Index_ReturnsView()
    {
        // Act
        var result = _homeController.Index();
        
        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
    }

    [Test]
    public async Task GetAnagrams_ReturnsFoundAnagramsFromCache_WhenProvidedAString()
    {
        // Arrange
        var input = "word";
        _wordServiceMock.Setup(service => service.GetCachedWordAsync(input)).ReturnsAsync(new CachedWord
        {
            InputWord = input,
            Anagrams = new List<string> { "wrod" }
        });
        
        // Act
        var actionResult = await _homeController.GetAnagrams(input);
        var viewResult = (ViewResult)actionResult;
        var result = (AnagramList)viewResult.ViewData.Model!;
        // Assert
        Assert.That(result.Anagrams.First(), Is.EqualTo("wrod"));
        _wordServiceMock.Verify(serviceMock => serviceMock.GetCachedWordAsync(input), Times.Exactly(1));
    }

    [Test]
    public async Task GetAnagrams_ReturnsFoundAnagramsFromResolver_WhenInputIsNotCached()
    {
        // Arrange
        var input = "word";
        var expectedList = new List<string> { "wrod", "wodr" };
        _wordServiceMock.Setup(service => service.GetCachedWordAsync(input)).ReturnsAsync(new CachedWord());
        _anagramResolverMock.Setup(resolverMock => resolverMock.FindAnagramsAsync(input)).ReturnsAsync(expectedList);
        
        // Act
        var actionResult = await _homeController.GetAnagrams(input);
        var viewResult = (ViewResult)actionResult;
        var result = (AnagramList)viewResult.ViewData.Model!;
        
        // Assert
        Assert.That(result.Anagrams, Is.EquivalentTo(expectedList));
        _wordServiceMock.Verify(serviceMock => serviceMock.GetCachedWordAsync(input), Times.Exactly(1));
        _anagramResolverMock.Verify(resolverMock => resolverMock.FindAnagramsAsync(input), Times.Exactly(1));
    }
    
    [Test]
    public async Task GetAnagrams_ReturnsFoundAnagramsFromCache_WhenProvidedInputModel()
    {
        // Arrange
        var input = "word";
        _wordServiceMock.Setup(service => service.GetCachedWordAsync(input)).ReturnsAsync(new CachedWord
        {
            InputWord = input,
            Anagrams = new List<string> { "wrod" }
        });
        var inputModel = new InputModel
        {
            Input = input
        };
        
        // Act
        var actionResult = await _homeController.GetAnagrams(inputModel);
        var viewResult = (ViewResult)actionResult;
        var result = (AnagramList)viewResult.ViewData.Model!;
        // Assert
        Assert.That(result.Anagrams.First(), Is.EqualTo("wrod"));
        _wordServiceMock.Verify(serviceMock => serviceMock.GetCachedWordAsync(input), Times.Exactly(1));
    }

    [Test]
    public async Task GetAnagrams_ReturnsFoundAnagramsFromResolver_WhenInputModelIsNotCached()
    {
        // Arrange
        var input = "word";
        var expectedList = new List<string> { "wrod", "wodr" };
        _wordServiceMock.Setup(service => service.GetCachedWordAsync(input)).ReturnsAsync(new CachedWord());
        _anagramResolverMock.Setup(resolverMock => resolverMock.FindAnagramsAsync(input)).ReturnsAsync(expectedList);
        var inputModel = new InputModel
        {
            Input = input
        };
        
        // Act
        var actionResult = await _homeController.GetAnagrams(inputModel);
        var viewResult = (ViewResult)actionResult;
        var result = (AnagramList)viewResult.ViewData.Model!;
        
        // Assert
        Assert.That(result.Anagrams, Is.EquivalentTo(expectedList));
        _wordServiceMock.Verify(serviceMock => serviceMock.GetCachedWordAsync(input), Times.Exactly(1));
        _anagramResolverMock.Verify(resolverMock => resolverMock.FindAnagramsAsync(input), Times.Exactly(1));
    }
}