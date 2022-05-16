using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IWordService
{
    public IEnumerable<string> GetWords();
    public IEnumerable<string> GetFilteredWords(string filter);
    WordResult AddWord(string word);
    WordResult Edit(string wordToEdit, string editedWord);
    WordResult DeleteWord(string word);
    bool CacheWord(string word, IEnumerable<string> anagrams);
    CachedWord GetCachedWord(string input);
    IEnumerable<SearchInfo> GetAnagramSearchInfo();
    bool AddAnagramSearchInfo(SearchInfo searchInfo);
    bool ClearTable(string tableName);
}