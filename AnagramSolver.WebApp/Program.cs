using AnagramSolver.BusinessLogic.Interfaces;
using AnagramSolver.BusinessLogic.Resolvers;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.CodeFirst.Models;
using AnagramSolver.EF.CodeFirst.Repositories;
using AspNetCoreRateLimit;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using ICacheable = AnagramSolver.BusinessLogic.Interfaces.ICacheable;
using IClearTable = AnagramSolver.BusinessLogic.Interfaces.IClearTable;
using ISearchInfo = AnagramSolver.BusinessLogic.Interfaces.ISearchInfo;
using LogLevel = NLog.LogLevel;

var environment = Environment.GetEnvironmentVariable("APSNETCORE_ENVIRONMENT");

var logger = NLogBuilder.ConfigureNLog("nlog.config.xml").GetCurrentClassLogger();
logger.Trace("Program started");

try
{
    var builder = WebApplication.CreateBuilder(args);

    IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.local.json", true, true)
        .AddJsonFile($"appsettings.{environment}.json", true, true)
        .AddEnvironmentVariables()
        .Build();

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.WithOrigins("https://localhost:7188/")
                .AllowAnyOrigin();
        });
    });

    builder.Services.AddMemoryCache();

    builder.Services.Configure<IpRateLimitOptions>(config.GetSection("IpRateLimiting"));
    builder.Services.Configure<IpRateLimitPolicies>(config.GetSection("IpRateLimitPolicies"));

    builder.Services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
    builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
    builder.Services.AddInMemoryRateLimiting();
    builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
    builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

    builder.Services.AddControllersWithViews();

    builder.Services.AddDbContext<AnagramSolverDbContext>(options =>
        options.UseLazyLoadingProxies().UseSqlServer(config.GetConnectionString("DatabaseConnection2")!));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddScoped<IWordRepository, AnagramSolverDbRepository>();
    builder.Services.AddScoped<IWordService, WordService>();
    builder.Services.AddScoped<IFilterableWordService, WordService>();
    builder.Services.AddScoped<ISearchInfo, WordService>();
    builder.Services.AddScoped<IClearTable, WordService>();
    builder.Services.AddScoped<ICacheable, WordService>();
    builder.Services.AddScoped<IAnagramService, AnagramService>();
    builder.Services.AddScoped<IAnagramResolver, AnagramResolver>();

    var app = builder.Build();

    app.UseIpRateLimiting();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseCors();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{input?}");

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}