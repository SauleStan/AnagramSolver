using AnagramSolver.Anagramica;
using AnagramSolver.BusinessLogic.Interfaces;

namespace AnagramSolver.BusinessLogic.Resolvers;

public class AnagramicaResolver : IAnagramResolver
{
    private readonly IAnagramicaClient _anagramicaClient;

    public AnagramicaResolver(IAnagramicaClient anagramicaClient)
    {
        _anagramicaClient = anagramicaClient;
    }
    public async Task<List<string>> FindAnagramsAsync(string inputWord)
    {
        return await _anagramicaClient.GetAllAnagramsAsync(inputWord);
    }
}