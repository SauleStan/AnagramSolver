using AnagramSolver.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers;

[Controller]
public class WordsController : Controller
{
    private readonly IWordService _wordService;
    private readonly HashSet<string> _words;

    public WordsController(IWordService wordService)
    {
        _wordService = wordService;
        _words = _wordService.GetWords();

    }
    
    public IActionResult DisplayWords(int? pageNumber)
    {
        int pageSize = 100;

        ViewBag.Words = PaginatedHashSet<HashSet<string>>.Create(_words, pageNumber ?? 1 ,pageSize);

        return View();
    }
}