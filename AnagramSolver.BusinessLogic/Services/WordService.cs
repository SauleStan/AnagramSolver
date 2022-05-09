using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic.Services;

public class WordService : IWordService
{
    private readonly IWordRepository _wordRepository;

    public WordService(IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }
    
    public IEnumerable<string> GetWords()
    {
        var words = _wordRepository.GetWords().Select(word => word.Name);
        return words;
    }

    public IEnumerable<string> GetFilteredWords(string filter)
    {
        var words = _wordRepository.GetFilteredWords(filter).Select(word => word.Name);
        return words;
    }

    public bool AddWord(string word)
    {
        try
        {
            if (_wordRepository.GetWords().Any(x => x.Name == word))
            {
                throw new ArgumentException($"{word} already exists.");
            }

            if (!_wordRepository.AddWord(word))
            {
                throw new Exception("Failed to add the word.");
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}