using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IWordService
{
    Task<IEnumerable<string?>> GetWordsAsync();
    Task<IEnumerable<string?>> GetWordAsync(string word);
    Task<WordResult> AddWordAsync(string word);
    Task<WordResult> EditWordAsync(string wordToEdit, string editedWord);
    Task<WordResult> DeleteWordAsync(string word);
}