using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ConsoleClient.Model
{
    internal class BookContract
    {
        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Author { get; set; }

        public int? Pages { get; set; }

        public string Publisher { get; set; }

        public int? Year { get; set; }

        public int? Edition { get; set; }

        public string Note { get; set; }

        public string Language { get; set; }

        public int? BookGenreId { get; set; }
    }
}