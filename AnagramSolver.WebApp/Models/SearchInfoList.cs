using AnagramSolver.Contracts.Models;

namespace AnagramSolver.WebApp.Models;

public class SearchInfoList
{
    public List<SearchInfo> SearchInfos { get; }

    public SearchInfoList(List<SearchInfo> searchInfoList)
    {
        SearchInfos = searchInfoList;
    }
}