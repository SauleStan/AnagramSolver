namespace AnagramSolver.WebApp.Models;

public class AnagramList
{
    public List<string> Anagrams { get; set; }

    public AnagramList(List<string> anagrams)
    {
        Anagrams = anagrams;
    }
}