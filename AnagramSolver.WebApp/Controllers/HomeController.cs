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

    public IActionResult GetAnagrams(string input)
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();
        var anagramList = GetCachedWordList(input);
        stopwatch.Stop();

        _wordService.AddAnagramSearchInfo(new SearchInfo()
        {
            UserIp = "123",
            ExecTime = stopwatch.Elapsed,
            SearchedWord = input,
            Anagrams = new List<string>(anagramList.Anagrams)
        });
        
        return View("Anagrams", anagramList);
    }
    
    [HttpPost]
    public IActionResult GetAnagrams([Bind("Input")]InputModel inputModel)
    {
        if (!ModelState.IsValid) return View("Index");

        Stopwatch stopwatch = new();
        stopwatch.Start();
        var anagramList = GetCachedWordList(inputModel.Input);
        stopwatch.Stop();
        
        _wordService.AddAnagramSearchInfo(new SearchInfo()
        {
            UserIp = "123",
            ExecTime = stopwatch.Elapsed,
            SearchedWord = inputModel.Input,
            Anagrams = new List<string>(anagramList.Anagrams)
        });
        
        return View("Anagrams", anagramList);
    }

    private AnagramList GetCachedWordList(string input)
    {
        var cachedWords = _wordService.GetCachedWords().Where(cachedWord => cachedWord.InputWord.Equals(input));
        var enumerable = cachedWords.ToList();
        if (enumerable.Count == 0)
        {
            var anagramList = _anagramResolver.FindAnagrams(input);
            _wordService.CacheWord(input, anagramList);
            return new AnagramList(anagramList, input);
        }
        CachedWord cachedWord = enumerable.First(cached => cached.InputWord.Equals(input));
        return new AnagramList(cachedWord.Anagrams, input);
    }
}