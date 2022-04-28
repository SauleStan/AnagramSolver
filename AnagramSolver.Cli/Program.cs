using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Cli;
using AnagramSolver.Cli.Interfaces;
using Microsoft.Extensions.Configuration;

// Configuration
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.local.json", true, true)
    .AddJsonFile("appsettings.{environment}.json", true, true)
    .AddEnvironmentVariables()
    .Build();

Console.InputEncoding = System.Text.Encoding.Unicode;
Console.OutputEncoding = System.Text.Encoding.Unicode;

// Get constraints from appsettings
var minLength = config.GetSection("Constraints").GetValue<int>("MinInput");
var minAnagrams = config.GetSection("Constraints").GetValue<int>("MinAnagramCount");
var maxAnagrams = config.GetSection("Constraints").GetValue<int>("MaxAnagramCount");

IAnagramSolver anagramSolver = new AnagramController(new AnagramService(), new WordService());

IUserInput userInput = new UserInput(minLength);

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
