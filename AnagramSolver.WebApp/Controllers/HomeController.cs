using System.Diagnostics;
using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers;

[Controller]
public class HomeController : Controller
{
    private readonly IAnagramResolver _anagramResolver;
    private readonly IWordService _wordService;

    public HomeController(IAnagramResolver anagramResolver, IWordService wordService)
    {
        _anagramResolver = anagramResolver;
        _wordService = wordService;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> GetAnagrams(string input)
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();
        var anagramList = await GetCachedWordListAsync(input);
        stopwatch.Stop();

        _wordService.AddAnagramSearchInfo(new SearchInfo
        {
            UserIp = "123",
            ExecTime = stopwatch.Elapsed,
            SearchedWord = input,
            Anagrams = new List<string>(anagramList.Anagrams)
        });
        
        return View("Anagrams", anagramList);
    }
    
    [HttpPost]
    public async Task<IActionResult> GetAnagrams([Bind("Input")]InputModel inputModel)
    {
        if (!ModelState.IsValid) return View("Index");

        Stopwatch stopwatch = new();
        stopwatch.Start();
        var anagramList = await GetCachedWordListAsync(inputModel.Input);
        stopwatch.Stop();
        
        _wordService.AddAnagramSearchInfo(new SearchInfo
        {
            UserIp = "123",
            ExecTime = stopwatch.Elapsed,
            SearchedWord = inputModel.Input,
            Anagrams = new List<string>(anagramList.Anagrams)
        });
        
        return View("Anagrams", anagramList);
    }

    public IActionResult ClearCache()
    {
        if (_wordService.ClearTable("CachedWord"))
        {
            ViewBag.ClearStatus = "Cache has been cleared";
        }
        return View("Index");
    }

    private async Task<AnagramList> GetCachedWordListAsync(string input)
    {
        var cachedWord = await _wordService.GetCachedWordAsync(input);
        if (cachedWord.Anagrams.Count == 0)
        {
            var anagramList = await _anagramResolver.FindAnagramsAsync(input);
            _wordService.CacheWord(input, anagramList);
            return new AnagramList(anagramList, input);
        }
        return new AnagramList(cachedWord.Anagrams, input);
    }
}