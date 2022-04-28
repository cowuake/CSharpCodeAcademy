using Library.BookGenreWPF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BookGenreWPF.ViewModels
{
    public class InsertBookGenreViewModel : Notifiable
    {
        private BookGenreModel _model;

        public InsertBookGenreViewModel()
            => _model = new BookGenreModel();

        public string Name
        {
            get { return _model.Name; }
            set { }
        }
        
        public BookGenreFamily Family { get; set; }
    }

    public enum BookGenreFamily : byte
    {
        Fiction,
        Nonfiction
    }
}