using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic;

public class AnagramService : IAnagramService
{
    public bool IsEqualCrib(Anagram anagram1, Anagram anagram2)
    {
        return anagram1.Crib.Equals(anagram2.Crib);
    }
    
    public HashSet<Anagram> ConvertToAnagrams(HashSet<string> words)
    {
        var anagramSet = new HashSet<Anagram>();
        foreach (var word in words)
        {
            anagramSet.Add(new Anagram(word, SortWordAlphabetically(word)));
        }

        return anagramSet;
    }

    public Anagram ConvertToAnagram(string word)
    {
        return new Anagram(word, SortWordAlphabetically(word));
    }

    private string SortWordAlphabetically(string word)
    {
        char[] chars = word.ToCharArray();
        Array.Sort(chars);
        return new string(chars);
    }
}