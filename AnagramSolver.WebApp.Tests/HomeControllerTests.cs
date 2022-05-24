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
    private Mock<ISearchInfo> _searchInfoMock = null!;
    private Mock<ICacheable> _cacheMock = null!;
    private Mock<IClearTable> _clearTableMock = null!;
    private Mock<IAnagramResolver> _anagramResolverMock = null!;
    private HomeController _homeController = null!;
    
    [SetUp]
    public void Setup()
    {
        _searchInfoMock = new Mock<ISearchInfo>();
        _cacheMock = new Mock<ICacheable>();
        _clearTableMock = new Mock<IClearTable>();
        _anagramResolverMock = new Mock<IAnagramResolver>();
        _homeController = new HomeController(
            _anagramResolverMock.Object,
            _searchInfoMock.Object,
            _cacheMock.Object,
            _clearTableMock.Object
            );
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
        _cacheMock.Setup(service => service.GetCachedWordAsync(input)).ReturnsAsync(new CachedWord
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
        _cacheMock.Verify(serviceMock => serviceMock.GetCachedWordAsync(input), Times.Exactly(1));
    }

    [Test]
    public async Task GetAnagrams_ReturnsFoundAnagramsFromResolver_WhenInputIsNotCached()
    {
        // Arrange
        var input = "word";
        var expectedList = new List<string> { "wrod", "wodr" };
        _cacheMock.Setup(service => service.GetCachedWordAsync(input)).ReturnsAsync(new CachedWord());
        _anagramResolverMock.Setup(resolverMock => resolverMock.FindAnagramsAsync(input)).ReturnsAsync(expectedList);
        
        // Act
        var actionResult = await _homeController.GetAnagrams(input);
        var viewResult = (ViewResult)actionResult;
        var result = (AnagramList)viewResult.ViewData.Model!;
        
        // Assert
        Assert.That(result.Anagrams, Is.EquivalentTo(expectedList));
        _cacheMock.Verify(serviceMock => serviceMock.GetCachedWordAsync(input), Times.Exactly(1));
        _anagramResolverMock.Verify(resolverMock => resolverMock.FindAnagramsAsync(input), Times.Exactly(1));
    }
    
    [Test]
    public async Task GetAnagrams_ReturnsFoundAnagramsFromCache_WhenProvidedInputModel()
    {
        // Arrange
        var input = "word";
        _cacheMock.Setup(service => service.GetCachedWordAsync(input)).ReturnsAsync(new CachedWord
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
        _cacheMock.Verify(serviceMock => serviceMock.GetCachedWordAsync(input), Times.Exactly(1));
    }

    [Test]
    public async Task GetAnagrams_ReturnsFoundAnagramsFromResolver_WhenInputModelIsNotCached()
    {
        // Arrange
        var input = "word";
        var expectedList = new List<string> { "wrod", "wodr" };
        _cacheMock.Setup(service => service.GetCachedWordAsync(input)).ReturnsAsync(new CachedWord());
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
        _cacheMock.Verify(serviceMock => serviceMock.GetCachedWordAsync(input), Times.Exactly(1));
        _anagramResolverMock.Verify(resolverMock => resolverMock.FindAnagramsAsync(input), Times.Exactly(1));
    }
}