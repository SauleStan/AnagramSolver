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
        return View("Words",PaginatedHashSet<HashSet<string>>.Create(_wordService.GetWords(), pageNumber, PageSize));
    }
    
    public IActionResult SaveNewWord()
    {
        return View("NewWord");
    }
    
    [HttpPost]
    public IActionResult SaveNewWord([Bind("Input")]InputModel inputModel)
    {
        if (inputModel is null || !ModelState.IsValid)
        {
            return View("NewWord");
        }
        
        bool result = _wordService.AddWord(inputModel.Input);
        ViewBag.SaveStatus = result;

        return View("NewWord");
    }
}