using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IAnagramService
{
    public HashSet<Anagram> ConvertToAnagrams(IEnumerable<string> words);
    public Anagram ConvertToAnagram(string word);
    
}