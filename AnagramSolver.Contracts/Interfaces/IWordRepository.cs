namespace AnagramSolver.Contracts.Interfaces;

public interface IWordRepository
{
    HashSet<string> GetWords(string path);
    void AddWord(string word, string path);
}