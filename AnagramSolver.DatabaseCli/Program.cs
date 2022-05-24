using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.DataAccess;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.CodeFirst.Models;
using AnagramSolver.EF.CodeFirst.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var environment = Environment.GetEnvironmentVariable("APSNETCORE_ENVIRONMENT");

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.local.json", true, true)
    .AddJsonFile($"appsettings.{environment}.json", true, true)
    .AddEnvironmentVariables()
    .Build();

Console.InputEncoding = System.Text.Encoding.Unicode;
Console.OutputEncoding = System.Text.Encoding.Unicode;

var dataFilePath = config.GetValue<string>("WordFilePath");

var services = new ServiceCollection().AddDbContext<AnagramSolverDbContext>(options => 
    options.UseSqlServer(config.GetConnectionString("DefaultConnection")!));
var serviceProvider = services.BuildServiceProvider();

if (dataFilePath != null)
{
    var wordFileService = new WordFileRepository(dataFilePath);
    var wordDbRepository = new AnagramSolverDbRepository(serviceProvider.GetService<AnagramSolverDbContext>()!);
    var words = await wordFileService.GetWordsAsync();
    var stringWords = words.Select(word => word.Name);
    await wordDbRepository.AddWordsAsync(stringWords!);
}