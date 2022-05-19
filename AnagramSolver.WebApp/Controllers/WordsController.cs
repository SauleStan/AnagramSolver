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
        return View("Words", PaginatedList<HashSet<string>>.Create(_wordService.GetWords()!, pageNumber, PageSize));
    }
    public IActionResult DisplayWord(string word, int pageNumber = 1)
    {
        return View("Words", PaginatedList<HashSet<string>>.Create(_wordService.GetWord(word)!, pageNumber, PageSize));
    }
    
    [HttpPost]
    public IActionResult DisplayFilteredWords(string searchInputModel, int pageNumber = 1)
    {
        return View("Words", PaginatedList<HashSet<string>>.Create(_wordService.GetFilteredWords(searchInputModel), pageNumber, PageSize));
    }
    
    public IActionResult EditWord(string wordToEdit)
    {
        return View("Edit", new EditWordModel
        {
            WordToEdit = wordToEdit
        });
    }
    
    [HttpPost]
    public IActionResult EditWord(EditWordModel word)
    {
        _wordService.Edit(word.WordToEdit, word.EditedWord);
        return RedirectToAction("DisplayWord", new { word = word.EditedWord});
    }

    public IActionResult DeleteWord(string wordToDelete)
    {
        var result = _wordService.DeleteWord(wordToDelete);
        if (result.IsSuccessful)
        {
            ViewBag.Status = $"{wordToDelete} has been deleted";
            return View("DeleteDetails");
        }
        ViewBag.Status = result.Error!;
        return View("DeleteDetails");
    }
    
    public IActionResult SaveNewWord()
    {
        return View("NewWord");
    }
    
    [HttpPost]
    public async Task<IActionResult> SaveNewWord([Bind("Word")]CreateWordModel createWordModel)
    {
        if (createWordModel == null || !ModelState.IsValid)
        {
            return View("NewWord");
        }
        
        var result = await _wordService.AddWordAsync(createWordModel.Word);
        ViewBag.SaveStatus = result.IsSuccessful ? "Word was added successfully" : "Failed to add the word";

        return View("NewWord");
    }
}