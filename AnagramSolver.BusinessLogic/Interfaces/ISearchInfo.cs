using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Interfaces;

public interface ISearchInfo
{
    Task<IEnumerable<SearchInfo>> GetAnagramSearchInfoAsync();
    Task<ActionResult> AddAnagramSearchInfoAsync(SearchInfo searchInfo);
}