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
    public List<string> GetWords()
    {
        return _wordService.GetWords().ToList()!;
    }
}