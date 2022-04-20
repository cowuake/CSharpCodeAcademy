using System.ComponentModel;

namespace Library.MVC.Models.BookGenres
{
    public class ListBookGenresViewModel
    {
        public int Id { get; set; }

        [DisplayName("Genre name")]
        public string Name { get; set; }

        [DisplayName("# books")]
        public int BooksCount { get; set; }
    }
}