using AnagramSolver.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers;

[Controller]
public class WordsController : Controller
{
    private readonly IWordService _wordService;

    public WordsController(IWordService wordService)
    {
        _wordService = wordService;
    }
    
    public IActionResult DisplayWords()
    {
        var words = _wordService.GetWords();
        ViewBag.Words = words.Take(100);
        return View();
    }
}