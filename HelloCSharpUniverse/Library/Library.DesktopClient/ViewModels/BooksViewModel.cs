using Library.DesktopClient.Commands;
using Library.DesktopClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Library.DesktopClient.ViewModels
{
    public class BooksViewModel : Notifiable
    {
        private WebApiClient _webApiClient;
        private BookFilterModel _filterModel;
        private ObservableCollection<BookModel> _books;
        private ICommand Search;

        public BooksViewModel()
        {
            _webApiClient = new WebApiClient();
            _filterModel = new BookFilterModel();
            _books = new ObservableCollection<BookModel>(_webApiClient.GetBooksAsync(_filterModel).Result);
            
        }

        public string SearchIsbn
        {
            get;
            set;
        }

        public string SearchTitle
        {
            get;
            set;
        }

        public ObservableCollection<BookModel> Books
        {
            get { return _books; }
            set { if (_books != value) { _books = value; NotifyChange(); } }
        }

        public RelayCommand Search
        {
            get { return new RelayCommand(SearchBooks, true); }
            set;
        }

        private void SearchBooks()
        {
            _books = new ObservableCollection<BookModel>(_webApiClient.GetBooksAsync(_filterModel).Result);
        }
    }
}