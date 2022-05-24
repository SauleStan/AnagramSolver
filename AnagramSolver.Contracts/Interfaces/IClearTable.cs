namespace AnagramSolver.Contracts.Interfaces;

public interface IClearTable
{
    Task<bool> ClearTableAsync(string tableName);
}