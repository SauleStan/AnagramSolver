namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IFilterable
{
    Task<IEnumerable<string>> GetFilteredWordsAsync(string filter);
}