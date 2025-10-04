using EncodeFile.Services;
using EncodeFile.ViewModels;
using EncodeFile.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace EncodeFile;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private ServiceProvider _serviceProvider;
    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IBase64Service, Base64Service>();
        services.AddSingleton<IPdfService, PdfService>();
        services.AddSingleton<MainModel>();
        services.AddSingleton<Base64FileModel>();
        services.AddSingleton<PdfFileModel>();
        services.AddSingleton<MainWindow>();
    }
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();

        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}

