namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IWordService
{
    public HashSet<string> GetWords();
    void AddWord(string word);
}