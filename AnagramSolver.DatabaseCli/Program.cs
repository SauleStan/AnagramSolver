using AnagramSolver.BusinessLogic.Services;
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

var dataFilePath = config.GetValue<string>("WordFilePath");

if (dataFilePath != null)
{
    var wordFileService = new WordService(new WordFileAccess(dataFilePath));
    var wordDbAccess = new WordDbAccess();

    foreach (var word in wordFileService.GetWords())
    {
        wordDbAccess.AddWord(word);
    }
}