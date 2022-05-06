using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.Cli.Interfaces;

namespace AnagramSolver.Cli.Output;

public class AnagramOutputFromResolver : IAnagramOutput
{
    private readonly IAnagramResolver _anagramResolver;
    private readonly int _minAnagrams;
    private readonly int _maxAnagrams;
    
    public AnagramOutputFromResolver(IAnagramResolver anagramResolver, int minAnagrams, int maxAnagrams)
    {
        _anagramResolver = anagramResolver;
        _minAnagrams = minAnagrams;
        _maxAnagrams = maxAnagrams;
    }
    
    public Task AnagramOutput(string userInput)
    {
        var inputWord = userInput;

        var anagrams = _anagramResolver.FindAnagrams(inputWord);

        Console.WriteLine("Anagrams: ");
        if (anagrams.Count < _minAnagrams)
        {
            Console.WriteLine($"Less than {_minAnagrams} anagrams have been found. Try another word.");
        }

        foreach (var anagram in anagrams.Take(_maxAnagrams))
        {
            Console.WriteLine(anagram);
        }
        return Task.CompletedTask;
    }
}