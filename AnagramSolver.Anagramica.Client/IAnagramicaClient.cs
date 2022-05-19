namespace AnagramSolver.Anagramica.Client;

public interface IAnagramicaClient
{
    Task<List<string>> GetAllAnagramsAsync(string userInput);
    Task<List<string>> GetBestAnagramsAsync(string userInput);
}