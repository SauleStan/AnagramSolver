using AnagramSolver.BusinessLogic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

// Configuration
using IHost host = Host.CreateDefaultBuilder(args).Build();

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

// Get constraints from appsettings
var minLength = config.GetSection("Constraints").GetValue<int>("MinInput");

AnagramController anagramController = new AnagramController();

// User input
var inputWord = "";
do
{
    Console.WriteLine("Your input: ");
    inputWord = Console.ReadLine();
    if (inputWord != null && inputWord.Length < minLength)
    {
        Console.WriteLine("Minimal input length: {0}", minLength);
    }
} while (inputWord != null && inputWord.Length < minLength);

if (inputWord != null)
{
    var words = anagramController.FindAnagrams(inputWord);
    Console.WriteLine("Anagrams: ");
    foreach (var word in words)
    {
        Console.WriteLine(word);
    }
}