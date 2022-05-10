using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers;

[Controller]
public class SearchInfoController : Controller
{
    private readonly IWordService _wordService;
    public SearchInfoController(IWordService wordService)
    {
        _wordService = wordService;
    }
    
    public IActionResult Details()
    {
        return View("Details", new SearchInfoList(_wordService.GetAnagramSearchInfo().ToList()));
    }

    public IActionResult ClearTable()
    {
        _wordService.ClearTable("SearchInfo");
        return View("Details", new SearchInfoList(new List<SearchInfo>()));
    }
}