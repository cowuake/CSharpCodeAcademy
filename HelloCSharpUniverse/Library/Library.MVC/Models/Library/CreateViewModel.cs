using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.MVC.Models.Library
{
    public class CreateViewModel
    {
        [DisplayName("ISBN code"), Required]
        public string ISBN { get; set; }

        [DisplayName("Title"), Required]
        public string Title { get; set; }

        [DisplayName("Author"), Required]
        public string Author { get; set; }

        [DisplayName("Summary")]
        public string Summary { get; set; }

        [DisplayName("Number of pages")]
        public int? Pages { get; set; }

        [DisplayName("Publisher")]
        public string Publisher { get; set; }

        [DisplayName("Year")]
        public int? Year { get; set; }

        [DisplayName("Edition")]
        public int? Edition { get; set; }

        [DisplayName("Note")]
        public string Note { get; set; }

        [DisplayName("Language")]
        public string Language { get; set; }
    }
}
