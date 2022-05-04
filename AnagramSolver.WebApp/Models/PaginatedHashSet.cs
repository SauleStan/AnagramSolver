namespace AnagramSolver.WebApp.Models;

public class PaginatedHashSet<T> : HashSet<T>
{
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }

    private PaginatedHashSet(IEnumerable<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        PageSize = pageSize;
        UnionWith(items);
    }

    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPages;

    public static PaginatedHashSet<string> Create(IEnumerable<string> source, int pageIndex, int pageSize)
    {
        var enumerable = source.ToList();
        var count = enumerable.Count();
        HashSet<string> items = enumerable.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToHashSet();
        return new PaginatedHashSet<string>(items, count, pageIndex, pageSize);
    }
}