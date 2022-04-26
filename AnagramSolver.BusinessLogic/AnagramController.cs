using AnagramSolver.Contracts.Models;
using AnagramSolver.Contracts.DataAccess;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic;

public class AnagramController
{
    private readonly IWordRepository _wordRepository = new FileDataAccess();
    private readonly HashSet<string> _fetchedWords;
    private HashSet<Anagram> _fetchedAnagrams;
    private readonly HashSet<string> _anagramsSet;
    private readonly IAnagramService _anagramService;

    public AnagramController(IAnagramService anagramService)
    {
        _anagramService = anagramService;
        _fetchedWords = _wordRepository.GetWords();
        _fetchedAnagrams = new HashSet<Anagram>();
        _anagramsSet = new HashSet<string>();
    }

    public HashSet<string> FindAnagrams(string inputWord)
    {
        // Filter the set to same length words
        _fetchedWords.RemoveWhere(x => x.Length != inputWord.Length);
        
        // Convert the set to Anagram models
        _fetchedAnagrams = _anagramService.ConvertToAnagrams(_fetchedWords);

        // Convert inputWord to Anagram
        Anagram inputAnagram = _anagramService.ConvertToAnagram(inputWord);
        
        // Check for anagrams
        foreach (var word in _fetchedAnagrams)
        {
            // Compares anagram cribs and ignores the word that is the same as input word
            if (_anagramService.IsEqualCrib(word, inputAnagram) && !word.Name.Equals(inputAnagram.Name))
            {
                _anagramsSet.Add(word.Name);
            }
        }

        return _anagramsSet;
    }
}