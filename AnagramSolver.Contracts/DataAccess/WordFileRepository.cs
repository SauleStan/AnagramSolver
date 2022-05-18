using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.DataAccess;

public class WordFileRepository : IWordRepository
{
    private readonly HashSet<Word> _words = new ();
    private readonly string _path;
    private const string Separator = "/t";
    public WordFileRepository(string path)
    {
        _path = path;
    }
    public IEnumerable<Word> GetWords()
    {
        
        using (var streamReader = new StreamReader(_path))
        {
            while (streamReader.ReadLine() is { } line)
            {
                var word = line.Split(Separator)[0];
                if (_words.All(x => x.Name != word))
                {
                    _words.Add(new Word(word));
                }
            }
        }
        return _words;
    }

    public IEnumerable<Word> GetFilteredWords(string filter)
    {
        using (var sr = new StreamReader(_path))
        {
            while (sr.ReadLine() is { } line)
            {
                var word = line.Split(Separator)[0];
                if (_words.All(x => x.Name != word) && word.Contains(filter))
                {
                    _words.Add(new Word(word));
                }
            }
        }
        return _words;
    }

    public bool AddWord(string word)
    {
        try
        {
            File.AppendAllText(_path, word + Environment.NewLine);
            return true;
        }
        catch (FileNotFoundException)
        {
            return false;
        }
    }

    public void EditWord(string wordToEdit, string editedWord)
    {
        throw new NotImplementedException();
    }

    public void DeleteWord(string word)
    {
        throw new NotImplementedException();
    }

    public bool AddWords(IEnumerable<string> words)
    {
        try
        {
            File.AppendAllLines(_path, words);
            return true;
        }
        catch (FileNotFoundException)
        {
            return false;
        }
    }

    public void CacheWord(string word, IEnumerable<string> anagrams)
    {
        throw new NotImplementedException();
    }

    public CachedWord GetCachedWord(string input)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<SearchInfo> GetAnagramSearchInfo()
    {
        throw new NotImplementedException();
    }

    public bool AddAnagramSearchInfo(SearchInfo searchInfo)
    {
        throw new NotImplementedException();
    }

    public bool ClearSearchInfoTable(string tableName)
    {
        throw new NotImplementedException();
    }
}