using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DesktopClient.Models
{
    public class BookModel
    {
        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Summary { get; set; }

        public int Pages { get; set; }

        public int BookGenreId { get; set; }
    }
}