using Newtonsoft.Json;

namespace AnagramSolver.Anagramica.Client;

public class AnagramicaClient : IAnagramicaClient
{
    private readonly HttpClient _client;

    public AnagramicaClient()
    {
        var handler = new HttpClientHandler();
        _client = new HttpClient(handler);
    }
    
    public List<string> GetAllAnagrams(string userInput)
    {
        try	
        {
            var jsonResult = FetchData($"http://www.anagramica.com/all/:{userInput}").Result;
            if (jsonResult != null)
            {
                return jsonResult["all"];
            }
        }
        catch(HttpRequestException e)
        {
            throw;
        }
        return new List<string>();
    }

    public List<string> GetBestAnagrams(string userInput)
    {
        try
        {
            var jsonResult = FetchData($"http://www.anagramica.com/best/:{userInput}").Result;
            if (jsonResult != null)
            {
                return jsonResult["best"];
            }
        }
        catch(HttpRequestException e)
        {
            throw;
        }
        return new List<string>();
    }

    private async Task<Dictionary<string, List<string>>?> FetchData(string uri)
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(responseBody);
        }
        catch (Exception)
        {
            throw;
        }
    }
}