using System.ComponentModel;

namespace Library.MVC.Models.Library
{
    public class LibraryViewModel
    {
        [DisplayName("ISBN")]
        public string ISBN { get; set; }

        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Author")]
        public string Author { get; set; }

        [DisplayName("Genre")]
        public string Genre { get; set; }

        [DisplayName("Return Url")]
        public string ReturnUrl { get; set; }
    }
}