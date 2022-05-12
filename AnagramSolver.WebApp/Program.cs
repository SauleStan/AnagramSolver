using System.Reflection;
using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using AnagramSolver.EF.DatabaseFirst.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("APSNETCORE_ENVIRONMENT");

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.local.json", true, true)
    .AddJsonFile($"appsettings.{environment}.json", true, true)
    .AddEnvironmentVariables()
    .Build();

var minLength = config.GetSection("Constraints").GetValue<int>("MinInput");
var minAnagrams = config.GetSection("Constraints").GetValue<int>("MinAnagramCount");
var maxAnagrams = config.GetSection("Constraints").GetValue<int>("MaxAnagramCount");
var dataFilePath = config.GetValue<string>("WordFilePath");

var directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
var path = directoryPath + dataFilePath;

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AnagramDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IWordRepository, WordDatabaseRepository>();
builder.Services.AddScoped<IWordService, WordService>();
builder.Services.AddScoped<IAnagramService, AnagramService>();
builder.Services.AddScoped<IAnagramResolver, AnagramResolver>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{input?}");

app.Run();