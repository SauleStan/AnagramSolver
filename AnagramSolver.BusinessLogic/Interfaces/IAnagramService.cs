using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IAnagramService
{
    public HashSet<Anagram> ConvertToAnagrams(HashSet<string> words);
    public Anagram ConvertToAnagram(string word);
    
}