using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic;

public class AnagramController : IAnagramSolver
{
    private readonly HashSet<string> _fetchedWords;
    private HashSet<Anagram> _fetchedAnagrams;
    private readonly HashSet<string> _anagramsSet;
    private readonly IAnagramService _anagramService;

    public AnagramController(IAnagramService anagramService, IWordService wordService)
    {
        _anagramService = anagramService;
        _fetchedWords = wordService.GetWords();
        _fetchedAnagrams = new HashSet<Anagram>();
        _anagramsSet = new HashSet<string>();
    }

    public HashSet<string> FindAnagrams(string inputWord)
    {
        _fetchedWords.RemoveWhere(x => x.Length != inputWord.Length);
        
        _fetchedAnagrams = _anagramService.ConvertToAnagrams(_fetchedWords);
        
        Anagram inputAnagram = _anagramService.ConvertToAnagram(inputWord);
        
        foreach (var word in _fetchedAnagrams)
        {
            if (_anagramService.IsEqualCrib(word, inputAnagram) && !word.Name.Equals(inputAnagram.Name))
            {
                _anagramsSet.Add(word.Name);
            }
        }

        return _anagramsSet;
    }
}