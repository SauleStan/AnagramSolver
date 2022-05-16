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

    public IEnumerable<Contracts.Models.Word> GetWords()
    {
        return _context.Words.AsEnumerable().Select(word => new Contracts.Models.Word()
        {
            Id = word.Id,
            Name = word.Name
        });
    }

    public IEnumerable<Contracts.Models.Word> GetFilteredWords(string filter)
    {
        throw new NotImplementedException();
    }

    public bool AddWord(string word)
    {
        try
        {
            _context.Add(new Word()
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
            var wordToUpdate = _context.Words.FirstOrDefault(dbWord => dbWord.Name.Equals(wordToEdit));
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
            var wordToDelete = _context.Words.FirstOrDefault(dbWord => dbWord.Name.Equals(word));
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
        throw new NotImplementedException();
    }

    public bool CacheWord(string word, IEnumerable<string> anagrams)
    {
        try
        {
            foreach (var anagram in anagrams)
            {
                var dbAnagram = _context.Words.FirstOrDefault(dbAnagram => dbAnagram.Name.Equals(anagram));
                _context.Add(new EF.DatabaseFirst.Models.CachedWord()
                {
                    InputWord = word,
                    AnagramWordId = dbAnagram.Id
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

    public Contracts.Models.CachedWord GetCachedWord(string input)
    {
        var cachedWords = _context.CachedWords.Include(word => word.AnagramWord).Where(word => word.InputWord.Equals(input));
        var cachedWord = new Contracts.Models.CachedWord();
        
        foreach (var dbCachedWord in cachedWords)
        {
            cachedWord.Anagrams.Add(dbCachedWord.AnagramWord.Name);
        }

        return cachedWord;
    }

    public IEnumerable<Contracts.Models.SearchInfo> GetAnagramSearchInfo()
    {
        return _context.SearchInfos.Include(info => info.Anagram).AsEnumerable()
            .Select(info =>
            {
                var newInfo = new Contracts.Models.SearchInfo()
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

    public bool AddAnagramSearchInfo(Contracts.Models.SearchInfo searchInfo)
    {
        try
        {
            foreach (var dbAnagram in searchInfo.Anagrams.Select(anagram =>
                         _context.Words.FirstOrDefault(word => word.Name.Equals(anagram))))
            {
                _context.Add(new SearchInfo()
                {
                    Id = searchInfo.Id,
                    UserIp = searchInfo.UserIp,
                    SearchedWord = searchInfo.SearchedWord,
                    ExecTime = searchInfo.ExecTime,
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