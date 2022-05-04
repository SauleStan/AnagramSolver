namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IWordService
{
    public HashSet<string> GetWords();
    bool AddWord(string word);
}