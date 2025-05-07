using ImportData.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ImportData.Helpers;
namespace ImportData.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new
        {
            Model = new MainViewModel()
        };
        this.InitLoadingControl("Model.IsRunning");
    }
    protected override void OnContentRendered(EventArgs e)
    {
        base.OnContentRendered(e);
        
    }
}