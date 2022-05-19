using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IWordService
{
    public Task<IEnumerable<string?>> GetWordsAsync();
    public Task<IEnumerable<string?>> GetWordAsync(string word);
    public IEnumerable<string> GetFilteredWords(string filter);
    Task<ActionResult> AddWordAsync(string word);
    ActionResult Edit(string wordToEdit, string editedWord);
    ActionResult DeleteWord(string word);
    ActionResult CacheWord(string word, IEnumerable<string> anagrams);
    CachedWord GetCachedWord(string input);
    IEnumerable<SearchInfo> GetAnagramSearchInfo();
    ActionResult AddAnagramSearchInfo(SearchInfo searchInfo);
    bool ClearTable(string tableName);
}