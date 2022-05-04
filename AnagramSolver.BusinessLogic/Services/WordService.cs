using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.Contracts.DataAccess;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic.Services;

public class WordService : IWordService
{
    private readonly IWordRepository _wordRepository = new WordFileAccess();
    private readonly string _path;

    public WordService(string path)
    {
        _path = path;
    }
    
    public HashSet<string> GetWords()
    {
        return _wordRepository.GetWords(_path);
    }

    public bool AddWord(string word)
    {
        try
        {
            if (_wordRepository.GetWords(_path).Contains(word))
            {
                throw new ArgumentException($"{word} already exists.");
            }
            _wordRepository.AddWord(word, _path);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}