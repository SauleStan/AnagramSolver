using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.Cli.Interfaces;

namespace AnagramSolver.Cli.Output;

public class AnagramOutputFromResolver : IAnagramOutput
{
    private readonly IAnagramResolver _anagramResolver;
    
    public AnagramOutputFromResolver(IAnagramResolver anagramResolver)
    {
        _anagramResolver = anagramResolver;
    }
    
    public async Task<List<string>> AnagramOutput(string userInput)
    {
        var inputWord = userInput;
        var anagrams = await _anagramResolver.FindAnagramsAsync(inputWord);
        return anagrams;
    }
}