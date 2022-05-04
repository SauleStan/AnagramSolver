using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers;

[Controller]
public class WordsController : Controller
{
    private readonly IWordService _wordService;
    private const int PageSize = 100;

    public WordsController(IWordService wordService)
    {
        _wordService = wordService;

    }
    
    public IActionResult DisplayWords(int pageNumber = 1)
    {
        return View(PaginatedHashSet<HashSet<string>>.Create(_wordService.GetWords(), pageNumber, PageSize));
    }
    
    public IActionResult SaveNewWord()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult SaveNewWord([Bind("Input")]AnagramModel anagramModel)
    {
        if (anagramModel is null || !ModelState.IsValid)
        {
            return View();
        }
        
        bool result = _wordService.AddWord(anagramModel.Input);
        ViewBag.SaveStatus = result;

        return View();
    }
}