using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Cli;
using AnagramSolver.Cli.Interfaces;
using Microsoft.Extensions.Configuration;

var environment = Environment.GetEnvironmentVariable("APSNETCORE_ENVIRONMENT");

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.local.json", true, true)
    .AddJsonFile($"appsettings.{environment}.json", true, true)
    .AddEnvironmentVariables()
    .Build();

Console.InputEncoding = System.Text.Encoding.Unicode;
Console.OutputEncoding = System.Text.Encoding.Unicode;

var minLength = config.GetSection("Constraints").GetValue<int>("MinInput");
var minAnagrams = config.GetSection("Constraints").GetValue<int>("MinAnagramCount");
var maxAnagrams = config.GetSection("Constraints").GetValue<int>("MaxAnagramCount");
var dataFilePath = config.GetValue<string>("DataFilePath");

if (dataFilePath != null)
{
    IAnagramSolver anagramSolver = new AnagramController(new AnagramService(), new WordService(dataFilePath));

    IUserInput userInput = new UserInput(minLength);

    while (true)
    {
        var inputWord = userInput.GetUserInput();

        var anagrams = anagramSolver.FindAnagrams(inputWord);

        Console.WriteLine("Anagrams: ");
        if (anagrams.Count < minAnagrams)
        {
            Console.WriteLine($"Less than {minAnagrams} anagrams have been found. Try another word.");
        }
        foreach (var anagram in anagrams.Take(maxAnagrams))
        {
            Console.WriteLine(anagram);
        }
    }
}