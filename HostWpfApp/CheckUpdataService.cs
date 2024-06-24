using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HostWpfApp;

public class CheckUpdataService:BackgroundService
{
    private readonly ILogger<CheckUpdataService> _logger;

    public CheckUpdataService(ILogger<CheckUpdataService> logger)
    {
        _logger = logger;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested) 
        {
            await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
            _logger.LogInformation("checking for updates...");
        }
    }
}