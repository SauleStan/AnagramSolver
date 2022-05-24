using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers;

[Controller]
public class WordsController : Controller
{
    private readonly IFilterableWordService _filterableWordService;
    private const int PageSize = 100;

    public WordsController(IFilterableWordService filterableWordService)
    {
        _filterableWordService = filterableWordService;

    }
    
    public async Task<IActionResult> DisplayWords(int pageNumber = 1)
    {
        return View("Words", PaginatedList<HashSet<string>>.Create((await _filterableWordService.GetWordsAsync())!, pageNumber, PageSize));
    }
    public async Task<IActionResult> DisplayWord(string word, int pageNumber = 1)
    {
        return View("Words", PaginatedList<HashSet<string>>.Create((await _filterableWordService.GetWordAsync(word))!, pageNumber, PageSize));
    }
    
    [HttpPost]
    public async Task<IActionResult> DisplayFilteredWords(string searchInputModel, int pageNumber = 1)
    {
        return View("Words", PaginatedList<HashSet<string>>.Create(await _filterableWordService.GetFilteredWordsAsync(searchInputModel), pageNumber, PageSize));
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
        await _filterableWordService.EditWordAsync(word.WordToEdit, word.EditedWord);
        return RedirectToAction("DisplayWord", new { word = word.EditedWord});
    }

    public async Task<IActionResult> DeleteWord(string wordToDelete)
    {
        var result = await _filterableWordService.DeleteWordAsync(wordToDelete);
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
        
        var result = await _filterableWordService.AddWordAsync(createWordModel.Word);
        ViewBag.SaveStatus = result.IsSuccessful ? "Word was added successfully" : "Failed to add the word";

        return View("NewWord");
    }
}