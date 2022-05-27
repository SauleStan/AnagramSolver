using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Interfaces;

public interface ISearchInfo
{
    Task<IEnumerable<SearchInfo>> GetAnagramSearchInfoAsync();
    Task<WordResult> AddAnagramSearchInfoAsync(SearchInfo searchInfo);
}