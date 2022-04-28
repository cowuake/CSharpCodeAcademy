using Library.BookGenreWPF.ViewModels;
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

namespace Library.BookGenreWPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class InsertBookGenreWindow : Window
    {
        public InsertBookGenreWindow()
        {
            InitializeComponent();

            DataContext = new InsertBookGenreViewModel(() => { DialogResult = true; Close(); });
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}