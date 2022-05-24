namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IClearTable
{
    Task<bool> ClearTableAsync(string tableName);
}