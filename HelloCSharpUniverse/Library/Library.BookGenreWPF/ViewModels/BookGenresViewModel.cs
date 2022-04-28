using Library.BookGenreWPF.Models;
using Library.BookGenreWPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Library.BookGenreWPF.ViewModels
{
    public class BookGenresViewModel : Notifiable
    {
        private WebApiClient _client;
        private ObservableCollection<BookGenreModel> _genres;

        public BookGenresViewModel()
        {
            _client = new WebApiClient();
        }

        public ObservableCollection<BookGenreModel> Genres
        {
            get
            {
                if (_genres != null)
                    return _genres;

                return new ObservableCollection<BookGenreModel>(_client.GetBookGenresAsync().Result);
            }

            set
            {
                if (_genres != value)
                {
                    _genres = value;
                    NotifyChange();
                }
            }
        }

        public void RefreshBookGenres()
        {
            Genres = new ObservableCollection<BookGenreModel>(_client.GetBookGenresAsync().Result);
        }
    }
}