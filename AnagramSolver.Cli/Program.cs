using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.Interfaces;
using Microsoft.Extensions.Configuration;

// Configuration
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

Console.InputEncoding = System.Text.Encoding.Unicode;
Console.OutputEncoding = System.Text.Encoding.Unicode;

// Get constraints from appsettings
var minLength = config.GetSection("Constraints").GetValue<int>("MinInput");
var minAnagrams = config.GetSection("Constraints").GetValue<int>("MinAnagramCount");
var maxAnagrams = config.GetSection("Constraints").GetValue<int>("MaxAnagramCount");

IAnagramSolver anagramSolver = new AnagramController(new AnagramService());

// User input
var inputWord = "";
bool validInput = false;
do
{
    Console.WriteLine("Your input: ");
    inputWord = Console.ReadLine();
    if (inputWord != null)
    {
        if (inputWord.Length < minLength)
        {
            Console.WriteLine("Minimal input length: {0}", minLength);
        }
        else
        {
            validInput = true;
        }
    }
} while (!validInput);

if (inputWord != null)
{
    var words = anagramSolver.FindAnagrams(inputWord);
    
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