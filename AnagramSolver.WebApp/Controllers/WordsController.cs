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
    
    public async Task<IActionResult> DisplayWords(int pageNumber = 1)
    {
        return View("Words", PaginatedList<HashSet<string>>.Create((await _wordService.GetWordsAsync())!, pageNumber, PageSize));
    }
    public async Task<IActionResult> DisplayWord(string word, int pageNumber = 1)
    {
        return View("Words", PaginatedList<HashSet<string>>.Create((await _wordService.GetWordAsync(word))!, pageNumber, PageSize));
    }
    
    [HttpPost]
    public async Task<IActionResult> DisplayFilteredWords(string searchInputModel, int pageNumber = 1)
    {
        return View("Words", PaginatedList<HashSet<string>>.Create(await _wordService.GetFilteredWordsAsync(searchInputModel), pageNumber, PageSize));
    }
    
    public IActionResult EditWord(string wordToEdit)
    {
        return View("Edit", new EditWordModel
        {
            WordToEdit = wordToEdit
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> EditWord(EditWordModel word)
    {
        await _wordService.EditWordAsync(word.WordToEdit, word.EditedWord);
        return RedirectToAction("DisplayWord", new { word = word.EditedWord});
    }

    public async Task<IActionResult> DeleteWord(string wordToDelete)
    {
        var result = await _wordService.DeleteWordAsync(wordToDelete);
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