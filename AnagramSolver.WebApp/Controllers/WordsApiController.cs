using AnagramSolver.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers;

[Route("api/Words")]
[ApiController]
public class WordsApiController : ControllerBase
{
    private readonly IWordService _wordService;
    public WordsApiController(IWordService wordService)
    {
        _wordService = wordService;
    }

    [HttpGet]
    public async Task<List<string>> GetWords()
    {
        var words = await _wordService.GetWordsAsync();
        return words.ToList()!;
    }
}