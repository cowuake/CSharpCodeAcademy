using System;
using System.Collections.Generic;
using System.Text;

namespace Library.WebAPI.ConsoleClient.Model
{
    public class BookContract
    {
        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Author { get; set; }
    }
}