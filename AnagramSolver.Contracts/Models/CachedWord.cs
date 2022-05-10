namespace AnagramSolver.Contracts.Models;

public class CachedWord
{
    public int Id { get; set; }
    public string InputWord { get; set; }
    public List<string> Anagrams { get; set; }

    public CachedWord()
    {
        Anagrams = new List<string>();
    }
}