// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


using var host = Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(builder =>
    {
        builder.AddJsonFile("appsettings.json");
        builder.AddCommandLine(args);
        builder.AddUserSecrets<Program>();

    } )
    .ConfigureServices(builder =>
    {
        //builder.AddTransient<MyService>(); 
        builder.AddHostedService<MyService>();
    })
    .Build();
host.Start();

// var service = host.Services.GetRequiredService<MyService>();
// service.DoWork();
host.WaitForShutdown();

class MyService:BackgroundService
{
    private readonly IHostEnvironment _hostEnvrironmrent;
    private readonly ILogger<MyService> _logger;
    private readonly IConfiguration _configuration;

    public MyService(IHostEnvironment hostEnvrironmrent,ILogger<MyService> logger,IConfiguration configuration)
    {
        _hostEnvrironmrent = hostEnvrironmrent;
        _logger = logger;
        _configuration = configuration;
    }

    public void DoWork()
    {
        _logger.LogInformation($"Application Name:{_hostEnvrironmrent.ApplicationName}");
        _logger.LogInformation($"Environment:{_hostEnvrironmrent.EnvironmentName}");
        _logger.LogInformation($"Content Root Path:{_hostEnvrironmrent.ContentRootPath}");
        _logger.LogDebug($"LogLevel:{_configuration["Logging:LogLevel:Default"]}");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Run(DoWork);
    }
}