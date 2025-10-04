using EncodeFile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EncodeFile.Views.UCs
{
    /// <summary>
    /// Interaction logic for Base64Panel.xaml
    /// </summary>
    public partial class Base64Panel : UserControl
    {
        
        public static readonly DependencyProperty ModelProperty =
        DependencyProperty.Register(nameof(Model), typeof(Base64FileModel), typeof(Base64Panel),
            new PropertyMetadata(OnModelChanged));

        public Base64FileModel Model
        {
            get => (Base64FileModel)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }
        private static void OnModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (Base64Panel)d;
            if (e.NewValue != null) 
                control.DataContext = new { ViewModel = e.NewValue };
        }
        
        

        public Base64Panel()
        {
            InitializeComponent();
        }
    }
}
