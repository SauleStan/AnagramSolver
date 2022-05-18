namespace AnagramSolver.Anagramica.Client;

public interface IAnagramicaClient
{
    List<string> GetAllAnagrams(string userInput);
    List<string> GetBestAnagrams(string userInput);
}