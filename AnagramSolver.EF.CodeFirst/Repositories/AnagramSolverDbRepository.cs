using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.EF.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace AnagramSolver.EF.CodeFirst.Repositories;

public class AnagramSolverDbRepository : IWordRepository
{
    private readonly AnagramSolverDbContext _dbContext;
    
    public AnagramSolverDbRepository(AnagramSolverDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Word>> GetWordsAsync()
    {
        return await _dbContext.WordEntities.Select(entity => new Word
        {
            Id = entity.Id,
            Name = entity.Name
        }).ToListAsync();
    }

    public Task<IEnumerable<Word>> GetFilteredWordsAsync(string filter)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddWordAsync(string word)
    {
        try
        {
            await _dbContext.AddAsync(new WordEntity
            {
                Name = word
            });
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task EditWordAsync(string wordToEdit, string editedWord)
    {
        try
        {
            var wordToUpdate = await _dbContext.WordEntities.FirstOrDefaultAsync(dbWord => dbWord.Name.Equals(wordToEdit));
            if (wordToUpdate == null) return;
            
            wordToUpdate.Name = editedWord;
            _dbContext.Update(wordToUpdate);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task DeleteWordAsync(string word)
    {
        try
        {
            var wordToDelete = await _dbContext.WordEntities.FirstOrDefaultAsync(dbWord => dbWord.Name.Equals(word));
            if (wordToDelete != null)
            {
                _dbContext.Remove(wordToDelete);
            }
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> AddWordsAsync(IEnumerable<string> words)
    {
        try
        {
            await _dbContext.AddRangeAsync(words.Select(word=>new WordEntity
            {
                Name = word
            }));
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task CacheWordAsync(string word, IEnumerable<string> anagrams)
    {
        try
        {
            foreach (var anagram in anagrams)
            {
                var dbAnagram = _dbContext.WordEntities.FirstOrDefault(dbAnagram => dbAnagram.Name.Equals(anagram));
                await _dbContext.AddAsync(new CachedWordEntity()
                {
                    InputWord = word,
                    AnagramId = dbAnagram?.Id
                });
                await _dbContext.SaveChangesAsync();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<CachedWord> GetCachedWordAsync(string input)
    {
        var cachedWords = await _dbContext.CachedWordEntities
            .Include(word => word.Anagram)
            .Where(word => word.InputWord.Equals(input)).ToListAsync();
        var cachedWord = new CachedWord();
        foreach (var dbCachedWord in cachedWords)
        {
            if (dbCachedWord.Anagram != null)
            {
                cachedWord.Anagrams.Add(dbCachedWord.Anagram.Name);
            }
        }

        return cachedWord;
    }

    public async Task<IEnumerable<SearchInfo>> GetAnagramSearchInfoAsync()
    {
        var searchInfos = await _dbContext.SearchInfoEntities.ToListAsync();
        return searchInfos.Select(info =>
        {
            var newInfo = new SearchInfo
            {
                Id = info.Id,
                SearchedWord = info.SearchedWord,
                ExecTime = info.ExecTime,
                UserIp = info.UserIp
            };
            if (info.Anagram != null)
            {
                newInfo.Anagrams.Add(info.Anagram.Name);
            }

            return newInfo;
        });
    }

    public async Task AddAnagramSearchInfoAsync(SearchInfo searchInfo)
    {
        try
        {
            foreach (var dbAnagramTask in searchInfo.Anagrams.Select(async anagram =>
                        await _dbContext.WordEntities.FirstOrDefaultAsync(word => word.Name.Equals(anagram))))
            {
                var dbAnagram = await dbAnagramTask;
                await _dbContext.AddAsync(new SearchInfoEntity()
                {
                    Id = searchInfo.Id,
                    UserIp = searchInfo.UserIp,
                    SearchedWord = searchInfo.SearchedWord,
                    ExecTime = searchInfo.ExecTime,
                    AnagramId = dbAnagram!.Id
                });
            }
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Task<bool> ClearSearchInfoTableAsync(string tableName)
    {
        throw new NotImplementedException();
    }
}