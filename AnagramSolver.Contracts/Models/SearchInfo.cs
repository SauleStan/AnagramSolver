namespace AnagramSolver.Contracts.Models;

public class SearchInfo
{
    public int Id { get; init; }
    public string UserIp { get; init; } = null!;
    public TimeSpan ExecTime { get; init; }
    public string SearchedWord { get; init; } = null!;
    public List<string> Anagrams { get; init; }

    public SearchInfo()
    {
        Anagrams = new List<string>();
    }
}