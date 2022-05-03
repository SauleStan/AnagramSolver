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
    
    public IActionResult DisplayWords(int? pageNumber)
    {
        HashSet<string> words = _wordService.GetWords();
        int pageSize = 100;

        ViewBag.Words = PaginatedHashSet<HashSet<string>>.Create(words, pageNumber ?? 1 ,pageSize);

        return View();
    }
}