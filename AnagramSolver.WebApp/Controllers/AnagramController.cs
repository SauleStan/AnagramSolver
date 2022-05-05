using AnagramSolver.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnagramController : ControllerBase
{
    private readonly IAnagramResolver _anagramResolver;
    
    public AnagramController(IAnagramResolver anagramResolver)
    {
        _anagramResolver = anagramResolver;
    }
    
    [HttpGet("{input}")]
    public List<string> GetAnagrams(string input)
    {
        return _anagramResolver.FindAnagrams(input).ToList();
    }
}