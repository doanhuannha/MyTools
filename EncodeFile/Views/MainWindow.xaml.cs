using EncodeFile.Helpers;
using EncodeFile.ViewModels;
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
using EncodeFile.Helpers;

namespace EncodeFile.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
partial class MainWindow : BaseWindow
{
    public MainWindow(EncodeFileViewModel model)
    {
        InitializeComponent();


        // Set the DataContext for the Window
        //DataContext = model;
        DataContext = new
        {
            MyModel = model
        };
        this.InitLoadingControl("MyModel.IsProcessing");
    }

}