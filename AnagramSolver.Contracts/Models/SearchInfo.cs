namespace AnagramSolver.Contracts.Models;

public class SearchInfo
{
    public int Id { get; set; }
    public string UserIp { get; set; }
    public TimeSpan ExecTime { get; set; }
    public string SearchedWord { get; set; }
    public List<string> Anagrams { get; init; }

    public SearchInfo()
    {
        Anagrams = new List<string>();
    }
}