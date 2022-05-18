using AnagramSolver.Anagramica.Client;
using AnagramSolver.BusinessLogic.Interfaces;

namespace AnagramSolver.BusinessLogic.Resolvers;

public class AnagramicaResolver : IAnagramResolver
{
    private readonly IAnagramicaClient _anagramicaClient;

    public AnagramicaResolver(IAnagramicaClient anagramicaClient)
    {
        _anagramicaClient = anagramicaClient;
    }
    public List<string> FindAnagrams(string inputWord)
    {
        return _anagramicaClient.GetAllAnagrams(inputWord);
    }
}