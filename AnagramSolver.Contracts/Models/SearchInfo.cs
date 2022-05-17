namespace AnagramSolver.Contracts.Models;

public class SearchInfo
{
    public int Id { get; init; }
    public string? UserIp { get; init; }
    public TimeSpan? ExecTime { get; init; }
    public string? SearchedWord { get; init; }
    public List<string> Anagrams { get; init; }

    public SearchInfo()
    {
        Anagrams = new List<string>();
    }
}