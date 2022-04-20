namespace Library.MVC.Models.Library
{
    public class BookDetailsViewModel
    {
        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Summary { get; set; }

        public int? Pages { get; set; }

        public string Publisher { get; set; }

        public int? Year { get; set; }

        public int? Edition { get; set; }

        public string Note { get; set; }

        public string Language { get; set; }

        public string Genre { get; set; }
    }
}