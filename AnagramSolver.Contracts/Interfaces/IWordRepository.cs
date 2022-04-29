namespace AnagramSolver.Contracts.Interfaces;

public interface IWordRepository
{
    HashSet<string> GetWords(string path);
}