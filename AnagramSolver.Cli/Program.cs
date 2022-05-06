using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Cli.Input;
using AnagramSolver.Cli.Interfaces;
using AnagramSolver.Cli.Output;
using AnagramSolver.Contracts.DataAccess;
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
var dataFilePath = config.GetValue<string>("WordFilePath");

if (dataFilePath != null)
{
    IAnagramResolver anagramResolver = new AnagramResolver(new AnagramService(), new WordService(new WordFileAccess(dataFilePath)));

    IUserInput userInput = new UserInput(minLength);

    IAnagramOutput anagramOutput = new AnagramOutputFromUri();
    while (true)
    {
        await anagramOutput.AnagramOutput(userInput.GetUserInput());
    }

}