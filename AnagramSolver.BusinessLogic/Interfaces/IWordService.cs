using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IWordService
{
    Task<IEnumerable<string?>> GetWordsAsync();
    Task<IEnumerable<string?>> GetWordAsync(string word);
    Task<ActionResult> AddWordAsync(string word);
    Task<ActionResult> EditWordAsync(string wordToEdit, string editedWord);
    Task<ActionResult> DeleteWordAsync(string word);
}