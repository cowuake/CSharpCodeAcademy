using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BaseLibrary
{
    public class OrderEntry
    {
        public MenuItem MenuItem { get; }
        public byte Quantity { get; set; }

        public OrderEntry(MenuItem item, byte quantity)
        {
            MenuItem = item;
            Quantity = quantity;
        }
    }
}
