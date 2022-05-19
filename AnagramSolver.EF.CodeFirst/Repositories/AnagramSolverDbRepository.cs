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
    public IEnumerable<Word> GetWords()
    {
        return _dbContext.WordEntities.Select(entity => new Word
        {
            Id = entity.Id,
            Name = entity.Name
        });
    }

    public IEnumerable<Word> GetFilteredWords(string filter)
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

    public void EditWord(string wordToEdit, string editedWord)
    {
        try
        {
            var wordToUpdate = _dbContext.WordEntities.FirstOrDefault(dbWord => dbWord.Name.Equals(wordToEdit));
            if (wordToUpdate == null) return;
            
            wordToUpdate.Name = editedWord;
            _dbContext.Update(wordToUpdate);
            _dbContext.SaveChanges();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void DeleteWord(string word)
    {
        try
        {
            var wordToDelete = _dbContext.WordEntities.FirstOrDefault(dbWord => dbWord.Name.Equals(word));
            if (wordToDelete != null)
            {
                _dbContext.Remove(wordToDelete);
            }
            _dbContext.SaveChanges();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public bool AddWords(IEnumerable<string> words)
    {
        try
        {
            _dbContext.AddRange(words.Select(word=>new WordEntity
            {
                Name = word
            }));
            _dbContext.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public void CacheWord(string word, IEnumerable<string> anagrams)
    {
        try
        {
            foreach (var anagram in anagrams)
            {
                var dbAnagram = _dbContext.WordEntities.FirstOrDefault(dbAnagram => dbAnagram.Name.Equals(anagram));
                _dbContext.Add(new CachedWordEntity()
                {
                    InputWord = word,
                    AnagramId = dbAnagram?.Id
                });
                _dbContext.SaveChanges();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public CachedWord GetCachedWord(string input)
    {
        var cachedWords = _dbContext.CachedWordEntities
            .Include(word => word.Anagram)
            .Where(word => word.InputWord.Equals(input));
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

    public IEnumerable<SearchInfo> GetAnagramSearchInfo()
    {
        return _dbContext.SearchInfoEntities.Include(info => info.Anagram).AsEnumerable()
            .Select(info =>
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

    public bool AddAnagramSearchInfo(SearchInfo searchInfo)
    {
        try
        {
            foreach (var dbAnagram in searchInfo.Anagrams.Select(anagram =>
                         _dbContext.WordEntities.FirstOrDefault(word => word.Name.Equals(anagram))))
            {
                _dbContext.Add(new SearchInfoEntity()
                {
                    Id = searchInfo.Id,
                    UserIp = searchInfo.UserIp,
                    SearchedWord = searchInfo.SearchedWord,
                    ExecTime = searchInfo.ExecTime,
                    AnagramId = dbAnagram!.Id
                });
                _dbContext.SaveChanges();
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool ClearSearchInfoTable(string tableName)
    {
        throw new NotImplementedException();
    }
}