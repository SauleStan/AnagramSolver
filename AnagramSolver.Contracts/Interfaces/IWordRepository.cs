using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces;

public interface IWordRepository
{
    IEnumerable<WordModel> GetWords();
    IEnumerable<WordModel> GetFilteredWords(string filter);
    bool AddWord(string word);
    bool AddWords(IEnumerable<string> words);
    bool CacheWord(string word, IEnumerable<string> anagrams);
    CachedWord GetCachedWord(string input);
    IEnumerable<SearchInfo> GetAnagramSearchInfo();
    bool AddAnagramSearchInfo(SearchInfo searchInfo);
    bool ClearSearchInfoTable(string tableName);
}