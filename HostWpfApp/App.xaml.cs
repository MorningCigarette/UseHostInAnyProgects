using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace HostWpfApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    [STAThread]
    static void Main(string[] args)
    {
        using var host = CreateHostBuilder(args).Build();
        host.Start();
        var app = new App();
        app.InitializeComponent();
        app.MainWindow = host.Services.GetRequiredService<MainWindow>();
        // app.MainWindow.DataContext = host.Services.GetRequiredService<MainViewMo del>();
        app.MainWindow.Visibility = Visibility.Visible;
        app.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices(container =>
            {
                container.AddHostedService<CheckUpdataService>();
                container.AddSingleton<MainWindow>(sp => new MainWindow()
                    { DataContext = sp.GetRequiredService<MainViewModel>() });
                container.AddSingleton<MainViewModel>();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.File("log.text", rollingInterval: RollingInterval.Day)
                    .CreateLogger();
                logging.AddSerilog(Log.Logger);
                //下面是另一种方式
                //logging.Services.AddSingleton(Log.Logger);//viemodel中的log就不是泛型了
            });
    }
}