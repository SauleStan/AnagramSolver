using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces;

public interface IWordRepository
{
    Task<IEnumerable<Word>> GetWordsAsync();
    IEnumerable<Word> GetFilteredWords(string filter);
    Task<bool> AddWordAsync(string word);
    void EditWord(string wordToEdit, string editedWord);
    Task DeleteWordAsync(string word);
    bool AddWords(IEnumerable<string> words);
    Task CacheWord(string word, IEnumerable<string> anagrams);
    Task<CachedWord> GetCachedWordAsync(string input);
    Task<IEnumerable<SearchInfo>> GetAnagramSearchInfoAsync();
    Task AddAnagramSearchInfo(SearchInfo searchInfo);
    bool ClearSearchInfoTable(string tableName);
}