using Library.InsertBookWPF.Model;
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

namespace Library.InsertBookWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly WebApiClient _client;

        public MainWindow()
        {
            InitializeComponent();
            LoadGenreData();
            _client = new WebApiClient();
        }

        #region ========================= ACTIONS =========================

        private void ResetClick(object sender, RoutedEventArgs e)
        {
            ResetTextBoxes();
        }

        private void InsertClick(object sender, RoutedEventArgs e)
        {
            BookContract book = ValidateForm(out string errMsg);
            if (book == null)
            {
                MessageBox.Show($"Error on form: {errMsg}",
                    "Error on validating form",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            var result = _client.InsertBook(book).Result;
            if (result)
            {

                MessageBox.Show(
                    $"Success: the book '{book.ISBN}' has been inserted",
                    "success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                ResetTextBoxes();
            }
            else
            {
                MessageBox.Show($"Error on insert '{book.Title}'",
                    "Error on insert",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private BookContract ValidateForm(out string msg)
        {
            msg = null;

            string isbn = tbxISBN.Text;
            if (string.IsNullOrEmpty(isbn))
            {
                msg = "Null ISBN field";
                return null;
            }

            string title = tbxTitle.Text;
            if (string.IsNullOrEmpty(title))
            {
                msg = "Null Title field";
                return null;
            }

            string author = tbxAuthor.Text;
            if (string.IsNullOrEmpty(author))
            {
                msg = "Null Author field";
                return null;
            }

            var selectedGenre = cbGenre.SelectedItem;
            if (selectedGenre == null || !(selectedGenre is BookGenreContract))
            {
                msg = "Invalid book genre";
                return null;
            }

            if (string.IsNullOrEmpty(tbxPages.Text) || !int.TryParse(tbxPages.Text, out int pages))
            {
                msg = "Invalid Pages field";
                return null;
            }

            return new BookContract
            {
                ISBN = isbn,
                Author = author,
                Pages = pages,
                Title = title,
                Summary = tbxSummary.Text,
                BookGenreId = (selectedGenre as BookGenreContract).Id,
            };
        }

        private void LoadGenreData()
        {
            var data = _client.GetBookGenres().Result;

            cbGenre.ItemsSource = data;
        }

        #endregion ========================= ACTIONS =========================

        #region ========================= UTILITIES =========================

        private void ResetTextBoxes()
        {
            tbxISBN.Text = String.Empty;
            tbxTitle.Text = String.Empty;
            tbxAuthor.Text = String.Empty;
            tbxSummary.Text = String.Empty;
            tbxPages.Text = String.Empty;
        }

        #endregion ========================= UTILITIES =========================

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }
    }
}