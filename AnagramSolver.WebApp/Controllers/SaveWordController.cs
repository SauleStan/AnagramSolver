using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers;

public class SaveWordController : Controller
{
    private readonly IWordService _wordService;
    private readonly HashSet<string> _words;

    public SaveWordController(IWordService wordService)
    {
        _wordService = wordService;
        _words = _wordService.GetWords();
    }

    public IActionResult SaveNewWord()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult SaveNewWord([Bind("Input")]AnagramModel anagramModel)
    {
        try
        {
            if (anagramModel.Input.Length is 0)
            {
                return View();
            }

            if (_words.Contains(anagramModel.Input))
            {
                ViewBag.SaveStatus = "Word already exists in the file";
                return View();
            }

            _wordService.AddWord(anagramModel.Input);
            ViewBag.SaveStatus = "Word was added to the file";
        }
        catch (NullReferenceException)
        {
            ViewBag.SaveStatus = "The Input field is required.";
        }
        catch (Exception e)
        {
            ViewBag.SaveStatus = "Something went wrong";
            Console.WriteLine(e.Message);
        }
        return View();
    }
}