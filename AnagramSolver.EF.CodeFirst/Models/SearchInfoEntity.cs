namespace AnagramSolver.EF.CodeFirst.Models;

public class SearchInfoEntity
{
    public int Id { get; set; }
    public string UserIp { get; set; }
    public TimeSpan ExecTime { get; set; }
    public string SearchedWord { get; set; }
    public int? AnagramId { get; set; }

    public virtual WordEntity? Anagram { get; set; }
}