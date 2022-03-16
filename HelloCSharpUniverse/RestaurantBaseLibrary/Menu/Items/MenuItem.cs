using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BaseLibrary
{
    public abstract class MenuItem : IMenuItem
    {
        public string UniqueID { get; }
        public string Name { get; }
        public MenuItemCategory Category { get; }
        public decimal Price { get; }
        public MenuItemTaxClass TaxClass { get; }
        public string Description { get; }

        public MenuItem(string uniqueID, string name, MenuItemCategory category,
                        decimal price, MenuItemTaxClass taxClass, string description)
        {
            UniqueID = uniqueID;
            Name = name;
            Category = category;
            Price = price;
            TaxClass = taxClass;
            Description = description;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Item {UniqueID} summary");
            sb.AppendLine($"\tName: {Name}");
            sb.AppendLine($"\tCategory: {Category}");
            sb.AppendLine($"\tTax class: {TaxClass}");
            sb.AppendLine($"\tDescription:{Description}");

            return sb.ToString();
        }

        public bool Read(Object source) => false;
        public bool Show() => false;
    }
}