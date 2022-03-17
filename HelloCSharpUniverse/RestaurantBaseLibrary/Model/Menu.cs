using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.BaseLibrary
{
    public class Menu
    {
        public DateTime LastUpdated { get; }
        public IList<MenuItem> Items { get; }

        public bool Show()
        {
            Console.WriteLine("Printing menu.");

            Items.Select(item => item.ToString()).ToList()
                .ForEach(item => Console.WriteLine(item));

            return true;
        }
    }
}
