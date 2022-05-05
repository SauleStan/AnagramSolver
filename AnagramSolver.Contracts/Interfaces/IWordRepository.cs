namespace AnagramSolver.Contracts.Interfaces;

public interface IWordRepository
{
    IEnumerable<string> GetWords(string path);
    void AddWord(string word, string path);
}