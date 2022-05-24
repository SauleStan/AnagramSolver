using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces;

public interface ISearchInfo
{
    Task<IEnumerable<SearchInfo>> GetAnagramSearchInfoAsync();
    Task AddAnagramSearchInfoAsync(SearchInfo searchInfo);
}