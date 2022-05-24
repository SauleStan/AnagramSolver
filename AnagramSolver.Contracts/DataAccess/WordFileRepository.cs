using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.DataAccess;

public class WordFileRepository : IWordFilterable
{
    private readonly HashSet<Word> _words = new ();
    private readonly string _path;
    private const string Separator = "/t";
    public WordFileRepository(string path)
    {
        _path = path;
    }
    public async Task<IEnumerable<Word>> GetWordsAsync()
    {
        
        using (var streamReader = new StreamReader(_path))
        {
            while (await streamReader.ReadLineAsync() is { } line)
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

    public async Task<IEnumerable<Word>> GetFilteredWordsAsync(string filter)
    {
        using (var sr = new StreamReader(_path))
        {
            while (await sr.ReadLineAsync() is { } line)
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

    public async Task<bool> AddWordAsync(string word)
    {
        try
        {
            await File.AppendAllTextAsync(_path, word + Environment.NewLine);
            return true;
        }
        catch (FileNotFoundException)
        {
            return false;
        }
    }

    public async Task<bool> AddWordsAsync(IEnumerable<string> words)
    {
        try
        {
            await File.AppendAllLinesAsync(_path, words);
            return true;
        }
        catch (FileNotFoundException)
        {
            return false;
        }
    }
}