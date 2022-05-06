using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.Contracts.DataAccess;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic.Services;

public class WordService : IWordService
{
    private readonly IWordRepository _wordRepository;

    public WordService(IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }
    
    public HashSet<string> GetWords()
    {
        return _wordRepository.GetWords().ToHashSet();
    }

    public bool AddWord(string word)
    {
        try
        {
            if (_wordRepository.GetWords().Contains(word))
            {
                throw new ArgumentException($"{word} already exists.");
            }
            _wordRepository.AddWord(word);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}