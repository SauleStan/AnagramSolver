using AnagramSolver.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnagramController : ControllerBase
{
    private readonly IAnagramResolver _anagramResolver;
    private readonly IWordService _wordService;
    
    public AnagramController(IAnagramResolver anagramResolver, IWordService wordService)
    {
        _anagramResolver = anagramResolver;
        _wordService = wordService;
    }
    
    [HttpGet("{input}")]
    public List<string> GetAnagrams(string input)
    {
        return _anagramResolver.FindAnagrams(input).ToList();
    }
}