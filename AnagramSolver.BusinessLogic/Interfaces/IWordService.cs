namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IWordService
{
    public IEnumerable<string> GetWords();
    public IEnumerable<string> GetFilteredWords(string filter);
    bool AddWord(string word);
}