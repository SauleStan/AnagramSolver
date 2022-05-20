using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using Microsoft.EntityFrameworkCore;
namespace AnagramSolver.EF.DatabaseFirst.Repositories;

public class WordDatabaseRepository : IWordRepository
{
    private readonly AnagramDbContext _context;
    public WordDatabaseRepository(AnagramDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Contracts.Models.Word>> GetWordsAsync()
    {
        return await _context.Words.Select(word => new Contracts.Models.Word()
        {
            Id = word.Id,
            Name = word.Name
        }).ToListAsync();
    }

    public Task<IEnumerable<Contracts.Models.Word>> GetFilteredWordsAsync(string filter)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddWordAsync(string word)
    {
        try
        {
            await _context.AddAsync(new Word()
            {
                Name = word
            });
            await _context.SaveChangesAsync();
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
            var wordToUpdate = await _context.Words.FirstOrDefaultAsync(dbWord => dbWord.Name.Equals(wordToEdit));
            if (wordToUpdate != null)
            {
                wordToUpdate.Name = editedWord;
                _context.Update(wordToUpdate);
                await _context.SaveChangesAsync();
            }
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
            var wordToDelete = await _context.Words.FirstOrDefaultAsync(dbWord => dbWord.Name!.Equals(word));
            if (wordToDelete != null)
            {
                _context.Remove(wordToDelete);
            }
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Task<bool> AddWordsAsync(IEnumerable<string> words)
    {
        throw new NotImplementedException();
    }

    public async Task CacheWordAsync(string word, IEnumerable<string> anagrams)
    {
        try
        {
            foreach (var anagram in anagrams)
            {
                var dbAnagram = await _context.Words.FirstOrDefaultAsync(dbAnagram => dbAnagram.Name!.Equals(anagram));
                await _context.AddAsync(new EF.DatabaseFirst.Models.CachedWord()
                {
                    InputWord = word,
                    AnagramWordId = dbAnagram?.Id
                });
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Contracts.Models.CachedWord> GetCachedWordAsync(string input)
    {
        var cachedWords = await _context.CachedWords.Include(word => word.AnagramWord)
            .Where(word => word.InputWord!.Equals(input)).ToListAsync();
        var cachedWord = new Contracts.Models.CachedWord();
        
        foreach (var dbCachedWord in cachedWords)
        {
            cachedWord.Anagrams.Add(dbCachedWord.AnagramWord.Name);
        }

        return cachedWord;
    }

    public async Task<IEnumerable<Contracts.Models.SearchInfo>> GetAnagramSearchInfoAsync()
    {
        var searchInfos = await _context.SearchInfos.Include(info => info.Anagram).ToListAsync();
        return searchInfos.Select(info =>
            {
                var newInfo = new Contracts.Models.SearchInfo()
                {
                    Id = info.Id,
                    SearchedWord = info.SearchedWord!,
                    ExecTime = (TimeSpan)info.ExecTime,
                    UserIp = info.UserIp!
                };
                if (info.Anagram != null)
                {
                    newInfo.Anagrams.Add(info.Anagram.Name!);
                }

                return newInfo;
            });
    }

    public async Task AddAnagramSearchInfoAsync(Contracts.Models.SearchInfo searchInfo)
    {
        try
        {
            foreach (var dbAnagram in searchInfo.Anagrams.Select(async anagram =>
                         await _context.Words.FirstOrDefaultAsync(word => word.Name!.Equals(anagram))))
            {
                var anagram = await dbAnagram;
                await _context.AddAsync(new SearchInfo()
                {
                    Id = searchInfo.Id,
                    UserIp = searchInfo.UserIp,
                    SearchedWord = searchInfo.SearchedWord,
                    ExecTime = searchInfo.ExecTime,
                    AnagramId = anagram?.Id
                });
            }
            await _context.SaveChangesAsync();
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