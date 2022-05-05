namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IWordService
{
    public List<string> GetWords();
    bool AddWord(string word);
}