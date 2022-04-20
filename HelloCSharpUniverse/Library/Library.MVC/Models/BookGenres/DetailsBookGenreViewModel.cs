using Library.MVC.Models.Library;
using System.Collections.Generic;
using System.ComponentModel;

namespace Library.MVC.Models.BookGenres
{
    public class DetailsBookGenreViewModel
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Books")]
        public IEnumerable<LibraryViewModel> Books {get;set;}
    }
}