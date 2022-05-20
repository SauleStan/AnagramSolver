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
    
    public async Task<IEnumerable<string?>> GetWordsAsync()
    {
        var words = await _wordRepository.GetWordsAsync();
        return words.Select(word => word.Name);
    }

    public async Task<IEnumerable<string?>> GetWordAsync(string word)
    {
        var words = await _wordRepository.GetWordsAsync();
        return words.Where(repoWord => repoWord.Name!.Equals(word))
            .Select(fetchedWord => fetchedWord.Name);
    }

    public IEnumerable<string> GetFilteredWords(string filter)
    {
        filter = filter.Insert(0, "%");
        filter += "%";
        var words = _wordRepository.GetFilteredWords(filter).Select(word => word.Name);
        return words!;
    }

    public async Task<ActionResult> AddWordAsync(string word)
    {
        try
        {
            var words = await _wordRepository.GetWordsAsync();
            if (words.Any(x => x.Name == word))
            {
                return new ActionResult
                {
                    IsSuccessful = false,
                    Error = $"{word} already exists."
                };
            }

            if (await _wordRepository.AddWordAsync(word) != true)
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

    public async Task<ActionResult> EditAsync(string wordToEdit, string editedWord)
    {
        try
        {
            await _wordRepository.EditWordAsync(wordToEdit, editedWord);
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

    public async Task<ActionResult> DeleteWordAsync(string word)
    {
        try
        {
            await _wordRepository.DeleteWordAsync(word);
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

    public async Task<CachedWord> GetCachedWordAsync(string input)
    {
        return await _wordRepository.GetCachedWordAsync(input);
    }

    public async Task<IEnumerable<SearchInfo>> GetAnagramSearchInfoAsync()
    {
        return await _wordRepository.GetAnagramSearchInfoAsync();
    }

    public async Task<ActionResult> AddAnagramSearchInfoAsync(SearchInfo searchInfo)
    {
        try
        {
            await _wordRepository.AddAnagramSearchInfoAsync(searchInfo);
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