using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.Contracts.Interfaces;
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
    public void GetAnagrams_ReturnsFoundAnagramsFromCache_WhenProvidedAString()
    {
        // Arrange
        var input = "word";
        _wordServiceMock.Setup(service => service.GetCachedWord(input)).Returns(new CachedWord
        {
            InputWord = input,
            Anagrams = new List<string> { "wrod" }
        });
        
        // Act
        var viewResult = (ViewResult)_homeController.GetAnagrams(input);
        var result = (AnagramList)viewResult.ViewData.Model!;
        // Assert
        Assert.That(viewResult, Is.TypeOf<ViewResult>());
        Assert.That(result.Anagrams.First(), Is.EqualTo("wrod"));
        _wordServiceMock.Verify(serviceMock => serviceMock.GetCachedWord(input), Times.Exactly(1));
    }

    [Test]
    public void GetAnagrams_ReturnsFoundAnagramsFromResolver_WhenInputIsNotCached()
    {
        // Arrange
        var input = "word";
        var expectedList = new List<string> { "wrod", "wodr" };
        _wordServiceMock.Setup(service => service.GetCachedWord(input)).Returns(new CachedWord());
        _anagramResolverMock.Setup(resolverMock => resolverMock.FindAnagrams(input)).Returns(expectedList);
        
        // Act
        var viewResult = (ViewResult)_homeController.GetAnagrams(input);
        var result = (AnagramList)viewResult.ViewData.Model!;
        
        // Assert
        Assert.That(viewResult, Is.TypeOf<ViewResult>());
        Assert.That(result.Anagrams, Is.EquivalentTo(expectedList));
        _wordServiceMock.Verify(serviceMock => serviceMock.GetCachedWord(input), Times.Exactly(1));
        _anagramResolverMock.Verify(resolverMock => resolverMock.FindAnagrams(input), Times.Exactly(1));
    }
}