namespace Contracts.Interfaces;

public interface IWordRepository
{
    HashSet<string> GetWords();
}