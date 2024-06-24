using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Configuration;

namespace HostWpfApp;

public class MainViewModel : ObservableObject
{

    public string Message { get; set; } = "Hello World";
    public string? LogLevel { get; set; }

    public MainViewModel(IConfiguration configuration)
    {

        LogLevel = configuration["Logging:LogLevel:Microsoft"];
    }
    
}