using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers;

[Controller]
public class HomeController : Controller
{
    private readonly IAnagramResolver _anagramResolver;

    public HomeController(IAnagramResolver anagramResolver)
    {
        _anagramResolver = anagramResolver;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetAnagrams(string input)
    {
        return View("Anagrams", new AnagramList(_anagramResolver.FindAnagrams(input)));
    }
    
    [HttpPost]
    public IActionResult GetAnagrams([Bind("Input")]InputModel inputModel)
    {
        if (ModelState.IsValid)
        {
            return View("Anagrams", new AnagramList(_anagramResolver.FindAnagrams(inputModel.Input)));
        }

        return View("Index");
    }
}