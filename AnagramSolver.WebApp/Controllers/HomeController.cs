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
        ViewBag.Anagrams = _anagramResolver.FindAnagrams(input);
        return View();
    }
    
    [HttpPost]
    public IActionResult GetAnagrams([Bind("Input")]AnagramModel anagramModel, int pageNumber = 1, int pageSize = 20)
    {
        if (ModelState.IsValid)
        {
            return View(PaginatedHashSet<string>.Create(_anagramResolver.FindAnagrams(anagramModel.Input), pageNumber, pageSize));
        }

        return View("Index");
    }
}