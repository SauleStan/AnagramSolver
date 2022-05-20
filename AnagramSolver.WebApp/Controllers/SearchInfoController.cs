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
    
    public async Task<IActionResult> Details()
    {
        var searchInfos = await _wordService.GetAnagramSearchInfoAsync();
        return View("Details", new SearchInfoList(searchInfos.ToList()));
    }

    public IActionResult ClearTable()
    {
        _wordService.ClearTable("SearchInfo");
        return View("Details", new SearchInfoList(new List<SearchInfo>()));
    }
}