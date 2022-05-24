using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces;

public interface IWord
{
    Task<IEnumerable<Word>> GetWordsAsync();
    Task<bool> AddWordAsync(string word);
    Task<bool> AddWordsAsync(IEnumerable<string> words);
}