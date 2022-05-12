namespace AnagramSolver.EF.CodeFirst.Models;

public partial class CachedWordEntity
{
    public int Id { get; set; }
    public string InputWord { get; set; }
    public int? AnagramId { get; set; }
    
    public virtual WordEntity? Anagram { get; set; }
}