using ImportData.Helpers;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;

namespace ImportData;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    bool IsDarkMode()
    {
        using var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
            @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
        return key != null && (int)(key.GetValue("AppsUseLightTheme") ?? 1) == 0;
    }
    protected override void OnStartup(StartupEventArgs e)
    {
        //ControlProperties.Init();
        base.OnStartup(e);
        #if DEBUG
        PresentationTraceSources.DataBindingSource.Listeners.Add(new BindingErrorThrower());
        PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.Error;
        #endif
        
    }
}
class BindingErrorThrower : TraceListener
{
    public override void Write(string message) { }

    public override void WriteLine(string message)
    {
        if (message.Contains("BindingExpression") && message.Contains("cannot find"))
        {
            throw new InvalidOperationException($"Binding error: {message}");
        }
    }
}

