using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces;

public interface IFilterable
{
    Task<IEnumerable<Word>> GetFilteredWordsAsync(string filter);
}