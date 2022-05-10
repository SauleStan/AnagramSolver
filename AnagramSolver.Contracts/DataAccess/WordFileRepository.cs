using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.DataAccess;

public class WordFileRepository : IWordRepository
{
    private readonly HashSet<WordModel> _words = new ();
    private readonly string _path;
    public WordFileRepository(string path)
    {
        _path = path;
    }
    public IEnumerable<WordModel> GetWords()
    {
        
        using (var sr = new StreamReader(_path))
        {
            string? line;
            
            while ((line = sr.ReadLine()) != null)
            {
                var word = line.Split("\t")[0];
                if (_words.All(x => x.Name != word))
                {
                    _words.Add(new WordModel(word));
                }
            }
        }
        return _words;
    }

    public IEnumerable<WordModel> GetFilteredWords(string filter)
    {
        using (var sr = new StreamReader(_path))
        {
            string? line;
            
            while ((line = sr.ReadLine()) != null)
            {
                var word = line.Split("\t")[0];
                if (_words.All(x => x.Name != word) && word.Contains(filter))
                {
                    _words.Add(new WordModel(word));
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

    public bool CacheWord(string word, IEnumerable<string> anagrams)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CachedWord> GetCachedWords()
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