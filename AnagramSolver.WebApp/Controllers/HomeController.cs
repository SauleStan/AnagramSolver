using AnagramSolver.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers;

[Controller]
public class HomeController : Controller
{
    private readonly IAnagramSolver _anagramSolver;

    public HomeController(IAnagramSolver anagramSolver)
    {
        _anagramSolver = anagramSolver;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetAnagrams(string input)
    {
        ViewBag.Anagrams = _anagramSolver.FindAnagrams(input);
        return View();
    }
}