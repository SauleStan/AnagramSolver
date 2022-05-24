using System.Diagnostics;
using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.BusinessLogic.Resolvers;
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
    IUserInput userInput = new UserInput(minLength);

    var input = userInput.GetUserInput();
    // IDisplay display = new Display();
    DisplayWithEvents display = new ();
    display.DisplayText += display_WriteToConsole;
    display.DisplayText += display_WriteToDebug;
    display.DisplayText += display_WriteToFile;
    display.FormattedPrint(InputToUppercase, input);
    
    // while (true)
    // {
    //     await anagramOutput.AnagramOutput(userInput.GetUserInput());
    // }

}

void display_WriteToConsole(object sender, string input)
{
    Console.WriteLine(input);
}
void display_WriteToDebug(object sender, string input)
{
    Debug.WriteLine(input);
}
void display_WriteToFile(object sender, string input)
{
    Console.WriteLine($"Wrote {input} to file");
}

void WriteToConsole(string input)
{
    Console.WriteLine(input);
}

void WriteToDebug(string input)
{
    Debug.WriteLine(input);
}

void WriteToFile(string input)
{
    Console.WriteLine($"Wrote {input} to file");
}

string InputToUppercase(string input)
{
    return string.Concat(input[..1].ToUpper(), input.AsSpan(1));
}