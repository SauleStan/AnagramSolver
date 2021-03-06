using System.Text.RegularExpressions;
using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Services;

public class AnagramService : IAnagramService
{
    public HashSet<Anagram> ConvertToAnagrams(IEnumerable<string> words)
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

    private static string SortWordAlphabetically(string word)
    {
        word = Regex.Replace(word, @"\s", "");
        var chars = word.ToCharArray();
        Array.Sort(chars);
        return new string(chars);
    }
}