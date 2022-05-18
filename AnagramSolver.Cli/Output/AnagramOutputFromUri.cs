using System.Net.Http.Json;
using AnagramSolver.Cli.Interfaces;

namespace AnagramSolver.Cli.Output;

public class AnagramOutputFromUri : IAnagramOutput
{
    private static readonly HttpClient Client;

    static AnagramOutputFromUri()
    {
        var handler = new HttpClientHandler();
        handler.ClientCertificateOptions = ClientCertificateOption.Manual;
        handler.ServerCertificateCustomValidationCallback = 
            (httpRequestMessage, cert, cetChain, policyErrors) => true;

        Client = new HttpClient(handler);
    }
    
    public async Task<List<string>> AnagramOutput(string userInput)
    {
        try	
        {
            HttpResponseMessage response = await Client.GetAsync($"https://localhost:7188/api/Anagram/{userInput}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadFromJsonAsync<List<string>>();
            if (responseBody != null)
            {
                return responseBody;
            }
        }
        catch(HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");	
            Console.WriteLine("Message :{0} ",e.Message);
        }
        return new List<string>();
    }
}