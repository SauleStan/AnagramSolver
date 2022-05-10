using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IWordService
{
    public IEnumerable<string> GetWords();
    public IEnumerable<string> GetFilteredWords(string filter);
    bool AddWord(string word);
    bool CacheWord(string word, IEnumerable<string> anagrams);
    CachedWord GetCachedWord(string input);
    IEnumerable<SearchInfo> GetAnagramSearchInfo();
    bool AddAnagramSearchInfo(SearchInfo searchInfo);
    bool ClearTable(string tableName);
}