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

namespace Library.BookGenreWPF
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

        private void OpenInsertBookGenreWindow(object sender, RoutedEventArgs e)
        {
            Views.InsertBookGenreWindow window = new Views.InsertBookGenreWindow();

            bool? result = window.ShowDialog();

            if (result == true)
                (DataContext as BookGenresViewModel).RefreshBookGenres();
        }
    }
}
