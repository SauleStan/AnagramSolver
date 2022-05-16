using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers;

[Controller]
public class WordsController : Controller
{
    private readonly IWordService _wordService;
    private const int PageSize = 100;

    public WordsController(IWordService wordService)
    {
        _wordService = wordService;

    }
    
    public IActionResult DisplayWords(int pageNumber = 1)
    {
        return View("Words", PaginatedList<HashSet<string>>.Create(_wordService.GetWords(), pageNumber, PageSize));
    }
    
    [HttpPost]
    public IActionResult DisplayFilteredWords(string searchInputModel, int pageNumber = 1)
    {
        return View("Words", PaginatedList<HashSet<string>>.Create(_wordService.GetFilteredWords(searchInputModel), pageNumber, PageSize));
    }
    
    public IActionResult EditWord(string wordToEdit)
    {
        return View("Edit", new EditWordModel()
        {
            WordToEdit = wordToEdit
        });
    }
    
    [HttpPost]
    public IActionResult EditWord(EditWordModel word, int pageNumber = 1)
    {
        var result = _wordService.Edit(word.WordToEdit, word.EditedWord);
        if (result.IsSuccessful)
        {
            return View("Words", PaginatedList<HashSet<string>>.Create(_wordService
                .GetWords(), 417, PageSize));
        }
        return View("Words",PaginatedList<HashSet<string>>.Create(_wordService
            .GetWords(), pageNumber, PageSize));
    }
    
    public IActionResult SaveNewWord()
    {
        return View("NewWord");
    }
    
    [HttpPost]
    public IActionResult SaveNewWord([Bind("Word")]CreateWordModel createWordModel)
    {
        if (createWordModel is null || !ModelState.IsValid)
        {
            return View("NewWord");
        }
        
        var result = _wordService.AddWord(createWordModel.Word);
        ViewBag.SaveStatus = result.IsSuccessful ? "Word was added successfully" : "Failed to add the word";

        return View("NewWord");
    }
}