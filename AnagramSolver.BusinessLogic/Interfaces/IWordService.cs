using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IWordService
{
    public Task<IEnumerable<string?>> GetWordsAsync();
    public Task<IEnumerable<string?>> GetWordAsync(string word);
    public IEnumerable<string> GetFilteredWords(string filter);
    Task<ActionResult> AddWordAsync(string word);
    Task<ActionResult> EditAsync(string wordToEdit, string editedWord);
    Task<ActionResult> DeleteWordAsync(string word);
    ActionResult CacheWord(string word, IEnumerable<string> anagrams);
    Task<CachedWord> GetCachedWordAsync(string input);
    Task<IEnumerable<SearchInfo>> GetAnagramSearchInfoAsync();
    Task<ActionResult> AddAnagramSearchInfoAsync(SearchInfo searchInfo);
    bool ClearTable(string tableName);
}