using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Services;

public class WordService : IWordService
{
    private readonly IWordRepository _wordRepository;

    public WordService(IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }
    
    public IEnumerable<string> GetWords()
    {
        var words = _wordRepository.GetWords().Select(word => word.Name);
        return words;
    }

    public IEnumerable<string> GetFilteredWords(string filter)
    {
        filter = filter.Insert(0, "%");
        filter += "%";
        var words = _wordRepository.GetFilteredWords(filter).Select(word => word.Name);
        return words;
    }

    public bool AddWord(string word)
    {
        try
        {
            if (_wordRepository.GetWords().Any(x => x.Name == word))
            {
                throw new ArgumentException($"{word} already exists.");
            }

            if (!_wordRepository.AddWord(word))
            {
                throw new Exception("Failed to add the word.");
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool CacheWord(string word, IEnumerable<string> anagrams)
    {
        return _wordRepository.CacheWord(word, anagrams);
    }

    public CachedWord GetCachedWord(string input)
    {
        return _wordRepository.GetCachedWord(input);
        }

    public IEnumerable<SearchInfo> GetAnagramSearchInfo()
    {
        return _wordRepository.GetAnagramSearchInfo();
    }

    public bool AddAnagramSearchInfo(SearchInfo searchInfo)
    {
        return _wordRepository.AddAnagramSearchInfo(searchInfo);
    }

    public bool ClearTable(string tableName)
    {
        return _wordRepository.ClearSearchInfoTable(tableName);
    }
}