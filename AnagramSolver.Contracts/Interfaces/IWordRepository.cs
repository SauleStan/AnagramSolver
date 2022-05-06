namespace AnagramSolver.Contracts.Interfaces;

public interface IWordRepository
{
    IEnumerable<string> GetWords();
    void AddWord(string word);
}