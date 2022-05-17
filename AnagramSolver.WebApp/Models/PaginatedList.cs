namespace AnagramSolver.WebApp.Models;

public class PaginatedList<T> : List<T>
{
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }

    private PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        PageSize = pageSize;
        AddRange(items);
    }

    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPages;

    public static PaginatedList<string> Create(IEnumerable<string> source, int pageIndex, int pageSize)
    {
        var enumerable = source.ToList();
        var count = enumerable.Count;
        var items = enumerable.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        return new PaginatedList<string>(items, count, pageIndex, pageSize);
    }
    
}