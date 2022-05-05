using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic;

public class AnagramResolver : IAnagramResolver
{
    private HashSet<Anagram> _fetchedAnagrams = new();
    private readonly HashSet<string> _anagramsSet = new();
    private readonly IAnagramService _anagramService;
    private readonly IWordService _wordService;

    public AnagramResolver(IAnagramService anagramService, IWordService wordService)
    {
        _anagramService = anagramService;
        _wordService = wordService;
    }

    public List<string> FindAnagrams(string inputWord)
    {
        _anagramsSet.Clear();
        
        var filteredFetchedWords = new HashSet<string>(_wordService.GetWords());
        filteredFetchedWords.RemoveWhere(x => x.Length != inputWord.Length);
        
        _fetchedAnagrams = _anagramService.ConvertToAnagrams(_wordService.GetWords());
        
        var inputAnagram = _anagramService.ConvertToAnagram(inputWord);
        
        foreach (var anagram in _fetchedAnagrams)
        {
            if (anagram.Crib.Equals(inputAnagram.Crib) && !anagram.Name.Equals(inputAnagram.Name))
            {
                _anagramsSet.Add(anagram.Name);
            }
        }

        return _anagramsSet.ToList();
    }
}