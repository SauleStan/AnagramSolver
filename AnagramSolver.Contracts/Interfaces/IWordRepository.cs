namespace AnagramSolver.Contracts.Interfaces;

public interface IWordRepository
{
    IEnumerable<string> GetWords();
    bool AddWord(string word);
}