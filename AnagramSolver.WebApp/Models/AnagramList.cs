namespace AnagramSolver.WebApp.Models;

public class AnagramList
{
    public string Word { get; set; }
    public List<string> Anagrams { get; set; }

    public AnagramList(List<string> anagrams, string word)
    {
        Anagrams = anagrams;
        Word = word;
    }
}