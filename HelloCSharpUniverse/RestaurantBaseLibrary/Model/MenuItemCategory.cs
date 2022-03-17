using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BaseLibrary
{
    public class MenuItemCategory
    {
        public string UniqueID { get; }
        public string Name { get; }
        public string Description { get; }
        public uint Order { get; }
        public uint VAT { get; }

        public MenuItemCategory(string uniqueID, string name, string description, uint order, uint vat)
        {
            UniqueID = uniqueID;
            Name = name;
            Description = description;
            Order = order;
            VAT = vat;
        }
    }
}
