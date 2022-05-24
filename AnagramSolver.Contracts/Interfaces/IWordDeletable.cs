namespace AnagramSolver.Contracts.Interfaces;

public interface IWordDeletable
{
    Task DeleteWordAsync(string word);
}