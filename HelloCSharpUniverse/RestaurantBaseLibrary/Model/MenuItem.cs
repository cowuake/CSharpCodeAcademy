using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BaseLibrary
{
    public class MenuItem
    {
        public string UniqueID { get; }
        public string Name { get; }
        public MenuItemCategory Category { get; }
        public decimal Price { get; }
        public string Description { get; }

        public MenuItem(string uniqueID, string name, MenuItemCategory category,
                        decimal price, string description)
        {
            UniqueID = uniqueID;
            Name = name;
            Category = category;
            Price = price;
            Description = description;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Item {UniqueID} summary");
            sb.AppendLine($"\tName: {Name}");
            sb.AppendLine($"\tCategory: {Category}");
            sb.AppendLine($"\tDescription:{Description}");

            return sb.ToString();
        }

        public bool Read(Object source)
        {
            if (!(source is string))
                return false;

            source = source as string;

            return false;
        }

        public bool Show()
        {
            string s = ToString();

            if (s == null)
                return false;

            Console.WriteLine(s);
            return true;
        }
    }
}