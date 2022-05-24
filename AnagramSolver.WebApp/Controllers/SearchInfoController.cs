using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers;

[Controller]
public class SearchInfoController : Controller
{
    private readonly ISearchInfo _searchInfo;
    private readonly IClearTable _clearTable;
    public SearchInfoController(ISearchInfo searchInfo, IClearTable clearTable)
    {
        _searchInfo = searchInfo;
        _clearTable = clearTable;
    }
    
    public async Task<IActionResult> Details()
    {
        var searchInfos = await _searchInfo.GetAnagramSearchInfoAsync();
        return View("Details", new SearchInfoList(searchInfos.ToList()));
    }

    public IActionResult ClearTable()
    {
        _clearTable.ClearTableAsync("SearchInfo");
        return View("Details", new SearchInfoList(new List<SearchInfo>()));
    }
}