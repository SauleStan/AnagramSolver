using AnagramSolver.BusinessLogic;
using Microsoft.Extensions.Configuration;

// Configuration
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

// Get constraints from appsettings
var minLength = config.GetSection("Constraints").GetValue<int>("MinInput");
var minAnagrams = config.GetSection("Constraints").GetValue<int>("MinAnagramCount");
var maxAnagrams = config.GetSection("Constraints").GetValue<int>("MaxAnagramCount");

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
    if (words.Count < minAnagrams)
    {
        Console.WriteLine("Less than {0} anagrams have been found. Try another word.", minAnagrams);
    }
    foreach (var word in words.Take(maxAnagrams))
    {
        Console.WriteLine(word);
    }
}