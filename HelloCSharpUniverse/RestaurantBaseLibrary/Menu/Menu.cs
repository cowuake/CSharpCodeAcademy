using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BaseLibrary
{
    public class Menu : IMenu
    {
        public DateTime LastUpdated { get; }
        public IList<MenuItem> Items { get; }

        public bool Show() => false;
    }
}
