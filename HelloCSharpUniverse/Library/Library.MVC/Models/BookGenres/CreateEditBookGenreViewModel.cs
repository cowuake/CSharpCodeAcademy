using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.MVC.Models.BookGenres
{
    public class CreateEditBookGenreViewModel
    {
        public int Id { get; set; }

        [DisplayName("Genre name"), Required]
        public string Name { get; set; }
    }
}