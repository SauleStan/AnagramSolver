using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IWordService
{
    public IEnumerable<string> GetWords();
    public IEnumerable<string> GetFilteredWords(string filter);
    bool AddWord(string word);
    bool CacheWord(string word, IEnumerable<string> anagrams);
    IEnumerable<CachedWord> GetCachedWords();
    IEnumerable<SearchInfo> GetAnagramSearchInfo();
    bool AddAnagramSearchInfo(SearchInfo searchInfo);
}