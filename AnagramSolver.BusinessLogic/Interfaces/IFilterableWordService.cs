namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IFilterableWordService : IWordService
{
    Task<IEnumerable<string>> GetFilteredWordsAsync(string filter);
}