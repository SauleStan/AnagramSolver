using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces;

public interface IWordRepository
{
    Task<IEnumerable<Word>> GetWordsAsync();
    Task<IEnumerable<Word>> GetFilteredWordsAsync(string filter);
    Task<bool> AddWordAsync(string word);
    Task EditWordAsync(string wordToEdit, string editedWord);
    Task DeleteWordAsync(string word);
    Task<bool> AddWordsAsync(IEnumerable<string> words);
    Task CacheWordAsync(string word, IEnumerable<string> anagrams);
    Task<CachedWord> GetCachedWordAsync(string input);
    Task<IEnumerable<SearchInfo>> GetAnagramSearchInfoAsync();
    Task AddAnagramSearchInfoAsync(SearchInfo searchInfo);
    Task<bool> ClearSearchInfoTableAsync(string tableName);
}