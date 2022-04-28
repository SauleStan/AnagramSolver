using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.Contracts.DataAccess;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic;

public class WordService : IWordService
{
    private readonly IWordRepository _wordRepository = new WordFileAccess();
    
    public HashSet<string> GetWords()
    {
        return _wordRepository.GetWords();
    }
}