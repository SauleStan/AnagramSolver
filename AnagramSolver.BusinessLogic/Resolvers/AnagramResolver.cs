using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Resolvers;

public class AnagramResolver : IAnagramResolver
{
    private readonly IAnagramService _anagramService;
    private readonly IWordService _wordService;

    public AnagramResolver(IAnagramService anagramService, IWordService wordService)
    {
        _anagramService = anagramService;
        _wordService = wordService;
    }

    public async Task<List<string>> FindAnagramsAsync(string inputWord)
    {
        var anagramSet = new HashSet<string>();
        
        var fetchedAnagrams = _anagramService.ConvertToAnagrams((await _wordService.GetWordsAsync())!);
        
        var inputAnagram = _anagramService.ConvertToAnagram(inputWord);
        
        foreach (var anagram in fetchedAnagrams)
        {
            if (anagram.Crib.Equals(inputAnagram.Crib) && !anagram.Name.Equals(inputAnagram.Name))
            {
                anagramSet.Add(anagram.Name);
            }
        }

        return anagramSet.ToList();
    }
}