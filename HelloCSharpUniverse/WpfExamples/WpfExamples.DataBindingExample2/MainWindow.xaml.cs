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

namespace WpfExamples.DataBindingExample2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this; // The window is a data context for itself
        }

        private void updateSourceClick(object sender, RoutedEventArgs e)
        {
            BindingExpression binding = heightText.GetBindingExpression(TextBox.TextProperty);
            BindingGroup.UpdateSources();
        }
    }
}
