using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.Extensions.Logging;
using ICacheable = AnagramSolver.BusinessLogic.Interfaces.ICacheable;
using IClearTable = AnagramSolver.BusinessLogic.Interfaces.IClearTable;
using ISearchInfo = AnagramSolver.BusinessLogic.Interfaces.ISearchInfo;

namespace AnagramSolver.BusinessLogic.Services;

public class WordService : IFilterableWordService, ISearchInfo, IClearTable, ICacheable
{
    private readonly IWordRepository _wordRepository;
    private readonly ILogger<WordService> _logger;

    public WordService(IWordRepository wordRepository, ILogger<WordService> logger)
    {
        _wordRepository = wordRepository;
        _logger = logger;
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

    public async Task<IEnumerable<string>> GetFilteredWordsAsync(string filter)
    {
        filter = filter.Insert(0, "%");
        filter += "%";
        var words = await _wordRepository.GetFilteredWordsAsync(filter);
        return words.Select(word => word.Name)!;
    }

    public async Task<WordResult> AddWordAsync(string word)
    {
        try
        {
            var words = await _wordRepository.GetWordsAsync();
            if (words.Any(x => x.Name == word))
            {
                var result = new WordResult
                {
                    IsSuccessful = false,
                    Error = $"{word} already exists."
                }; 
                _logger.LogError(result.Error);
                return result;
            }

            if (await _wordRepository.AddWordAsync(word) != true)
            {
                var result = new WordResult
                {
                    IsSuccessful = false,
                    Error = "Failed to add the word."
                };
                _logger.LogError(result.Error);
                return result;
            }
            
            _logger.LogInformation($"{word} was added to database");
            return new WordResult
            {
                IsSuccessful = true
            };
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);
            throw;
        }
    }

    public async Task<WordResult> EditWordAsync(string wordToEdit, string editedWord)
    {
        try
        {
            await _wordRepository.EditWordAsync(wordToEdit, editedWord);
            return new WordResult
            {
                IsSuccessful = true
            };
        }
        catch (Exception)
        {
            return new WordResult
            {
                IsSuccessful = false,
                Error = $"Failed to edit word \"{wordToEdit}\""
            };
        }
    }

    public async Task<WordResult> DeleteWordAsync(string word)
    {
        try
        {
            await _wordRepository.DeleteWordAsync(word);
            return new WordResult
            {
                IsSuccessful = true
            };
        }
        catch (Exception)
        {
            return new WordResult
            {
                IsSuccessful = false,
                Error = "Failed to delete word"
            };
        }
    }

    public async Task<WordResult> CacheWordAsync(string word, IEnumerable<string> anagrams)
    {
        try
        {
            await _wordRepository.CacheWordAsync(word, anagrams);
            return new WordResult
            {
                IsSuccessful = true
            };
        }
        catch (Exception)
        {
            return new WordResult
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

    public async Task<WordResult> AddAnagramSearchInfoAsync(SearchInfo searchInfo)
    {
        try
        {
            await _wordRepository.AddAnagramSearchInfoAsync(searchInfo);
            return new WordResult
            {
                IsSuccessful = true
            };
        }
        catch (Exception)
        {
            return new WordResult
            {
                IsSuccessful = false,
                Error = "Failed to add search info"
            };
        }
    }

    public async Task<bool> ClearTableAsync(string tableName)
    {
        return await _wordRepository.ClearTableAsync(tableName);
    }
}