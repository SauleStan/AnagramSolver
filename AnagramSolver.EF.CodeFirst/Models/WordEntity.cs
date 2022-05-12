namespace AnagramSolver.EF.CodeFirst.Models;

public class WordEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public virtual List<CachedWordEntity> CachedWords { get; set; }
    public virtual List<SearchInfoEntity> SearchInfos { get; set; }
}