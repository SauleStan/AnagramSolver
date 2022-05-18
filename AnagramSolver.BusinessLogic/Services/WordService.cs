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
        return words!;
    }

    public IEnumerable<string?> GetWord(string word)
    {
        return _wordRepository.GetWords().Where(repoWord => repoWord.Name!.Equals(word))
            .Select(fetchedWord => fetchedWord.Name);
    }

    public IEnumerable<string> GetFilteredWords(string filter)
    {
        filter = filter.Insert(0, "%");
        filter += "%";
        var words = _wordRepository.GetFilteredWords(filter).Select(word => word.Name);
        return words!;
    }

    public ActionResult AddWord(string word)
    {
        try
        {
            if (_wordRepository.GetWords().Any(x => x.Name == word))
            {
                return new ActionResult
                {
                    IsSuccessful = false,
                    Error = $"{word} already exists."
                };
            }

            if (!_wordRepository.AddWord(word))
            {
                return new ActionResult
                {
                    IsSuccessful = false,
                    Error = "Failed to add the word."
                };
            }

            return new ActionResult
            {
                IsSuccessful = true
            };
        }
        catch (Exception)
        {
            throw;
        }
    }

    public ActionResult Edit(string wordToEdit, string editedWord)
    {
        try
        {
            _wordRepository.EditWord(wordToEdit, editedWord);
            return new ActionResult
            {
                IsSuccessful = true
            };
        }
        catch (Exception)
        {
            return new ActionResult
            {
                IsSuccessful = false,
                Error = $"Failed to edit word \"{wordToEdit}\""
            };
        }
    }

    public ActionResult DeleteWord(string word)
    {
        try
        {
            _wordRepository.DeleteWord(word);
            return new ActionResult
            {
                IsSuccessful = true
            };
        }
        catch (Exception)
        {
            return new ActionResult
            {
                IsSuccessful = false,
                Error = "Failed to delete word"
            };
        }
    }

    public ActionResult CacheWord(string word, IEnumerable<string> anagrams)
    {
        try
        {
            _wordRepository.CacheWord(word, anagrams);
            return new ActionResult
            {
                IsSuccessful = true
            };
        }
        catch (Exception)
        {
            return new ActionResult
            {
                IsSuccessful = false,
                Error = "Failed to cache the word"
            };
        }
    }

    public CachedWord GetCachedWord(string input)
    {
        return _wordRepository.GetCachedWord(input);
    }

    public IEnumerable<SearchInfo> GetAnagramSearchInfo()
    {
        return _wordRepository.GetAnagramSearchInfo();
    }

    public ActionResult AddAnagramSearchInfo(SearchInfo searchInfo)
    {
        try
        {
            _wordRepository.AddAnagramSearchInfo(searchInfo);
            return new ActionResult
            {
                IsSuccessful = true
            };
        }
        catch (Exception)
        {
            return new ActionResult
            {
                IsSuccessful = false,
                Error = "Failed to add search info"
            };
        }
    }

    public bool ClearTable(string tableName)
    {
        return _wordRepository.ClearSearchInfoTable(tableName);
    }
}