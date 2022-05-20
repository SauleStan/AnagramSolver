using Newtonsoft.Json;

namespace AnagramSolver.Anagramica;

public class AnagramicaClient : IAnagramicaClient
{
    private readonly HttpClient _client;

    public AnagramicaClient()
    {
        var handler = new HttpClientHandler();
        _client = new HttpClient(handler);
    }
    
    public async Task<List<string>> GetAllAnagramsAsync(string userInput)
    {
        try	
        {
            var jsonResult = await FetchData($"http://www.anagramica.com/all/:{userInput}");
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

    public async Task<List<string>> GetBestAnagramsAsync(string userInput)
    {
        try
        {
            var jsonResult = await FetchData($"http://www.anagramica.com/best/:{userInput}");
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