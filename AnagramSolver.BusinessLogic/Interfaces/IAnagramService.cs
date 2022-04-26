using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IAnagramService
{
    public bool IsEqualCrib(Anagram anagram1, Anagram anagram2);
    public HashSet<Anagram> ConvertToAnagrams(HashSet<string> words);
    public Anagram ConvertToAnagram(string word);
    
}