using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.MVC.Models.Library
{
    public class CreateEditBookViewModel
    {
        [DisplayName("ISBN code"), Required(ErrorMessage = "ISBN is mandatory.")]
        public string ISBN { get; set; }

        [DisplayName("Title"), Required(ErrorMessage = "Title is mandatory.")]
        public string Title { get; set; }

        [DisplayName("Author"), Required(ErrorMessage = "Author is mandatory.")]
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

        [DisplayName("Category ID")]
        public string BookCategoryId { get; set; }

        public IEnumerable<SelectListItem> AvailableCategories;
    }
}