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
    
    public async Task AnagramOutput(string userInput)
    {
        try	
        {
            HttpResponseMessage response = await Client.GetAsync($"https://localhost:7188/api/Anagram/{userInput}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseBody);
        }
        catch(HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");	
            Console.WriteLine("Message :{0} ",e.Message);
        }
    }
}