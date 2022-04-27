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

namespace Library.SimpleClientWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ResetClick(object sender, RoutedEventArgs e)
        {
            tbxISBN.Text = String.Empty;
            tbxTitle.Text = String.Empty;
            tbxAuthor.Text = String.Empty;
            tbxSummary.Text = String.Empty;
            tbxPages.Text = String.Empty;
        }

        private void InsertClick(object sender, RoutedEventArgs e)
        {

        }
    }
}