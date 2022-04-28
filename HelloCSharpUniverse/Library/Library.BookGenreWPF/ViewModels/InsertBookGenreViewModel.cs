using Library.BookGenreWPF.Commands;
using Library.BookGenreWPF.Models;
using Library.BookGenreWPF.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Library.BookGenreWPF.ViewModels
{
    public class InsertBookGenreViewModel : Notifiable
    {
        private ICommand _insertCommand;
        private WebApiClient _client;
        private BookGenreModel _genre;
        private Action _onSuccessAction;

        public InsertBookGenreViewModel()
        {
            _client = new WebApiClient();
            _genre = new BookGenreModel();
        }
        

        public InsertBookGenreViewModel(Action action)
        {
            _genre = new BookGenreModel();
            _client = new WebApiClient();
            _onSuccessAction = action;
        }

        public string Name
        {
            get { return _genre.Name; }
            set { if (_genre.Name != value) { _genre.Name = value; NotifyChange(); } }
        }
        
        public string Family
        {
            get { return _genre.Family; }
            set { if (_genre.Family != value) { _genre.Family = value; NotifyChange(); } }
        }

        
        public ICommand InsertCmd
        {
            get
            {
                if (_insertCommand == null)
                    _insertCommand = new RelayCommand(InsertBookGenreExecute, true);
                return _insertCommand;
            }
        }

        private bool ValidForm(out string msg)
        {
            msg = null;

            if (string.IsNullOrEmpty(Name))
            {
                msg = "Book genre's name is null or empty";
                return false;
            }

            if (string.IsNullOrEmpty(Family))
            {
                msg = "Book genre's family is null or empty";
                return false;
            }

            return true;
        }

        private void InsertBookGenreExecute()
        {
            if (!ValidForm(out string errMsg))
            {
                MessageBox.Show($"Error on form: {errMsg}",
                    "Error on validating form",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            var result = _client.InsertBookGenre(_genre).Result;

            if (result)
            {
                MessageBox.Show(
                    $"SUCCESS: book genre '{_genre.Family}/{_genre.Name}' inserted.",
                    "success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                _onSuccessAction();
            }
            else
            {
                MessageBox.Show($"FAILURE: Error when inserting book genre '{_genre.Family}/{_genre.Name}'.",
                    "Error on Insert",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}