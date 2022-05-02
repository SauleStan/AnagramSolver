using System.Reflection;
using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers;

[Controller]
public class HomeController : Controller
{
    private static readonly string? DirectoryPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    private static readonly string Path = DirectoryPath + "\\Resources\\zodynas.txt";
    private readonly IAnagramSolver _anagramSolver = new AnagramController(new AnagramService(), new WordService(Path));

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