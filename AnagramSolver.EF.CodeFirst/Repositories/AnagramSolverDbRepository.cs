using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.EF.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace AnagramSolver.EF.CodeFirst.Repositories;

public class AnagramSolverDbRepository : IWordRepository
{
    private readonly AnagramSolverDbContext _context;
    
    public AnagramSolverDbRepository(AnagramSolverDbContext context)
    {
        _context = context;
    }
    public IEnumerable<Word> GetWords()
    {
        return _context.WordEntities.Select(entity => new Word()
        {
            Id = entity.Id,
            Name = entity.Name
        });
    }

    public IEnumerable<Word> GetFilteredWords(string filter)
    {
        throw new NotImplementedException();
    }

    public bool AddWord(string word)
    {
        try
        {
            _context.Add(new WordEntity()
            {
                Name = word
            });
            _context.SaveChanges();
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
            var wordToUpdate = _context.WordEntities.FirstOrDefault(dbWord => dbWord.Name.Equals(wordToEdit));
            if (wordToUpdate != null)
            {
                wordToUpdate.Name = editedWord;
                _context.Update(wordToUpdate);
                _context.SaveChanges();
            }
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
            var wordToDelete = _context.WordEntities.FirstOrDefault(dbWord => dbWord.Name.Equals(word));
            _context.Remove(wordToDelete);
            _context.SaveChanges();
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
            _context.AddRange(words.Select(word=>new WordEntity()
            {
                Name = word
            }));
            _context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool CacheWord(string word, IEnumerable<string> anagrams)
    {
        try
        {
            foreach (var anagram in anagrams)
            {
                var dbAnagram = _context.WordEntities.FirstOrDefault(dbAnagram => dbAnagram.Name.Equals(anagram));
                _context.Add(new CachedWordEntity()
                {
                    InputWord = word,
                    AnagramId = dbAnagram.Id
                });
                _context.SaveChanges();
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public CachedWord GetCachedWord(string input)
    {
        var cachedWords = _context.CachedWordEntities
            .Include(word => word.Anagram)
            .Where(word => word.InputWord.Equals(input));
        var cachedWord = new CachedWord();
        
        foreach (var dbCachedWord in cachedWords)
        {
            cachedWord.Anagrams.Add(dbCachedWord.Anagram.Name);
        }

        return cachedWord;
    }

    public IEnumerable<SearchInfo> GetAnagramSearchInfo()
    {
        return _context.SearchInfoEntities.Include(info => info.Anagram).AsEnumerable()
            .Select(info =>
            {
                var newInfo = new SearchInfo()
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
                         _context.WordEntities.FirstOrDefault(word => word.Name.Equals(anagram))))
            {
                _context.Add(new SearchInfoEntity()
                {
                    Id = searchInfo.Id,
                    UserIp = searchInfo.UserIp,
                    SearchedWord = searchInfo.SearchedWord,
                    ExecTime = (TimeSpan)searchInfo.ExecTime,
                    AnagramId = dbAnagram.Id
                });
                _context.SaveChanges();
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